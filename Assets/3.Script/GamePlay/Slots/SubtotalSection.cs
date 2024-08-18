using UnityEngine;
using DG.Tweening;

public class SubtotalSection : PointSlot
{
    public BonusSection bonus;
    [SerializeField] int targetPoint = 63;
    [SerializeField] int bonusPoint = 35;

    private void Start()
    {
        IsSelected = true;
    }

    public override void UpdateScore(int score)
    {
        text.color = new Color(0, 0, 0);
        CurrentScore = score;
        text.text = $"{CurrentScore} / {targetPoint}";
        if (CurrentScore >= targetPoint)
        {
            text.text = "Success!";
            text.GetComponent<RectTransform>().DOPunchScale(Vector3.up, 1f);

            bonus.UpdateScore(bonusPoint);
        }
    }
}
