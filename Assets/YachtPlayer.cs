using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Mirror;

public class YachtPlayer : NetworkBehaviour
{
    [SerializeField]
    Text nickName;
    [SerializeField]
    Text rate;

    void Start()
    {
        if (!isLocalPlayer)
        {

        }   
    }

    void Init()
    {
        //SQLManager�� info �������� �г��Ӱ� ������ �����´�.
        //nickName.text = 
        //record.text = 
    }
}
