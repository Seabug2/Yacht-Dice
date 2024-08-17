using UnityEngine;
using Mirror;
using UnityEngine.SceneManagement;

public class HostNetworkManager : NetworkManager
{
    [SerializeField]
    GameObject waitingMessage;
    [SerializeField]
    GameObject dicePannel;

    private new void Awake()
    {
        waitingMessage.SetActive(true);
        dicePannel.SetActive(false);
    }

    public override void OnServerConnect(NetworkConnectionToClient conn)
    {
        base.OnServerConnect(conn);

        // 로컬 클라이언트와 다른 클라이언트 구분
        if (conn.connectionId != 0) // 보통 로컬 클라이언트의 connectionId는 0입니다.
        {
            waitingMessage.SetActive(false);
            dicePannel.SetActive(true);
            NetworkClient.localPlayer.GetComponent<YachtPlayer>().CmdMyTurn();
        }
    }

    void OnRemoteClientConnected(NetworkConnection conn)
    {
        Debug.Log($"새로운 원격 클라이언트가 연결되었습니다! Connection ID: {conn.connectionId}");
        waitingMessage.SetActive(false);
        dicePannel.SetActive(true);

        NetworkClient.localPlayer.GetComponent<YachtPlayer>().CmdMyTurn();
    }

    public override void OnServerDisconnect(NetworkConnectionToClient conn)
    {
        base.OnServerDisconnect(conn);
        
        SceneManager.LoadScene("Host Room");
    }

    void OnRemoteClientDisconnected(NetworkConnection conn)
    {
        Debug.Log($"원격 클라이언트가 연결이 끊어졌습니다! Connection ID: {conn.connectionId}");

        //SceneManager.LoadScene(0);
    }
}
