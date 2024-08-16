using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SubtotalSection : PointSlot
{
    public BonusSection bonus;
    public void AddScore(int score)
    {
        slot_currentScore += score;
        bonus.CalcBonus(slot_currentScore);
    }
}
