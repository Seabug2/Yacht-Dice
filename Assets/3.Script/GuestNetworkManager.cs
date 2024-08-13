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
            joinUI.SetActive(false);
        }
        catch(Exception e)
        {
            Debug.Log(e.Message);
            log.text = "�α��ο� �����߽��ϴ�. �ּҸ� Ȯ�� ���ּ���.";
        }
    }
}
