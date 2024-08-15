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
            //slot.button.onClick.AddListener(CloseTurn);
            slot.GetComponent<Button>().onClick.AddListener(EndTurn);
            slot.GetComponent<RectTransform>().localScale = Vector3.zero;
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
            //pips[i] = dices[i].reroll();
            /*
            public int reroll(){
            if(���� �ֻ����� Keep�� ���¶��) return myNum;
            1 ~ 6 �߿� ������ ���� ��ȯ
            return Random.range(1,7);
            }
            */
        }

        RerollEvent?.Invoke(pips);
    }

    /// <summary>
    /// �� �޼���� �����κ��� ����
    /// </summary>
    /// <param name="_pips">�ֻ��� 5���� ���� ���� ����</param>
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
            ���� = 0 ���� �ʱ�ȭ
            }
            return ����;
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
