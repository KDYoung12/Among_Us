using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuUI : MonoBehaviour
{
    public void OnClickOnlineButton()
    {
        Debug.Log("Click Online");
    }
    public void OnClickQuitButton()
    {
        // 게임을 나갔을 때
        // .. 유니티 에디터 상태라면 플레이 중단
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
        // 에디터가 아닌 빌드상태라면 종료
#else
        Application.Quit();
#endif
    }
}
