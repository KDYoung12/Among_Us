using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrewFloater : MonoBehaviour
{
    // 소환할 프리펩을 받을 변수 선언
    [SerializeField]
    private GameObject prefab;
    // 크루원의 이미지를 바꿔줄 변수 선언
    [SerializeField]
    private List<Sprite> sprites;

    // 떠다니는 크루원의 색깔을 중복되지 않게 해줄 변수 선언
    private bool[] crewStates = new bool[12];
    // 크루원을 소환할 간격을 계산하는 변수 선언
    private float timer = 0.5f;
    // 크루원이 중심으로 부터 소환될 위치를 정하는 변수 선언
    private float distance = 9f;


    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < 12; i++)
        {
            SpawnFloatingCrew((EPlayerColor)i, Random.Range(0f, distance));
        }
    }

    // Update is called once per frame
    void Update()
    {
        // 일정 시간마다 소환할 수 있도록 코드 작성
        timer -= Time.deltaTime;
        if(timer <= 0f)
        {
            SpawnFloatingCrew((EPlayerColor)Random.Range(0, 12), distance);
            timer = 1f;
        }
    }

    // 크루원을 소환하는데 쓰이는 함수 생성
    public void SpawnFloatingCrew(EPlayerColor playerColor, float dist)
    {
        // 같은 색깔이 소환되지 않은 상황이라면 
        if (!crewStates[(int)playerColor])
        {
            crewStates[(int)playerColor] = true;
            // 카메라의 영역을 벗어나서 크루원을 소환하게 해야함
            float angle = Random.Range(0f, 2f * Mathf.PI);
            // 중심으로 부터 원형을 그리는 벡터를 만들 수 있음 (cos, sin 사용)
            Vector3 spawnPos = new Vector3(Mathf.Sin(angle), Mathf.Cos(angle), 0f) * dist;
            // 날아갈 방향
            Vector3 direction = new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f), 0f);
            // 속도값
            float floatingSpeed = Random.Range(1f, 4f);
            // 회전값
            float rotateSpeed = Random.Range(-0.6f, 0.4f);

            // 크루원 생성
            var crew = Instantiate(prefab, spawnPos, Quaternion.identity).GetComponent<FloatingCrew>();
            // SetFloatingCrew함수에 매개변수 할당
            crew.SetFloatingCrew(sprites[Random.Range(0, sprites.Count)], playerColor, direction, floatingSpeed,
                rotateSpeed, Random.Range(0.5f, 1f));
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        var crew = collision.GetComponent<FloatingCrew>();
        // 크루원이 CrewFloater의 영역을 벗어났을 때 삭제
        if(crew != null)
        {
            crewStates[(int)crew.playerColor] = false;
            Destroy(crew.gameObject);
        }
    }

}
