using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChanceSection : PointSlot
{
    /// <summary>
    /// ���Ǿ��� �ֻ��� 5���� ���� ��ģ ��ġ�� ������
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
