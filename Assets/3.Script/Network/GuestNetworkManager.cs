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

    //��ư�� ��������
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
            log.text = "�α��ο� �����߽��ϴ�. �ּҸ� Ȯ�� ���ּ���.";
        }
    }

    public override void OnClientConnect()
    {
        base.OnClientConnect();
        joinUI.SetActive(false);
        dicePannel.SetActive(true);
    }

    // ȣ��Ʈ���� ������ �������� �� ȣ��Ǵ� �޼���
    public override void OnClientDisconnect()
    {
        if (!isQuitting)
        {
            base.OnClientDisconnect();

            // ������ �������� �� 0��° ������ �̵�
            SceneManager.LoadScene(0);
        }
    }

    private bool isQuitting = false;

    // ���ø����̼� ���� �� ȣ��Ǵ� �޼���
    public override void OnApplicationQuit()
    {
        isQuitting = true;
        base.OnApplicationQuit();
    }
}