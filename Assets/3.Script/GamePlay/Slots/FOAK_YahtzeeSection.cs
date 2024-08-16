using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FOAK_YahtzeeSection : PointSlot
{
    public int slotIndex; // 0 : Four of a kind || 1 : Yahtzee
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
        Debug.Log($"Card Check : {dice_count[0]} | {dice_count[1]} | {dice_count[2]} | {dice_count[3]} | {dice_count[4]} | {dice_count[5]}");

        for (int i = 0; i < 6; i++)
        {
            if (dice_count[i] >= 4 && slotIndex == 0)
            {
                Debug.Log("Four of a kind");
                for (int j = 0; j < 5; j++)
                {
                    sum += pips[j];
                }
            }
            if (dice_count[i] == 5 && slotIndex == 1)
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
