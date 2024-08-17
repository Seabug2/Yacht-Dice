using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BonusSection : PointSlot
{
    public override void UpdateScore(int score)
    {
        text.color = new Color(0, 0, 0);
    }

    public void CalcBonus(int subtotal)
    {
        if (subtotal >= 63)
        {
            CurrentScore = 35;
        }
    }
}
