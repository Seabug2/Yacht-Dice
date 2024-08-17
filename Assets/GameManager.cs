using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.Mathematics;
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
            slot.slot_btn.onClick.AddListener(EndTurn);
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
            pips[i] = dices[i].dice_num();
        }

        RerollEvent?.Invoke(pips);
    }

    /// <summary>
    /// 이 메서드는 서버로부터 실행
    /// </summary>
    /// <param name="_pips">주사위 5개를 굴려 얻은 숫자</param>
    public void UpdateSlot(int[] _pips)
    {
        for(int i = 0; i < 5; i ++)
        {
            dices[i].UpdateDice(_pips[i]);
        }
        foreach (PointSlot slot in slots)
        {
            slot.UpdateScore(slot.CalculateScore(_pips));
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
            slot.InitSlot();
        }

        int[] points = new int[slots.Length];

        for (int i = 0; i < points.Length; i++)
        {
            points[i] = slots[i].slot_currentScore;
        }

        EndTurnEvent?.Invoke(points);
    }

    public void FixedSlot(int[] points)
    {
        for (int i = 0; i < slots.Length; i++)
        {
            slots[i].slot_txt.text = points[i].ToString();
        }
    }
}
