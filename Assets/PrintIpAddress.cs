using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PrintIpAddress : MonoBehaviour
{
    void Start()
    {
        GetComponent<Text>().text = $"��븦 ��ٸ��� ��...\r\n(Local Address : {GetMyAddress.GetLocalIPv4()})";
    }
}
