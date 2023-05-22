using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class AmongUsRoomManager : NetworkRoomManager
{
    // 서버에서 새로 접속한 클라이언트를 감지했을 때 동작하는 함수 
    public override void OnRoomServerConnect(NetworkConnectionToClient conn)
    {
        base.OnRoomServerConnect(conn);

        var player = Instantiate(spawnPrefabs[0]);
        // 이 플레이어가 소환 되었음을 다른 플레이어에게 알려줌
        NetworkServer.Spawn(player, conn);

    }
}
