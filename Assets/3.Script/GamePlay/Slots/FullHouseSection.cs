using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class FullHouseSection : PointSlot
{
    public override void UpdateSlot(int[] _points)
    {
        //�ߺ� ���Ÿ� ���� List�� ����
        List<int> list = _points.ToList<int>();
        //�ߺ� ��� ����
        list = list.Distinct().ToList<int>();

        //�ߺ� ���Ÿ� �ߴµ� ��Ұ� 3�� �̻��̸� 2, 3�� �������� ����
        if (list.Count > 2)
        {
            point = 0;
            return;
        }

        //��Ұ� 2���ΰ�� 1, 4�� �ƴ� ��쿡 2, 3�� ����
        if (list.Count == 2)
        {
            int count = 0;
            int checkNum = list[0];
            for (int i = 0; i < _points.Length; i++)
            {
                if (checkNum == _points[i])
                {
                    count++;
                }
            }

            //�� ����� ������ ���� 2�� 3�� ������ Ǯ�Ͽ콺
            if(count == 2 || count == 3)
            {
                //2���� 3���� ���� ���� ����
                point = list[0] * count + list[1] * (5 - count); 
            }
        }
        //�ߺ� ���Ÿ� �ߴµ� 
        else
        {

        }
    }
}
