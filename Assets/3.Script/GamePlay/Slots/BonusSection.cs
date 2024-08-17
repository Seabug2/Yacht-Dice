using UnityEngine;
using DG.Tweening;

public class BonusSection : PointSlot
{
    private void Start()
    {
        IsSelected = true;
    }

    public override void UpdateScore(int score)
    {
        if(CurrentScore == 0)
            text.GetComponent<RectTransform>().DOPunchScale(Vector3.up, 1f);

        text.color = new Color(1, 0, 0);
        CurrentScore = score;
        text.text = CurrentScore.ToString();
    }
}
