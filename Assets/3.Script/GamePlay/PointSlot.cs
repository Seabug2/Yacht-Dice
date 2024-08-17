using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using DG.Tweening;

public class PointSlot : MonoBehaviour, IPointerClickHandler
{
    public bool IsSelected { get; protected set; }

    public int CurrentScore { get; protected set; }

    protected Text text;
    public bool interactable;

    private void Awake()
    {
        text = GetComponentInChildren<Text>();
    }

    private void Start()
    {
        InitSlot(false);
    }

    //�ʱ�ȭ �ڽ��� ���ʰ� ������ ��, ����� ��
    public void InitSlot()
    {
        text.color = new Color(0, 0, 0);
        interactable = false;

        if (!IsSelected)
            CurrentScore = 0;
        
        text.text = CurrentScore.ToString();
    }

    //�ʱ�ȭ �ڽ��� ���ʸ� ������ ��
    public void InitSlot(bool IsSelected)
    {
        this.IsSelected = IsSelected;
        text.color = new Color(0, 0, 0);
        interactable = false;

        if (!IsSelected)
            CurrentScore = 0;

        text.text = CurrentScore.ToString();
    }

    virtual public int CalculateScore(int[] pips)
    {
        return CurrentScore;
    }

    virtual public void UpdateScore(int[] pips)
    {
        if (IsSelected) return;

        int CurrentScore = CalculateScore(pips);
        //�ֻ��� ���� �޾��� �� ����� ������ 0���� ũ�� ���������� ǥ�����ش�.
        if (CurrentScore > 0)
        {
            text.text = CurrentScore.ToString();
            text.color = new Color(255, 0, 0);
        }
    }

    /// <summary>
    /// int �� �ϳ��� �Ű������� ������ ���,
    /// �÷��̾ ����ĭ�� ������ Ȯ���� ����
    /// </summary>
    /// <param name="score"></param>
    virtual public void UpdateScore(int score)
    {
        if (score > 0)
        {
            text.text = score.ToString();
            text.color = new Color(255, 0, 0);
        }
    }

    public event Action OnClickEvent;

    public void OnPointerClick(PointerEventData eventData)
    {
        if (!interactable || IsSelected) return;

        IsSelected = true;
        text.transform.DOPunchScale(Vector3.up, 1f);
        OnClickEvent?.Invoke();
    }
}
