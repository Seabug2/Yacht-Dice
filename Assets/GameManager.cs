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
        //�̺�Ʈ �߰�
        foreach (PointSlot slot in slots)
        {
            slot.slot_btn.onClick.AddListener(EndTurn);
        }

        rerollButton.onClick.AddListener(Reroll);
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
    /// �ڽ��� ���ʰ� �Ǹ� ����Ǵ� �޼���
    /// </summary>
    public void StartTurn()
    {
        //�ڽ��� Ŭ���̾�Ʈ �ʿ����� ó���Ǵ� �۾�
        rerollChance = 3; //�ֻ����� ���� ��ȸ�� 3�� ����
        rerollButton.interactable = true; //�ֻ��� ������ ��ư�� Ȱ��ȭ
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
            pips[i] = dices[i].dice_num();
        }

        RerollEvent?.Invoke(pips);
    }

    /// <summary>
    /// �� �޼���� �����κ��� ����
    /// </summary>
    /// <param name="_pips">�ֻ��� 5���� ���� ���� ����</param>
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
    /// ���ʸ� ��ĥ �� ������ �̺�Ʈ
    /// </summary>
    public event Action<int[]> EndTurnEvent;

    /// <summary>
    /// ����ĭ�� ������ �ڽ��� ���ʸ� ��ģ��.
    /// </summary>
    public void EndTurn()
    {
        //����ĭ�� ������ ����ĭ�� ������ ����ǰ� IsFixed�� true�� ��
        //�ֻ��� ������ ��ư�� ��Ȱ��ȭ ��
        rerollButton.interactable = false;

        //��� ����ĭ ��Ȱ��ȭ
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
