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

    //�ʱ�ȭ �ڽ��� ���ʰ� ������ ��, ����� ��
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
}
