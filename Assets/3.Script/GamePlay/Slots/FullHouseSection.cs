using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class FullHouseSection : PointSlot
{
    public override int UpdateScore(int[] pips)
    {
        int sum = 0;
        bool trips = false;
        bool pair = false;

        for (int i = 0; i < 6; i++)
        {
            dice_count[i] = 0;
        }

        for (int i = 0; i < 5; i++)
        {
            dice_count[pips[i] - 1] += 1;
        }

        for (int i = 0; i < 6; i++)
        {
            if (dice_count[i] == 3)
            {
                trips = true;
            }
            if (dice_count[i] == 2)
            {
                pair = true;
            }
            if (dice_count[i] == 5)
            {
                trips = true;
                pair = true;
            }
        }

        if (trips && pair)
        {
            Debug.Log("FULL HOUSE!");
            for (int i = 0; i < 5; i++)
            {
                sum += pips[i];
            }
        }

        return sum;
    }
}
