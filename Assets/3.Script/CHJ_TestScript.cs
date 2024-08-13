using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Net;
using System.Net.Sockets;


public class CHJ_TestScript : MonoBehaviour
{
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

}
