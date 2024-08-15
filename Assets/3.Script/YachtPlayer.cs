using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Mirror;
using DG.Tweening;

public class YachtPlayer : NetworkBehaviour
{
    [Header("User Info")]
    [SerializeField] Text nickName;
    [SerializeField] Text rate;

    [Header("점수판"), Space(10)]
    [SerializeField] ScoreBoard myScoreBoard; //나의 점수판
    /*
     * bool isFixed
     * text 표기될 점수 텍스트
     * Fixed() {
     * 버튼에 추가할 메서드
     * 버튼을 누르면 isFixed가 true가 된다.
     * }
     * UpdateSlot() {
     * isFixed == true 라면 실행되지 않는다.
     * 버튼.활성화 = !isFixed;
     * }
     * 
     */

    YachtPlayer opponent = null;

    private void Awake()
    {

    }

    void Start()
    {
        if (isLocalPlayer)
        {
            name = "Local Player";
            myScoreBoard = GameObject.Find("Local Player Score Board").GetComponent<ScoreBoard>();
        }
        else
        {
            name = "Remote Player";
            myScoreBoard = GameObject.Find("Remote Player Score Board").GetComponent<ScoreBoard>();
        }
        //Init();
    }

    void Init()
    {
        //SQLManager의 info 정보에서 닉네임과 전적을 가져온다.
        nickName.text = SQLManager.instance.info.User_nickname;
        int win = SQLManager.instance.info.wins;
        int lose = SQLManager.instance.info.loses;
        rate.text = $"{win + lose}전 {win}승 {lose}패";

        //이벤트를 연결하는 작업
        myScoreBoard.StartTurnEvent += () => { 
        
        };

        myScoreBoard.RerollEvent += CmdUpdateBoard;

        myScoreBoard.EndTurnEvent += CmdEndTurn;
    }


    [Command]
    public void CmdMyTurn()
    {
        RpcMyTurn();
    }

    [ClientRpc]
    public void RpcMyTurn()
    {
        if (!isLocalPlayer) return;

        myScoreBoard.StartTurn();
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
        myScoreBoard.UpdateSlot(_pips);
    }


    [Command]
    public void CmdEndTurn(int[] _points)
    {
        RpcEndTurn(_points);
    }

    [ClientRpc]
    public void RpcEndTurn(int[] _points)
    {
        myScoreBoard.FixedSlot(_points);
        opponent.CmdMyTurn();
    }
}
