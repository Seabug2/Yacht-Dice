using UnityEngine;

public class TotalSection : PointSlot
{
    private void Start()
    {
        IsSelected = true;
    }
    public override void UpdateScore(int score)
    {
        CurrentScore = score;
        text.text = score.ToString();
    }
}
