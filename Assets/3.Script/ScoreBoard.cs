using System;
using UnityEngine;
using UnityEngine.UI;

public class ScoreBoard : MonoBehaviour
{
    [Header("User Info"), Space(10)]
    [SerializeField] Text nickName;
    public Text NickName => nickName;

    [SerializeField] Text rate;
    public Text Rate => rate;

    public void InfoUISet(string name, string rate)
    {
        nickName.text = name;
        this.rate.text = rate;
    }

    [Header("결과창")]
    public GameObject resultBoard;

    private void Awake()
    {
        resultBoard.SetActive(false);
    }


    [Header("점수판")]
    [SerializeField] PointSlot[] slots;

    [Header("주사위"), Space(10)]
    [SerializeField] Die[] dice;
    public Die[] Dice => dice;

    /// <summary>
    /// 12번의 차례가 지나면 게임 종료
    /// </summary>
    public int TurnCount;// { get; private set; }

    /// <summary>
    /// 주사위를 굴리는 버튼
    /// </summary>
    [SerializeField] Button rerollButton;

    private void Start()
    {
        TurnCount = slots.Length;

        //점수칸을 누르면 자신의 차례를 마치고 상대방에게 차례를 넘긴다.
        foreach (PointSlot slot in slots)
        {
            //차례를 시작할 때 점수칸을 초기화한다.
            //StartTurnEvent += slot.InitSlot;

            //주사위를 굴린 순간부터 점수칸을 선택할 수 있다.
            rerollButton.onClick.AddListener(() =>
            {
                if (!slot.IsSelected)
                    slot.interactable = true;
            });

            //점수칸을 선택하면 차례를 마침.
            slot.OnClickEvent += () =>
            {
                rerollButton.interactable = false;
                EndTurn();
            };
        }

        //가장 처음엔 Reroll 버튼은 비활성화.
        rerollButton.interactable = false;
    }

    /// <summary>
    /// 자신의 차례마다 주사위를 굴릴 기회가 3번씩 주어진다.
    /// </summary>
    int rerollChance = 0;

    /// <summary>
    /// 자신의 차례가 되면 실행되는 메서드
    /// </summary>
    public void StartTurn()
    {
        //자신의 클라이언트 쪽에서만 처리되는 작업
        rerollChance = 3; //주사위를 굴릴 기회를 3번 받음
        rerollButton.interactable = true; //주사위 굴리기 버튼을 활성화
    }

    /// <summary>
    /// 주사위를 굴린 후 실행될 이벤트
    /// </summary>
    public event Action<int[]> RerollEvent;

    /// <summary>
    /// Reroll 버튼을 눌렀을 때 실행되는 메서드
    /// (인스펙터에서 직접 연결 해야함 / 중복 연결 방지)
    /// </summary>
    public void Reroll()
    {
        rerollChance--;

        //주사위 5개를 굴린 결과를 저장할 배열
        int[] pips = new int[5];

        for (int i = 0; i < 5; i++)
        {
            pips[i] = dice[i].Roll();
        }

        foreach (Die die in dice)
        {
            die.interactable = true;
        }

        if (rerollChance <= 0)
        {
            rerollButton.interactable = false;
            foreach (Die die in dice)
            {
                die.DontTouchDice();
            }
        }

        RerollEvent?.Invoke(pips);
    }

    //public event Action<int[]> SlotsUpdateEvent;

    /// <summary>
    /// 이 메서드는 서버로부터 실행
    /// </summary>
    /// <param name="_pips">주사위 5개를 굴려 얻은 숫자</param>
    public void BoardUpdate(int[] _pips)
    {
        for (int i = 0; i < 5; i++)
        {
            dice[i].UpdateDice(_pips[i]);
        }

        foreach (PointSlot slot in slots)
        {
            slot.UpdateScore(_pips);
        }
    }

    /// <summary>
    /// 차례를 마칠 때 실행할 이벤트
    /// </summary>
    public event Action<bool[]> EndTurnEvent;

    /// <summary>
    /// 점수칸을 누르면 자신의 차례를 마친다.
    /// </summary>
    public void EndTurn()
    {
        bool[] isSelected = new bool[slots.Length];

        for (int i = 0; i < slots.Length; i++)
        {
            isSelected[i] = slots[i].IsSelected;
        }

        foreach (Die die in dice)
        {
            die.DontTouchDice();
        }

        EndTurnEvent?.Invoke(isSelected);
    }

    public SubtotalSection subtotalSection;
    public BonusSection bonusSection;
    public TotalSection totalSlot;
    public void EndUpdate(bool[] isSelected)
    {
        for (int i = 0; i < slots.Length; i++)
        {
            //Debug.Log($"{i} : {isSelected[i]}");
            slots[i].InitSlot(isSelected[i]);
        }

        int totalCount = 0;

        //먼저 UpperSection 검사
        for (int i = 0; i < 6; i++)
        {
            totalCount += slots[i].CurrentScore;
        }
        subtotalSection.UpdateScore(totalCount);

        totalCount = 0; //재사용
        for (int i = 0; i < slots.Length; i++)
        {
            totalCount += slots[i].CurrentScore;
        }
        totalCount += bonusSection.CurrentScore;

        totalSlot.UpdateScore(totalCount);
    }
}
