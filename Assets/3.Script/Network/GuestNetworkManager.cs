using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Mirror;

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
            //joinUI.SetActive(false);
            //dicePannel.SetActive(true);
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
        // 연결에 성공했을 때 실행할 코드
        joinUI.SetActive(false);
        dicePannel.SetActive(true);
        log.text = "서버에 성공적으로 연결되었습니다.";
    }
}
