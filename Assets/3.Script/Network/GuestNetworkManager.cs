using System;
using UnityEngine;
using UnityEngine.UI;
using Mirror;
using UnityEngine.SceneManagement;

public class GuestNetworkManager : NetworkManager
{
    [SerializeField]
    InputField address;
    [SerializeField]
    Text log;
    [SerializeField]
    GameObject joinUI;
    [SerializeField]
    GameObject dicePannel;

    private new void Awake()
    {
        log.text = string.Empty;
        joinUI.SetActive(true);
        dicePannel.SetActive(false);
    }

    //버튼에 연결해줌
    public void ConnectToServer()
    {
        try
        {
            networkAddress = address.text;
            StartClient();
        }
        catch (Exception e)
        {
            Debug.Log(e.Message);
            log.text = "로그인에 실패했습니다. 주소를 확인 해주세요.";
        }
    }

    public override void OnClientConnect()
    {
        base.OnClientConnect();
        joinUI.SetActive(false);
        dicePannel.SetActive(true);
    }

    public override void OnServerDisconnect(NetworkConnectionToClient conn)
    {
        base.OnServerDisconnect(conn);

        SceneManager.LoadScene("Lobby");
    }
}
