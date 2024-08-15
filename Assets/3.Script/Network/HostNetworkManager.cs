using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class HostNetworkManager : NetworkManager
{
    [SerializeField, Space(20)]
    GameObject waitingMessage;
    [SerializeField]
    GameObject dicePannel;

    public override void OnServerConnect(NetworkConnectionToClient conn)
    {
        base.OnServerConnect(conn);

        // 로컬 클라이언트와 다른 클라이언트 구분
        if (conn.connectionId != 0) // 보통 로컬 클라이언트의 connectionId는 0입니다.
        {
            OnRemoteClientConnected(conn);
        }
    }

    void OnRemoteClientConnected(NetworkConnection conn)
    {
        Debug.Log($"새로운 원격 클라이언트가 연결되었습니다! Connection ID: {conn.connectionId}");
        waitingMessage.SetActive(false);
        dicePannel.SetActive(true);

        NetworkClient.localPlayer.GetComponent<YachtPlayer>().CmdMyTurn();
        //GameObject.Find("Local Player Score Board").GetComponent<ScoreBoard>().StartTurn();
    }
}
