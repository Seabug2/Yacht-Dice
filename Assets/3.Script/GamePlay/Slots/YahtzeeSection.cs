using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class YahtzeeSection : PointSlot
{
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
        Debug.Log($"Yahtzee Check : {dice_count[0]} | {dice_count[1]} | {dice_count[2]} | {dice_count[3]} | {dice_count[4]} | {dice_count[5]}");

        for (int i = 0; i < 6; i++)
        {
            if (dice_count[i] == 5)
            {
                Debug.Log("YAHTZEE");
                for (int j = 0; j < 5; j++)
                {
                    sum = 50;
                }
            }
        }

        return sum;
    }
}
