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
            log.text = "�α��ο� �����߽��ϴ�. �ּҸ� Ȯ�� ���ּ���.";
        }
    }

    public override void OnClientConnect()
    {
        base.OnClientConnect();
        // ���ῡ �������� �� ������ �ڵ�
        joinUI.SetActive(false);
        dicePannel.SetActive(true);
        log.text = "������ ���������� ����Ǿ����ϴ�.";
    }
}
