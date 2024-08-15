using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Mirror;
using DG.Tweening;

public class YachtPlayer : NetworkBehaviour
{
    [Header("점수판")]
    [SerializeField] GameManager myManager; //나의 점수판

    YachtPlayer opponent = null;

    private void Awake()
    {

    }

    void Start()
    {
        if (isLocalPlayer)
        {
            name = "Local Player";
            myManager = GameObject.Find("Local Player Score Board").GetComponent<GameManager>();
        }
        else
        {
            name = "Remote Player";
            myManager = GameObject.Find("Remote Player Score Board").GetComponent<GameManager>();
        }
        //Init();
        myManager.PopUp();
    }

    void Init()
    {
        //SQLManager의 info 정보에서 닉네임과 전적을 가져온다.
        string name = SQLManager.instance.info.User_nickname;
        int win = SQLManager.instance.info.wins;
        int lose = SQLManager.instance.info.loses;
        string rate = $"{win + lose}전 {win}승 {lose}패";
        //CmdStartGame(name, rate);

        //이벤트를 연결하는 작업
        //myScoreBoard.StartTurnEvent += ;

        myManager.RerollEvent += CmdUpdateBoard;

        myManager.EndTurnEvent += CmdEndTurn;
    }

    //[Command]
    //public void CmdStartGame(string name, string rate)
    //{
    //    RpcStartGame(name, rate);
    //}
    
    //[ClientRpc]
    //public void RpcStartGame(string name, string rate)
    //{
    //    myManager.InfoUISet(name, rate);
    //}


    [Command]
    public void CmdMyTurn()
    {
        RpcMyTurn();
    }

    [ClientRpc]
    public void RpcMyTurn()
    {
        //자신의 차례를 시작하는 건 상대쪽 클라이언트에서 실행되는 CmdMyTurn() 이므로
        //모든 클라이언트에 존재하는 자신의 YachtPlayer 객체 중에
        //자신의 클라이언트에 존재하는 YachtPlayer만 myManager.StartTurn()를 실행할 수 있도록 한다.
        if (!isLocalPlayer) return;

        myManager.StartTurn();
    }

    [Command]
    void CmdUpdateBoard(int[] _pips)
    {
        RpcUpdateBoard(_pips);
    }

    /// <summary>
    /// 나온 결과 값에 따라 주사위와 점수판을 갱신합니다.
    /// </summary>
    /// <param name="_pips"></param>
    [ClientRpc]
    void RpcUpdateBoard(int[] _pips)
    {
        myManager.UpdateSlot(_pips);
    }

    [Command]
    public void CmdEndTurn(int[] _points)
    {
        RpcEndTurn(_points);
    }

    [ClientRpc]
    public void RpcEndTurn(int[] _points)
    {
        myManager.FixedSlot(_points);
        opponent.CmdMyTurn();
    }
}
