using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpperSection : PointSlot
{
    [SerializeField,Range(1,6)]
    int targetNum;

    public override void UpdateSlot(int[] _points)
    {
        point = 0;

        foreach (int i in _points)
        {
            if(targetNum == i)
            {
                point += i;
            }
        }
    }
}
