using UnityEngine;
using Mirror;

public class YachtPlayer : NetworkBehaviour
{
    [Header("점수판")]
    [SerializeField] ScoreBoard myScoreBoard; //나의 점수판
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
        // 네트워크에 연결된 모든 플레이어 객체를 검색
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
            Debug.Log("로그인 없음");
            return;
        }

        //SQLManager의 info 정보에서 닉네임과 전적을 가져온다.
        string name = SQLManager.instance.info.User_nickname;
        int win = SQLManager.instance.info.wins;
        int lose = SQLManager.instance.info.loses;
        string rate = $"{win + lose}전 {win}승 {lose}패";
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
        //자신의 차례를 시작하는 건 상대쪽 클라이언트에서 실행되는 CmdMyTurn() 이므로
        //모든 클라이언트에 존재하는 자신의 YachtPlayer 객체 중에
        //자신의 클라이언트에 존재하는 YachtPlayer만 myManager.StartTurn()를 실행할 수 있도록 한다.
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
    /// 나온 결과 값에 따라 주사위와 점수판을 갱신합니다.
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

        //클라이언트에서 서버의 Command 받은 identity 객체 중에 로컬 플레이어가 아닌 플레이어의 상대 플레이어는 클라이언트의 로컬 플레이어이기 때문에 CmdMyTurn()를 실행해야한다.
        if (!isLocalPlayer)
        {
            Opponent.CmdMyTurn();
        }
    }
}
