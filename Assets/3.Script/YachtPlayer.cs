using UnityEngine;
using Mirror;

public class YachtPlayer : NetworkBehaviour
{
    [Header("������")]
    [SerializeField] ScoreBoard myScoreBoard; // ���� ������
    public YachtPlayer opponent;

    void Start()
    {
        if (isLocalPlayer)
        {
            gameObject.name = "Local Player";
            myScoreBoard = GameObject.Find("Local Player Score Board").GetComponent<ScoreBoard>();

            if (SQLManager.instance == null || SQLManager.instance.info == null)
            {
                Debug.Log("�α��� ����");
                return;
            }

            string name = SQLManager.instance.info.User_nickname;
            int win = SQLManager.instance.info.wins;
            int lose = SQLManager.instance.info.loses;
            string rate = $"{win + lose}�� {win}�� {lose}��";
            CmdProfile(name, rate);
        }
        else
        {
            gameObject.name = "Remote Player";
            myScoreBoard = GameObject.Find("Remote Player Score Board").GetComponent<ScoreBoard>();
        }

        myScoreBoard.RerollEvent += UpdateBoard;
        myScoreBoard.EndTurnEvent += EndTurn;

        foreach (var identity in NetworkClient.spawned.Values)
        {
            identity.TryGetComponent(out YachtPlayer yachtPlayer);

            if (yachtPlayer != null && yachtPlayer != this)
            {
                opponent = yachtPlayer;
                opponent.opponent = this;
                break;
            }
        }
    }
    [Client]
    public void CientProfile()
    {
        if (SQLManager.instance == null || SQLManager.instance.info == null)
        {
            Debug.Log("�α��� ����");
            return;
        }

        string name = SQLManager.instance.info.User_nickname;
        int win = SQLManager.instance.info.wins;
        int lose = SQLManager.instance.info.loses;
        string rate = $"{win + lose}�� {win}�� {lose}��";
        CmdProfile(name, rate);
    }

    [Command]
    public void CmdProfile(string name, string rate)
    {

        RpcProfile(name, rate);
    }

    [ClientRpc]
    void RpcProfile(string name, string rate)
    {
        myScoreBoard.InfoUISet(name, rate);
        //if (!isLocalPlayer)
        //{
        //    //opponent.myScoreBoard.NickName.text = "asdasfgagasdasgag";
        //    opponent.CliProf(opponent.myScoreBoard.NickName.text, opponent.myScoreBoard.Rate.text);
        //}
    }

    //[Client]
    //public void CliProf(string name, string rate)
    //{
    //    CmdProf(name, rate);
    //}
    //[Command]
    //public void CmdProf(string name, string rate)
    //{
    //    RpcProf(name, rate);
    //}
    //[ClientRpc]
    //public void RpcProf(string name, string rate)
    //{
    //    opponent.myScoreBoard.InfoUISet(name, rate);
    //}


    [Client]
    public void MyTurn()
    {
        if (isLocalPlayer)
            CmdMyTurn();
    }

    [Command]
    public void CmdMyTurn()
    {
        RpcMyTurn();
    }

    [ClientRpc]
    void RpcMyTurn()
    {
        if (isLocalPlayer)
            myScoreBoard.StartTurn();
    }

    [Client]
    public void UpdateBoard(int[] _pips)
    {
        if (isLocalPlayer)
            CmdUpdateBoard(_pips);
    }

    [Command]
    public void CmdUpdateBoard(int[] _pips)
    {
        RpcUpdateBoard(_pips);
    }

    [ClientRpc]
    void RpcUpdateBoard(int[] _pips)
    {
        myScoreBoard.BoardUpdate(_pips);
    }

    [Client]
    public void EndTurn(bool[] isSelected)
    {
        if (isLocalPlayer)
        {
            CmdEndTurn(isSelected);
        }
    }

    [Command]
    public void CmdEndTurn(bool[] isSelected)
    {
        RpcEndTurn(isSelected);
    }

    //RPC�� ȣ���� Command���� �ؾ���
    [ClientRpc]
    void RpcEndTurn(bool[] isSelected)
    {
        myScoreBoard.EndUpdate(isSelected);

        myScoreBoard.TurnCount--;

        if (myScoreBoard.TurnCount <= 0 && opponent.myScoreBoard.TurnCount <= 0)
        {
            if (isLocalPlayer)
                GameOver();
            return;
        }

        opponent.MyTurn();
    }

    [Client]
    void GameOver()
    {
        CmdGameOver();
    }

    [Command]
    void CmdGameOver()
    {
        RpcGameOver();
    }

    [ClientRpc]
    void RpcGameOver()
    {
        myScoreBoard.resultBoard.SetActive(true);
    }
}
