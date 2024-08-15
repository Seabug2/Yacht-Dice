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

        // ���� Ŭ���̾�Ʈ�� �ٸ� Ŭ���̾�Ʈ ����
        if (conn.connectionId != 0) // ���� ���� Ŭ���̾�Ʈ�� connectionId�� 0�Դϴ�.
        {
            OnRemoteClientConnected(conn);
        }
    }

    void OnRemoteClientConnected(NetworkConnection conn)
    {
        Debug.Log($"���ο� ���� Ŭ���̾�Ʈ�� ����Ǿ����ϴ�! Connection ID: {conn.connectionId}");
        waitingMessage.SetActive(false);
        dicePannel.SetActive(true);

        NetworkClient.localPlayer.GetComponent<YachtPlayer>().CmdMyTurn();
        //GameObject.Find("Local Player Score Board").GetComponent<ScoreBoard>().StartTurn();
    }
}
