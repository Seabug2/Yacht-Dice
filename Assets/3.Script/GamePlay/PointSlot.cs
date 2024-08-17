using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class PointSlot : MonoBehaviour
{
    public bool IsSelected { get; protected set; }

    public int CurrentScore { get; protected set; }

    [SerializeField]
    string initTxt;

    Button button;
    public Button Button => button;

    protected Text text;

    private void Awake()
    {
        button = GetComponent<Button>();
        button.onClick.AddListener( ScoreSelect_btn);

        text = GetComponentInChildren<Text>();
        IsSelected = false;

        InitSlot();
    }

    //초기화 자신의 차례가 시작할 때, 종료될 때
    public void InitSlot()
    {
        text.color = new Color(0, 0, 0);

        if (IsSelected)
        {
            return;
        }

        CurrentScore = 0;
        text.text = initTxt;
        button.interactable = false;
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

    public void ScoreSelect_btn()
    {
        IsSelected = true;
        text.transform.DOPunchScale(Vector3.up, 1f);
    }
}
