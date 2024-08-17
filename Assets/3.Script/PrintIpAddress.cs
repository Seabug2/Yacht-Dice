using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PrintIpAddress : MonoBehaviour
{
    void Start()
    {
        GetComponent<Text>().text = $"상대를 기다리는 중...\r\n(Local Address : {GetMyAddress.GetLocalIPv4()})";
    }
}
