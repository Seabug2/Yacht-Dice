using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class StraightSection : PointSlot
{
    public int straightIndex; // 0 : Small Straight | 1 : Large Straight

    public override int CalculateScore(int[] pips)
    {
        int sum = 0;
        int[] dice_count = new int[6];

        for (int i = 0; i < 6; i++)
        {
            dice_count[i] = 0;
        }

        for (int i = 0; i < 5; i++)
        {
            dice_count[pips[i] - 1] += 1;
        }
        Debug.Log($"Straight Check : {dice_count[0]} | {dice_count[1]} | {dice_count[2]} | {dice_count[3]} | {dice_count[4]} | {dice_count[5]}");

        int straightCount = 0;
        for (int i = 0; i < 5; i++)
        {
            if (dice_count[i] >= 1 && dice_count[i + 1] >= 1)
            {
                straightCount += 1;
            }
        }
        Debug.Log($"Straight Check : {straightCount}");

        if (straightCount >= 3 && straightIndex == 0)
        {
            Debug.Log("Small Straight");
            sum = 15;
        }
        if (straightCount >= 4 && straightIndex == 1)
        {
            Debug.Log("Large Straight");
            sum = 30;
        }

        return sum;
    }
}
