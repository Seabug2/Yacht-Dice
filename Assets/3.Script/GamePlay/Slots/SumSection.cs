using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SumSection : PointSlot
{
    [SerializeField]
    int targetPoint = 63;

    private void Start()
    {
        point = 0;
    }

    public void SumPoint(int _fixedPoint)
    {
        point += _fixedPoint;
        if(point >= targetPoint)
        {
            BonusSection bonus = FindObjectOfType<BonusSection>();
        }
    }
}
