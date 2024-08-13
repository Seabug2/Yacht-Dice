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
        //SQLManager의 info 정보에서 닉네임과 저적을 가져온다.
        nickName.text = SQLManager.instance.info.User_nickname;
        int win = SQLManager.instance.info.wins;
        int lose = SQLManager.instance.info.loses;
        rate.text = $"{win + lose}전 {win}승 {lose}패";
    }
}
