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

        // 로컬 클라이언트와 다른 클라이언트 구분
        if (conn.connectionId != 0)
        {
            waitingMessage.SetActive(false);
            dicePannel.SetActive(true);
            NetworkClient.localPlayer.GetComponent<YachtPlayer>().CmdMyTurn();
        }
    }

    private bool isStopping = false; // 플래그 변수 추가

    public override void OnServerDisconnect(NetworkConnectionToClient conn)
    {
        if (isStopping) return; // 이미 종료 중이면 재실행 방지

        base.OnServerDisconnect(conn);

        if (NetworkServer.connections.Count <= 1) // 연결된 클라이언트가 없을 경우에만 실행
        {
            isStopping = true; // 플래그 설정
            StopHost(); // 서버 종료
            SceneManager.LoadScene(0); // 씬 이동
        }
    }
}
