using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChanceSection : PointSlot
{
    /// <summary>
    /// 조건없이 주사위 5개의 눈을 합친 수치를 점수로
    /// </summary>
    /// <param name="_points"></param>
    public override void UpdateSlot(int[] _points)
    {
        foreach(int i in _points)
        {
            point += i;
        }
    }
}
