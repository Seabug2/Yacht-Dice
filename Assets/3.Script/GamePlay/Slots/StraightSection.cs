using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class StraightSection : PointSlot
{
    public int straightIndex; // 0 : Small Straight | 1 : Large Straight

    public override int UpdateScore(int[] pips)
    {
        int sum = 0;

        for (int i = 0; i < 6; i++)
        {
            dice_count[i] = 0;
        }

        for (int i = 0; i < 5; i++)
        {
            dice_count[pips[i] - 1] += 1;
        }
        Debug.Log($"Straight Check : {dice_count[0]} | {dice_count[1]} | {dice_count[2]} | {dice_count[3]} | {dice_count[4]} | {dice_count[5]}");

        if (straightIndex == 0)
        {
            bool checkss = false;

            if (dice_count[0] >= 1 && dice_count[1] >= 1 && dice_count[2] >= 1 && dice_count[3] >= 1)
            {
                checkss = true;
            }
            if (dice_count[1] >= 1 && dice_count[2] >= 1 && dice_count[3] >= 1 && dice_count[4] >= 1)
            {
                checkss = true;
            }
            if (dice_count[2] >= 1 && dice_count[3] >= 1 && dice_count[4] >= 1 && dice_count[5] >= 1)
            {
                checkss = true;
            }

            Debug.Log($"Small Straight Check : {checkss}");
            if (checkss)
            {
                sum = 15;
            }
        }
        if (straightIndex == 1)
        {
            bool checksl = false;

            if (dice_count[0] >= 1 && dice_count[1] >= 1 && dice_count[2] >= 1 && dice_count[3] >= 1 && dice_count[4] >= 1)
            {
                checksl = true;
            }
            if (dice_count[1] >= 1 && dice_count[2] >= 1 && dice_count[3] >= 1 && dice_count[4] >= 1 && dice_count[5] >= 1)
            {
                checksl = true;
            }

            Debug.Log($"Large Straight Check : {checksl}");
            if (checksl)
            {
                sum = 30;
            }
        }

        return sum;
    }
}
