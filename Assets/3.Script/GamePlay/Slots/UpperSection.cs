using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpperSection : PointSlot
{
    [Range(1,7)]
    public int slotIndex;

    public override int CalculateScore(int[] pips)
    {
        int sum = 0;

        if (!IsSelected)
        {
            for (int i = 0; i < 5; i++)
            {
                if (pips[i] == slotIndex)
                {
                    sum += slotIndex;
                }
            }
        }
        return sum;
    }
}
