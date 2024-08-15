using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class ScoreBoard : MonoBehaviour
{
    YachtPlayer player;

    [Header("������")]
    [SerializeField] PointSlot[] slots;
    [SerializeField] PointSlot sumSlot;
    [SerializeField] PointSlot bonusSlot;
    [SerializeField] PointSlot totalSlot;

    [Header("�ֻ���"), Space(10)]
    [SerializeField] Dice[] dices;
    /// <summary>
    /// �ֻ����� ������ ��ư
    /// </summary>
    [SerializeField] Button rerollButton;

    private void Start()
    {
        //��� ����ĭ�� ��ư�� ���ʸ� ��ġ�� �޼��带 ����
        foreach (PointSlot slot in slots)
        {
            //slot.button.onClick.AddListener(CloseTurn);
            slot.GetComponent<Button>().onClick.AddListener(EndTurn);
        }
    }

    /// <summary>
    /// �ڽ��� ���ʰ� ���۵� �� ����� �̺�Ʈ
    /// </summary>
    public event Action StartTurnEvent;

    /// <summary>
    /// �ڽ��� ���ʸ��� �ֻ����� ���� ��ȸ�� 3���� �־�����.
    /// </summary>
    int rerollChance = 0;

    /// <summary>
    /// �ֻ��� ������ ��ư�� Ȱ��ȭ �ȴ�.
    /// �ֻ����� ���� ��ȸ�� 3�� ���´�.
    /// </summary>
    public void StartTurn()
    {
        rerollChance = 3;
        rerollButton.interactable = true;
        StartTurnEvent?.Invoke();
    }



    /// <summary>
    /// �ֻ����� ���� �� ����� �̺�Ʈ
    /// </summary>
    public event Action<int[]> RerollEvent;

    /// <summary>
    /// Reroll ��ư�� ������ �� ����Ǵ� �޼���
    /// </summary>
    public void Reroll()
    {
        rerollChance--;
        rerollButton.interactable = (rerollChance > 0);

        //�ֻ��� 5���� ���� ����� ������ �迭
        int[] pips = new int[5];

        for (int i = 0; i < 5; i++)
        {
            //pips[i] = dices[i].reroll();
            /*
            public int reroll(){
            if(���� �ֻ����� Keep�� ���¶��) return;
            1 ~ 6 �߿� ������ ���� ��ȯ
            return Random.range(1,7);
            }
            */
        }

        RerollEvent?.Invoke(pips);
    }

    /// <summary>
    /// �÷��̾ ���� �ֻ����� ���� �޾� �ڽ��� ����ĭ�� �ֻ��� �׸��� 
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
    /// ���ʸ� ��ĥ �� ������ �̺�Ʈ
    /// </summary>
    public event Action<int[]> EndTurnEvent;

    /// <summary>
    /// ����ĭ�� ������ Ȯ���ϸ� �ڽ��� ���ʸ� ��ģ��.
    /// ������ MyTurn�� �����ϵ��� �Ѵ�.
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

        //�ֻ��� 5���� ���� ����� ������ �迭
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
