using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
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

    public void PopUp()
    {
        for (int i = 0; i < slots.Length; i++)
        {
            slots[i].GetComponent<RectTransform>().DOScale(1, .3f).SetDelay(.25f * i).SetEase(Ease.OutBack);
        }
    }

    [Header("점수판")]
    [SerializeField] PointSlot[] slots;
    [SerializeField] PointSlot sumSlot;
    [SerializeField] PointSlot bonusSlot;
    [SerializeField] PointSlot totalSlot;

    [Header("주사위"), Space(10)]
    [SerializeField] Dice[] dices;

    /// <summary>
    /// 주사위를 굴리는 버튼
    /// </summary>
    [SerializeField] Button rerollButton;

    private void Start()
    {
        //이벤트 추가
        foreach (PointSlot slot in slots)
        {
            //slot.button.onClick.AddListener(CloseTurn);
            slot.GetComponent<Button>().onClick.AddListener(EndTurn);
            slot.GetComponent<RectTransform>().localScale = Vector3.zero;
        }
        rerollButton.onClick.AddListener(Reroll);
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
            //pips[i] = dices[i].reroll();
            /*
            public int reroll(){
            if(현재 주사위가 Keep된 상태라면) return myNum;
            1 ~ 6 중에 랜덤한 수를 반환
            return Random.range(1,7);
            }
            */
        }

        RerollEvent?.Invoke(pips);
    }

    /// <summary>
    /// 이 메서드는 서버로부터 실행
    /// </summary>
    /// <param name="_pips">주사위 5개를 굴려 얻은 숫자</param>
    public void UpdateSlot(int[] _pips)
    {
        foreach (PointSlot slot in slots)
        {
            //slot.Update(_pips);
        }
        foreach (Dice dice in dices)
        {
            //dice.Update(_pips);
        }
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
        //점수칸을 누르면 점수칸에 점수가 저장되고 IsFixed가 true가 됨
        //주사위 굴리기 버튼이 비활성화 됨
        rerollButton.interactable = false;

        //모든 점수칸 비활성화
        foreach (PointSlot slot in slots)
        {
            //slot.ResetSlot();
            /*public void ResetSlot(){
            text = initial;
            myNum = 0;
            }*/
        }

        int[] points = new int[slots.Length];

        for (int i = 0; i < points.Length; i++)
        {
            //points[i] = slots[i].Read();
            /*
            int Read(){
            if(!isFixed){
            점수 = 0 으로 초기화
            }
            return 점수;
            }
            */
        }

        EndTurnEvent?.Invoke(points);
    }

    public void FixedSlot(int[] points)
    {
        for (int i = 0; i < slots.Length; i++)
        {
            //slots[i]. (points[i]);
        }
    }
}
