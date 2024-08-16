using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChanceSection : PointSlot
{
    public override int UpdateScore(int[] pips)
    {
        int sum = 0;

        for (int i = 0; i < 5; i++)
        {
            sum += pips[i];
        }

        return sum;
    }
}
