using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class StraightSection : PointSlot
{
    [SerializeField, Range(4, 5)]
    int straightSize;
    [SerializeField]
    int getPoint;

    public override void UpdateSlot(int[] _points)
    {
        //sort�� ����ϱ� ���� List�� ��ȯ
        List<int> sortList = _points.ToList<int>();
        //������������ ����
        sortList.Sort();

        int count = 0;

        for(int i = 0; i < sortList.Count - 1; i++)
        {
            if(sortList[i] + 1 == sortList[i + 1])
            {
                count++;
            }
            else
            {
                count = 0;
            }
        }

        if(count >= straightSize - 1)
        {
            point = getPoint;
        }
        else
        {
            point = 0;
        }
    }
}
