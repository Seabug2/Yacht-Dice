using System;
using UnityEngine;
using UnityEngine.UI;

public class ScoreBoard : MonoBehaviour
{
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

    [Header("�ֻ���"), Space(10)]
    [SerializeField] Die[] dice;
    public Die[] Dice => dice;

    /// <summary>
    /// 12���� ���ʰ� ������ ���� ����
    /// </summary>
    public int TurnCount { get; private set; }

    /// <summary>
    /// �ֻ����� ������ ��ư
    /// </summary>
    [SerializeField] Button rerollButton;
    public Button RerollButton => rerollButton;

    private void Start()
    {
        TurnCount = slots.Length;

        //����ĭ�� ������ �ڽ��� ���ʸ� ��ġ�� ���濡�� ���ʸ� �ѱ��.
        foreach (PointSlot slot in slots)
        {
            //���ʸ� ������ �� ����ĭ�� �ʱ�ȭ�Ѵ�.
            //StartTurnEvent += slot.InitSlot;

            //�ֻ����� ���� �������� ����ĭ�� ������ �� �ִ�.
            rerollButton.onClick.AddListener(() =>
            {
                if (!slot.IsSelected)
                    slot.interactable = true;
            });

            //����ĭ�� �����ϸ� ���ʸ� ��ħ.
            slot.OnClickEvent += () =>
            {
                rerollButton.interactable = false;
                Debug.Log("����");
                EndTurn();
            };
        }

        //���� ó���� Reroll ��ư�� ��Ȱ��ȭ.
        rerollButton.interactable = false;
    }

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
    }

    /// <summary>
    /// �ֻ����� ���� �� ����� �̺�Ʈ
    /// </summary>
    public event Action<int[]> RerollEvent;

    /// <summary>
    /// Reroll ��ư�� ������ �� ����Ǵ� �޼���
    /// (�ν����Ϳ��� ���� ���� �ؾ��� / �ߺ� ���� ����)
    /// </summary>
    public void Reroll()
    {
        rerollChance--;

        //�ֻ��� 5���� ���� ����� ������ �迭
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
    /// �� �޼���� �����κ��� ����
    /// </summary>
    /// <param name="_pips">�ֻ��� 5���� ���� ���� ����</param>
    public void BoardUpdate(int[] _pips)
    {
        for (int i = 0; i < 5; i++)
        {
            dice[i].UpdateDice(_pips[i]);
        }

        //SlotsUpdateEvent?.Invoke(_pips);

        foreach (PointSlot slot in slots)
        {
            slot.UpdateScore(_pips);
        }
    }

    /// <summary>
    /// ���ʸ� ��ĥ �� ������ �̺�Ʈ
    /// </summary>
    public event Action<bool[]> EndTurnEvent;

    /// <summary>
    /// ����ĭ�� ������ �ڽ��� ���ʸ� ��ģ��.
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
        TurnCount--;
        for (int i = 0; i < slots.Length; i++)
        {
            Debug.Log($"{i} : {isSelected[i]}");
            slots[i].InitSlot(isSelected[i]);
        }

        int totalCount = 0;

        //���� UpperSection �˻�
        for (int i = 0; i < 6; i++)
        {
            totalCount += slots[i].CurrentScore;
        }
        print(totalCount);
        subtotalSection.UpdateScore(totalCount);

        totalCount = 0; //����
        for (int i = 0; i < slots.Length; i++)
        {
            totalCount += slots[i].CurrentScore;
        }
        totalCount += bonusSection.CurrentScore;
        print(totalCount);

        totalSlot.UpdateScore(totalCount);
    }


    void Ending()
    {

    }
}
