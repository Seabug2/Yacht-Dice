using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SubtotalSection : PointSlot
{
    public BonusSection bonus;

    public override void UpdateScore(int score)
    {
        text.color = new Color(0, 0, 0);
    }

    public void AddScore(int score)
    {
        CurrentScore += score;
        bonus.CalcBonus(CurrentScore);
    }
}
