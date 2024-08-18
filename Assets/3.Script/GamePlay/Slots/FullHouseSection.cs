using UnityEngine;

public class FullHouseSection : PointSlot
{
    public override int CalculateScore(int[] pips)
    {
        int sum = 0;
        int[] dice_count = new int[6];
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
