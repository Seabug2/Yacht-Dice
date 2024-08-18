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

        // ���� Ŭ���̾�Ʈ�� �ٸ� Ŭ���̾�Ʈ ����
        if (conn.connectionId != 0) // ���� ���� Ŭ���̾�Ʈ�� connectionId�� 0�Դϴ�.
        {
            waitingMessage.SetActive(false);
            dicePannel.SetActive(true);
            NetworkClient.localPlayer.GetComponent<YachtPlayer>().CmdMyTurn();
        }
    }

    public override void OnServerDisconnect(NetworkConnectionToClient conn)
    {
        base.OnServerDisconnect(conn);
        
        SceneManager.LoadScene("Host Room");
    }
}
