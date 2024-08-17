using UnityEngine;
using Mirror;

public class YachtPlayer : NetworkBehaviour
{
    [Header("������")]
    [SerializeField]GameManager myManager; //���� ������
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
            myManager = GameObject.Find("Local Player Score Board").GetComponent<GameManager>();
        }
        else
        {
            name = "Remote Player";
            myManager = GameObject.Find("Remote Player Score Board").GetComponent<GameManager>();
        }

        Init();
    }

    void Init()
    {
        myManager.RerollEvent += CmdUpdateBoard;
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
    void RpcProfile(string name, string rate) {
        myManager.InfoUISet(name, rate);
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
            myManager.StartTurn();
            Debug.Log("���� ����");
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
        myManager.BoardUpdate(_pips);
    }

    [Command]
    public void CmdEndTurn()
    {
        RpcEndTurn();
    }

    [ClientRpc]
    void RpcEndTurn()
    {
        opponent.CmdMyTurn();
    }
}
