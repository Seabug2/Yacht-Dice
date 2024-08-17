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
            log.text = "�α��ο� �����߽��ϴ�. �ּҸ� Ȯ�� ���ּ���.";
        }
    }

    public override void OnClientConnect()
    {
        base.OnClientConnect();
        joinUI.SetActive(false);
        dicePannel.SetActive(true);
        //log.text = "������ ���������� ����Ǿ����ϴ�.";
    }

    public override void OnServerDisconnect(NetworkConnectionToClient conn)
    {
        base.OnServerDisconnect(conn);

        OnRemoteClientDisconnected(conn);
    }

    void OnRemoteClientDisconnected(NetworkConnection conn)
    {
        Debug.Log($"���� Ŭ���̾�Ʈ�� ������ ���������ϴ�! Connection ID: {conn.connectionId}");

        SceneManager.LoadScene(0);
    }
}
