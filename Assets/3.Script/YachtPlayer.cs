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

    private void Awake()
    {
        transform.SetParent(GameObject.Find("Profiles").transform);    
    }

    void Start()
    {
        if (isLocalPlayer)
        {
            nickName.text = "YOU";
        }
        else
        {

        }
    }

    void Init()
    {
        //SQLManager�� info �������� �г��Ӱ� ������ �����´�.
        nickName.text = SQLManager.instance.info.User_nickname;
        int win = SQLManager.instance.info.wins;
        int lose = SQLManager.instance.info.loses;
        rate.text = $"{win + lose}�� {win}�� {lose}��";
    }
}
