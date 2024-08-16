using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SubtotalSection : PointSlot
{
    public BonusSection bonus;

    public override void UpdateScore(int score)
    {
        slot_txt.color = new Color(0, 0, 0);
    }

    public void AddScore(int score)
    {
        slot_currentScore += score;
        bonus.CalcBonus(slot_currentScore);
    }
}
