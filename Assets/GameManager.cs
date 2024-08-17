using System;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    YachtPlayer player;

    [Header("User Info"), Space(10)]
    [SerializeField] Text nickName;
    [SerializeField] Text rate;

    public void InfoUISet(string _name, string _rate)
    {
        nickName.text = _name;
        rate.text = _rate;
    }

    [Header("점수판")]
    [SerializeField] PointSlot[] slots;
    [SerializeField] PointSlot sumSlot;
    [SerializeField] PointSlot bonusSlot;
    [SerializeField] PointSlot totalSlot;

    [Header("주사위"), Space(10)]
    [SerializeField] Die[] dice;
    public Die[] Dice => dice;

    /// <summary>
    /// 주사위를 굴리는 버튼
    /// </summary>
    [SerializeField] Button rerollButton;

    private void Start()
    {
        //점수칸을 누르면 자신의 차례를 마치고 상대방에게 차례를 넘긴다.
        foreach (PointSlot slot in slots)
        {
            //차례를 시작할 때 점수칸을 초기화한다.
            StartTurnEvent += slot.InitSlot;

            //주사위를 굴린 순간부터 점수칸을 선택할 수 있다.
            rerollButton.onClick.AddListener(() =>
            {
                slot.Button.interactable = true;
            });

            //주사위를 굴린 후에 점수칸을 갱신
            SlotsUpdateEvent += slot.UpdateScore;

            //점수칸을 선택하면 차례를 마침.
            slot.Button.onClick.AddListener(EndTurn);
        }

        //Reroll 버튼에 주사위를 굴리는 메서드 연결.
        rerollButton.onClick.AddListener(Reroll);
        //가장 처음엔 Reroll 버튼은 비활성화.
        rerollButton.interactable = false;

        foreach (Die die in dice)
        {
            //자신의 차례가 시작된 순간은 주사위를 Keep 할 수 없다.
            StartTurnEvent += die.DontTouchDice;
            //주사위를 굴린 후 부터 주사위를 Keep 할 수 있다.
            rerollButton.onClick.AddListener(die.IsKeepable);
        }
    }

    /// <summary>
    /// 자신의 차례가 시작될 때 실행될 이벤트
    /// </summary>
    public event Action StartTurnEvent;

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
        StartTurnEvent?.Invoke();
    }

    /// <summary>
    /// 주사위를 굴린 후 실행될 이벤트
    /// </summary>
    public event Action<int[]> RerollEvent;

    /// <summary>
    /// Reroll 버튼을 눌렀을 때 실행되는 메서드
    /// </summary>
    public void Reroll()
    {
        rerollChance--;
        rerollButton.interactable = (rerollChance > 0);

        //주사위 5개를 굴린 결과를 저장할 배열
        int[] pips = new int[5];

        for (int i = 0; i < 5; i++)
        {
            pips[i] = dice[i].Roll();
        }

        RerollEvent?.Invoke(pips);
    }

    public event Action<int[]> SlotsUpdateEvent;

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

        SlotsUpdateEvent?.Invoke(_pips);

        //foreach (PointSlot slot in slots)
        //{
        //    slot.UpdateScore(slot.CalculateScore(_pips));
        //}
    }

    /// <summary>
    /// 차례를 마칠 때 실행할 이벤트
    /// </summary>
    public event Action<int[]> EndTurnEvent;

    /// <summary>
    /// 점수칸을 누르면 자신의 차례를 마친다.
    /// </summary>
    public void EndTurn()
    {
        //더 이상 주사위를 굴릴 수 없다.
        rerollButton.interactable = false;

        //모든 점수칸 비활성화
        foreach (PointSlot slot in slots)
        {
            slot.InitSlot();
        }

        int[] points = new int[slots.Length];

        for (int i = 0; i < points.Length; i++)
        {
            points[i] = slots[i].CurrentScore;
        }

        EndTurnEvent?.Invoke(points);
    }
}
