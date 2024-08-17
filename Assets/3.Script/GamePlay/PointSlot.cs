using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using UnityEngine.EventSystems;
using System;

public class PointSlot : MonoBehaviour, IPointerClickHandler
{
    public bool IsSelected { get; protected set; }

    public int CurrentScore { get; protected set; }

    [SerializeField]
    string initTxt;

    protected Text text;
    public bool interactable;

    private void Awake()
    {
        text = GetComponentInChildren<Text>();
        IsSelected = false;

        InitSlot();
    }

    //�ʱ�ȭ �ڽ��� ���ʰ� ������ ��, ����� ��
    public void InitSlot()
    {
        text.color = new Color(0, 0, 0);
        interactable = false;

        if (IsSelected)
        {
            return;
        }

        CurrentScore = 0;
        text.text = initTxt;
    }

    //�ʱ�ȭ �ڽ��� ���ʸ� ������ ��
    public void InitSlot(bool IsSelected)
    {
        this.IsSelected = IsSelected;
        text.color = new Color(0, 0, 0);
        interactable = false;

        if (IsSelected)
        {
            return;
        }

        CurrentScore = 0;
        text.text = initTxt;
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

    public void ScoreSelect_btn()
    {
        IsSelected = true;
        text.transform.DOPunchScale(Vector3.up, 1f);
    }

    public event Action OnClickEvent;

    public void OnPointerClick(PointerEventData eventData)
    {
        if (!interactable) return;
        
        ScoreSelect_btn();
        OnClickEvent?.Invoke();
    }
}
