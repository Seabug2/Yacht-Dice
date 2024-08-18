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

            if (NetworkServer.active || NetworkClient.isConnected)
            {
                StopClient();
            }

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

    // 호스트와의 연결이 끊어졌을 때 호출되는 메서드
    public override void OnClientDisconnect()
    {
        if (!isQuitting)
        {
            base.OnClientDisconnect();

            // 연결이 끊어졌을 때 0번째 씬으로 이동
            SceneManager.LoadScene(0);
        }
    }

    private bool isQuitting = false;

    // 애플리케이션 종료 시 호출되는 메서드
    public override void OnApplicationQuit()
    {
        isQuitting = true;
        base.OnApplicationQuit();
    }
}