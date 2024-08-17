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

        CurrentScore = CalculateScore(pips);

        //�ֻ��� ���� �޾��� �� ����� ������ 0���� ũ�� ���������� ǥ�����ش�.
        text.text = CurrentScore.ToString();
        
        if (CurrentScore > 0)
        {
            text.color = new Color(1, 0, 0);
        }
        else
        {
            text.color = new Color(0, 0, 0);
        }
    }

    /// <summary>
    /// int �� �ϳ��� �Ű������� ������ ���,
    /// �÷��̾ ����ĭ�� ������ Ȯ���� ����
    /// </summary>
    /// <param name="score"></param>
    virtual public void UpdateScore(int score){}

    public event Action OnClickEvent;

    public void OnPointerClick(PointerEventData eventData)
    {
        //��Ȱ��ȭ �Ǿ��ų� �̹� ���õ� ����ĭ�̸� Ŭ������ �ʴ´�.
        if (!interactable || IsSelected) return;

        IsSelected = true;
        text.transform.DOPunchScale(Vector3.up, 1f);

        OnClickEvent?.Invoke();
    }
}
