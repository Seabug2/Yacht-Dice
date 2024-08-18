using UnityEngine;
using UnityEngine.UI;
using Mirror;
using UnityEngine.SceneManagement;

public class HostNetworkManager : NetworkManager
{
    [SerializeField]
    GameObject waitingMessage;
    [SerializeField]
    GameObject dicePannel;
    [SerializeField]
    Button reroll;

    public override void Awake()
    {
        base.Awake();

        waitingMessage.SetActive(true);
        dicePannel.SetActive(false);
        networkAddress = GetMyAddress.GetLocalIPv4();

        if (NetworkServer.active || NetworkClient.isConnected)
        {
            StopHost();
        }
        StartHost();
    }

    public override void Start()
    {
        base.Start();

        reroll.onClick.AddListener (() => {
            NetworkClient.localPlayer.GetComponent<YachtPlayer>().CientProfile();
            reroll.onClick.RemoveListener(() => {
                NetworkClient.localPlayer.GetComponent<YachtPlayer>().CientProfile();
            });
        });
    }

    public override void OnServerConnect(NetworkConnectionToClient conn)
    {
        base.OnServerConnect(conn);

        // ���� Ŭ���̾�Ʈ�� �ٸ� Ŭ���̾�Ʈ ����
        if (conn.connectionId != 0)
        {
            waitingMessage.SetActive(false);
            dicePannel.SetActive(true);
            NetworkClient.localPlayer.GetComponent<YachtPlayer>().CmdMyTurn();
        }
    }

    private bool isStopping = false; // �÷��� ���� �߰�

    public override void OnServerDisconnect(NetworkConnectionToClient conn)
    {
        if (isStopping) return; // �̹� ���� ���̸� ����� ����

        base.OnServerDisconnect(conn);

        if (NetworkServer.connections.Count <= 1) // ����� Ŭ���̾�Ʈ�� ���� ��쿡�� ����
        {
            isStopping = true; // �÷��� ����
            StopHost(); // ���� ����
            SceneManager.LoadScene(0); // �� �̵�
        }
    }
}
