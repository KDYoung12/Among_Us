using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// SettingUI의 기능과 비슷한 기능들이 많이 필요하기 때문에 상속함
public class GameRoomSettingUI : SettingUI
{
    public void ExitGameRoom()
    {
        // Network Manager 가져오기
        var manager = AmongUsRoomManager.singleton;
        // Manager의 모드가 Host라면
        if(manager.mode == Mirror.NetworkManagerMode.Host)
        {
            // StopHost() 호출
            manager.StopHost();
        }
        // Manager의 모드가 ClientOnly라면
        else if(manager.mode == Mirror.NetworkManagerMode.ClientOnly)
        {
            // StopClient() 호출
            manager.StopClient();
        }
    }
}
