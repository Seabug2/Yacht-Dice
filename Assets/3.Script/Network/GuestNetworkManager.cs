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

    new void Start()
    {
        log.text = string.Empty;
    }

    public void ConnectToServer()
    {
        try
        {
            networkAddress = address.text;
            StartClient();
        }
        catch(Exception e)
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
        //log.text = "서버에 성공적으로 연결되었습니다.";
    }

    public override void OnServerDisconnect(NetworkConnectionToClient conn)
    {
        base.OnServerDisconnect(conn);

        OnRemoteClientDisconnected(conn);
    }

    void OnRemoteClientDisconnected(NetworkConnection conn)
    {
        Debug.Log($"원격 클라이언트가 연결이 끊어졌습니다! Connection ID: {conn.connectionId}");

        SceneManager.LoadScene(0);
    }
}
