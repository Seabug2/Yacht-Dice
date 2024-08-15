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

    [Header("������"), Space(10)]
    [SerializeField] ScoreBoard myScoreBoard; //���� ������
    /*
     * bool isFixed
     * text ǥ��� ���� �ؽ�Ʈ
     * Fixed() {
     * ��ư�� �߰��� �޼���
     * ��ư�� ������ isFixed�� true�� �ȴ�.
     * }
     * UpdateSlot() {
     * isFixed == true ��� ������� �ʴ´�.
     * ��ư.Ȱ��ȭ = !isFixed;
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
        //SQLManager�� info �������� �г��Ӱ� ������ �����´�.
        nickName.text = SQLManager.instance.info.User_nickname;
        int win = SQLManager.instance.info.wins;
        int lose = SQLManager.instance.info.loses;
        rate.text = $"{win + lose}�� {win}�� {lose}��";

        //�̺�Ʈ�� �����ϴ� �۾�
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
    /// ���� ��� ���� ���� �ֻ����� �������� �����մϴ�.
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
