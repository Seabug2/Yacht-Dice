using UnityEngine;
using Mirror;

public class YachtPlayer : NetworkBehaviour
{
    [Header("������")]
    [SerializeField] ScoreBoard myScoreBoard; //���� ������
    YachtPlayer opponent = null;

    public YachtPlayer Opponent
    {
        get
        {
            if (opponent == null)
            {
                opponent = FindOpponent();
            }
            return opponent;
        }
    }

    YachtPlayer FindOpponent()
    {
        // ��Ʈ��ũ�� ����� ��� �÷��̾� ��ü�� �˻�
        foreach (var identity in NetworkClient.spawned.Values)
        {
            if (identity != null && identity.isLocalPlayer == false)
            {
                return identity.GetComponent<YachtPlayer>();
            }
        }

        Debug.LogWarning("Opponent not found!");
        return null;
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

        Init();
    }

    void Init()
    {
        myScoreBoard.RerollEvent += CmdUpdateBoard;
        //myManager.EndTurnEvent += CmdEndTurn;

        if (SQLManager.instance == null || SQLManager.instance.info == null)
        {
            Debug.Log("�α��� ����");
            return;
        }

        //SQLManager�� info �������� �г��Ӱ� ������ �����´�.
        string name = SQLManager.instance.info.User_nickname;
        int win = SQLManager.instance.info.wins;
        int lose = SQLManager.instance.info.loses;
        string rate = $"{win + lose}�� {win}�� {lose}��";
        CmdProfile(name, rate);
    }


    [Command]
    void CmdProfile(string name, string rate)
    {
        RpcProfile(name, rate);
    }

    [ClientRpc]
    void RpcProfile(string name, string rate)
    {
        myScoreBoard.InfoUISet(name, rate);
    }


    [Command]
    public void CmdMyTurn()
    {
        RpcMyTurn();
    }

    [ClientRpc]
    void RpcMyTurn()
    {
        //�ڽ��� ���ʸ� �����ϴ� �� ����� Ŭ���̾�Ʈ���� ����Ǵ� CmdMyTurn() �̹Ƿ�
        //��� Ŭ���̾�Ʈ�� �����ϴ� �ڽ��� YachtPlayer ��ü �߿�
        //�ڽ��� Ŭ���̾�Ʈ�� �����ϴ� YachtPlayer�� myManager.StartTurn()�� ������ �� �ֵ��� �Ѵ�.
        if (isLocalPlayer)
        {
            myScoreBoard.StartTurn();
        }
    }

    [Command]
    public void CmdUpdateBoard(int[] _pips)
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
        myScoreBoard.BoardUpdate(_pips);
    }

    [Command]
    public void CmdEndTurn(bool[] isSelected)
    {
        RpcEndTurn(isSelected);
    }

    [ClientRpc]
    void RpcEndTurn(bool[] isSelected)
    {
        myScoreBoard.EndTurn(isSelected);

        //Ŭ���̾�Ʈ���� ������ Command ���� identity ��ü �߿� ���� �÷��̾ �ƴ� �÷��̾��� ��� �÷��̾�� Ŭ���̾�Ʈ�� ���� �÷��̾��̱� ������ CmdMyTurn()�� �����ؾ��Ѵ�.
        if (!isLocalPlayer)
        {
            Opponent.CmdMyTurn();
        }
    }
}
