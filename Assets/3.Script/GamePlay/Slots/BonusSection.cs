using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BonusSection : PointSlot
{
    public void CalcBonus(int subtotal)
    {
        if (subtotal >= 63)
        {
            slot_currentScore = 35;
        }
    }
}
