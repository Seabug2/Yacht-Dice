using UnityEngine;
using Mirror;

public class YachtPlayer : NetworkBehaviour
{
    [Header("점수판")]
    [SerializeField] ScoreBoard myScoreBoard; // 나의 점수판
    public YachtPlayer opponent = null;

    public override void OnStartLocalPlayer()
    {
        base.OnStartLocalPlayer();
        CmdFindOpponent();
    }

    [Command]
    void CmdFindOpponent()
    {
        foreach (var identity in NetworkClient.spawned.Values)
        {
            identity.TryGetComponent(out YachtPlayer yachtPlayer);

            if (yachtPlayer != null && yachtPlayer != this)
            {
                opponent = yachtPlayer;
                opponent.opponent = this;
                //yachtPlayer.RpcSetOpponent(this.netIdentity); // 상대방에게 이 클라이언트를 설정하도록 명령
                break;
            }
        }
    }

    [ClientRpc]
    void RpcSetOpponent(NetworkIdentity opponentIdentity)
    {
        opponent = opponentIdentity.GetComponent<YachtPlayer>();
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
        myScoreBoard.RerollEvent += UpdateBoard;
        myScoreBoard.EndTurnEvent += EndTurn;

        if (SQLManager.instance == null || SQLManager.instance.info == null)
        {
            Debug.Log("로그인 없음");
            return;
        }

        string name = SQLManager.instance.info.User_nickname;
        int win = SQLManager.instance.info.wins;
        int lose = SQLManager.instance.info.loses;
        string rate = $"{win + lose}전 {win}승 {lose}패";
        Profile(name, rate);
    }

    [Client]
    void Profile(string name, string rate)
    {
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
        for (int i = 0; i < _pips.Length; i++)
        {
            print(_pips[i]);
        }
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

        if (myScoreBoard.TurnCount <= 0 && opponent.myScoreBoard.TurnCount <= 0)
        {
            myScoreBoard.resultBoard.SetActive(true);
            return;
        }

        // 서버에서 상대방의 차례를 시작하도록 명령
        if (opponent != null)
        {
            opponent.RpcMyTurn();
        }
    }

    //RPC의 호출은 Command에서 해야함
    [ClientRpc]
    void RpcEndTurn(bool[] isSelected)
    {
        myScoreBoard.EndUpdate(isSelected);
    }

    [ClientRpc]
    void GameOver()
    {
        myScoreBoard.resultBoard.SetActive(true);
    }
}
