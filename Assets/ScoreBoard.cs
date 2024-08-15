using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class ScoreBoard : MonoBehaviour
{
    YachtPlayer player;

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
        //모든 점수칸의 버튼에 차례를 마치는 메서드를 연결
        foreach (PointSlot slot in slots)
        {
            //slot.button.onClick.AddListener(CloseTurn);
            slot.GetComponent<Button>().onClick.AddListener(EndTurn);
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
    /// 주사위 굴리기 버튼이 활성화 된다.
    /// 주사위를 굴릴 기회를 3번 갖는다.
    /// </summary>
    public void StartTurn()
    {
        rerollChance = 3;
        rerollButton.interactable = true;
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
            if(현재 주사위가 Keep된 상태라면) return;
            1 ~ 6 중에 랜덤한 수를 반환
            return Random.range(1,7);
            }
            */
        }

        RerollEvent?.Invoke(pips);
    }

    /// <summary>
    /// 플레이어가 굴린 주사위의 값을 받아 자신의 점수칸과 주사위 그림을 
    /// </summary>
    /// <param name="_pips"></param>
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
    /// 점수칸에 점수를 확정하면 자신의 차례를 마친다.
    /// 상대방이 MyTurn을 실행하도록 한다.
    /// </summary>
    public void EndTurn()
    {
        rerollButton.interactable = false;

        foreach (PointSlot slot in slots)
        {
            //slot.ResetSlot();
            /*
            public void ResetSlot(){
            text = initial;
            myNum = 0;
            }
            */
        }

        //주사위 5개를 굴린 결과를 저장할 배열
        int[] points = new int[slots.Length];

        for (int i = 0; i < points.Length; i++)
        {
            points[i] = slots.Length;
        }

        EndTurnEvent?.Invoke(points);
    }

    public void FixedSlot(int[] points)
    {
        Debug.Log(points.ToString());
    }
}
