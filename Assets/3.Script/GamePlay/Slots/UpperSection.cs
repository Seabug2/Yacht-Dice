using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpperSection : PointSlot
{
    public int slotIndex; // 0 ~ 5 (Aces = 0, Deuces = 1, Threes = 2, Fours = 3, Fives = 4, Sixes = 5)

    public SubtotalSection subtotal;
    public override int CalculateScore(int[] pips)
    {
        int sum = 0;

        if (!isSelected)
        {
            for (int i = 0; i < 5; i++)
            {
                if (pips[i] == slotIndex + 1)
                {
                    sum += slotIndex + 1;
                }
            }
        }

        subtotal.AddScore(sum);

        return sum;
    }
}
