using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Mirror;

public class CreateRoomUI : MonoBehaviour
{
    // 이미지를 담는 리스트
    [SerializeField]
    private List<Image> crewImgs;

    // 버튼을 담는 리스트
    [SerializeField]
    private List<Button> imposterCountButtons;
    [SerializeField]
    private List<Button> maxPlayerCountButtons;

    private CreateGameRoomData roomData;

    void Start()
    {
        for(int i = 0; i < crewImgs.Count; i++)
        {
            Material materialInstance = Instantiate(crewImgs[i].material);
            crewImgs[i].material = materialInstance;
        }

        roomData = new CreateGameRoomData() { imposterCount = 1, maxPlayerCount = 10 };
        UpdateCrewImages();
    }
    // 임포스터의 인원수를 변경하는 함수
    public void UpdateImposterCount(int count)
    {
        roomData.imposterCount = count;

        // 버튼의 이미지 테두리를 보이게 하는 로직
        for (int i = 0; i < imposterCountButtons.Count; i++)
        {
            if (i == count - 1)
            {
                imposterCountButtons[i].image.color = new Color(1f, 1f, 1f, 1f);
            }
            else
            {
                imposterCountButtons[i].image.color = new Color(1f, 1f, 1f, 0f);
            }
        }

        // 임포스터 수에 따른 플레이어 수를 지정
        int limitMaxPlayer = count == 1 ? 4 : count == 2 ? 7 : 9;
        if(roomData.maxPlayerCount < limitMaxPlayer)
        {
            UpdateMaxPlayerCount(limitMaxPlayer);
        }
        else
        {
            UpdateMaxPlayerCount(roomData.maxPlayerCount);
        }

        // limitMaxPlayer보다 작은 인원수의 버튼은 비활성화
        for (int i = 0; i < maxPlayerCountButtons.Count; i++)
        {
            var text = maxPlayerCountButtons[i].GetComponentInChildren<Text>();
            if(i < limitMaxPlayer - 4)
            {
                // interactable : 버튼 활성화(true) / 버튼 비활성화(false)
                maxPlayerCountButtons[i].interactable = false;
                text.color = Color.gray;
            }
            else
            {
                maxPlayerCountButtons[i].interactable = true;
                text.color = Color.white;
            }
        }
    }


    // 최대 인원수를 변경하는 함수
    public void UpdateMaxPlayerCount(int count)
    {
        roomData.maxPlayerCount = count;

        // 버튼의 이미지 테두리를 보이게 하는 로직
        for (int i = 0; i < maxPlayerCountButtons.Count; i++)
        {
            if(i == count - 4)
            {
                maxPlayerCountButtons[i].image.color = new Color(1f, 1f, 1f, 1f);
            }
            else
            {
                maxPlayerCountButtons[i].image.color = new Color(1f, 1f, 1f, 0f);
            }
        }

        UpdateCrewImages();
    }

    private void UpdateCrewImages()
    {
        // 색깔을 흰색으로 초기화 해주는 코드
        for (int i = 0; i < crewImgs.Count; i++)
        {
            crewImgs[i].material.SetColor("_PlayerColor", Color.white);
        }

        int imposterCount = roomData.imposterCount;
        int idx = 0;

        // 크루원 중에 임포스터가 몇 명이 있는지 표시
        while(imposterCount != 0)
        {
            if(idx >= roomData.maxPlayerCount)
            {
                idx = 0;
            }

            if(crewImgs[idx].material.GetColor("_PlayerColor") != Color.red && Random.Range(0, 5) == 0)
            {
                crewImgs[idx].material.SetColor("_PlayerColor", Color.red);
                imposterCount--;
            }
            idx++;
        }

        // 설정한 크루원 수 만큼만 이미지를 활성화

        for (int i = 0; i < crewImgs.Count; i++)
        {
            if(i < roomData.maxPlayerCount)
            {
                crewImgs[i].gameObject.SetActive(true);
            }
            else
            {
                crewImgs[i].gameObject.SetActive(false);
            }
        }
    }

    public void CreateRoom()
    {
        var manager = AmongUsRoomManager.singleton;
        // 방 설정
        //
        //
        manager.StartHost();
    }
}

public class CreateGameRoomData
{
    // 이 클래스를 이용하여 새로 생성되는 방의 정보를 저장
    public int imposterCount;
    public int maxPlayerCount;
}