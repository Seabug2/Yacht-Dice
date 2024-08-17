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

    //초기화 자신의 차례가 시작할 때, 종료될 때
    public void InitSlot()
    {
        text.color = new Color(0, 0, 0);
        interactable = false;

        if (!IsSelected)
            CurrentScore = 0;
        
        text.text = CurrentScore.ToString();
    }

    //초기화 자신의 차례를 종료할 때
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
        //주사위 값을 받았을 때 계산한 점수가 0보다 크면 빨간색으로 표시해준다.
        if (CurrentScore > 0)
        {
            text.text = CurrentScore.ToString();
            text.color = new Color(255, 0, 0);
        }
    }

    /// <summary>
    /// int 값 하나를 매개변수로 가지는 경우,
    /// 플레이어가 점수칸에 점수를 확정한 시점
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
