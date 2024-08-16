using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Net;
using System.Net.Sockets;
using UnityEngine.SceneManagement;

// ����� �� ���� ��ũ��Ʈ
public class CHJ_TestScript : MonoBehaviour
{
    public InputField score;
    public Toggle isWin;
    public Button apply;

    void Start()
    {
        string localIP = GetLocalIPv4();
        Debug.Log("Local IP Address: " + localIP);
    }

    string GetLocalIPv4()
    {
        string localIP = string.Empty;
        foreach (var ip in Dns.GetHostEntry(Dns.GetHostName()).AddressList)
        {
            if (ip.AddressFamily == AddressFamily.InterNetwork)
            {
                localIP = ip.ToString();
                break;
            }
        }
        return localIP;
    }

    public void Apply()
    {
        SQLManager.instance.Result(isWin.isOn, int.Parse(score.text));
        SceneManager.LoadScene(0);
    }
}
