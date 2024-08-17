using System;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
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
    public int turnCount { get; private set; }

    /// <summary>
    /// �ֻ����� ������ ��ư
    /// </summary>
    [SerializeField] Button rerollButton;
    public Button RerollButton => rerollButton;

    private void Start()
    {
        turnCount = 0;

        //����ĭ�� ������ �ڽ��� ���ʸ� ��ġ�� ���濡�� ���ʸ� �ѱ��.
        foreach (PointSlot slot in slots)
        {
            //���ʸ� ������ �� ����ĭ�� �ʱ�ȭ�Ѵ�.
            StartTurnEvent += slot.InitSlot;

            //�ֻ����� ���� �������� ����ĭ�� ������ �� �ִ�.
            rerollButton.onClick.AddListener(() =>
            {
                slot.Button.interactable = true;
            });

            //�ֻ����� ���� �Ŀ� ����ĭ�� ����
            SlotsUpdateEvent += slot.UpdateScore;

            //����ĭ�� �����ϸ� ���ʸ� ��ħ.
            slot.Button.onClick.AddListener(() => {
                rerollButton.interactable = false;
                turnCount++;
            });
        }

        //Reroll ��ư�� �ֻ����� ������ �޼��� ����.
        rerollButton.onClick.AddListener(Reroll);
        //���� ó���� Reroll ��ư�� ��Ȱ��ȭ.
        rerollButton.interactable = false;

        foreach (Die die in dice)
        {
            //�ڽ��� ���ʰ� ���۵� ������ �ֻ����� Keep �� �� ����.
            StartTurnEvent += die.DontTouchDice;
            //�ֻ����� ���� �� ���� �ֻ����� Keep �� �� �ִ�.
            rerollButton.onClick.AddListener(die.IsKeepable);
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
            pips[i] = dice[i].Roll();
        }

        RerollEvent?.Invoke(pips);
    }

    public event Action<int[]> SlotsUpdateEvent;

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

        SlotsUpdateEvent?.Invoke(_pips);

        //foreach (PointSlot slot in slots)
        //{
        //    slot.UpdateScore(slot.CalculateScore(_pips));
        //}
    }

    /// <summary>
    /// ���ʸ� ��ĥ �� ������ �̺�Ʈ
    /// </summary>
    public event Action EndTurnEvent;

    /// <summary>
    /// ����ĭ�� ������ �ڽ��� ���ʸ� ��ģ��.
    /// </summary>
    public void EndTurn()
    {
        //�� �̻� �ֻ����� ���� �� ����.


        EndTurnEvent?.Invoke();
    }
}
