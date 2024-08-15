using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Mirror;
using DG.Tweening;

public class YachtPlayer : NetworkBehaviour
{
    [Header("������")]
    [SerializeField] GameManager myManager; //���� ������

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
        //SQLManager�� info �������� �г��Ӱ� ������ �����´�.
        string name = SQLManager.instance.info.User_nickname;
        int win = SQLManager.instance.info.wins;
        int lose = SQLManager.instance.info.loses;
        string rate = $"{win + lose}�� {win}�� {lose}��";
        //CmdStartGame(name, rate);

        //�̺�Ʈ�� �����ϴ� �۾�
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
        //�ڽ��� ���ʸ� �����ϴ� �� ����� Ŭ���̾�Ʈ���� ����Ǵ� CmdMyTurn() �̹Ƿ�
        //��� Ŭ���̾�Ʈ�� �����ϴ� �ڽ��� YachtPlayer ��ü �߿�
        //�ڽ��� Ŭ���̾�Ʈ�� �����ϴ� YachtPlayer�� myManager.StartTurn()�� ������ �� �ֵ��� �Ѵ�.
        if (!isLocalPlayer) return;

        myManager.StartTurn();
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
