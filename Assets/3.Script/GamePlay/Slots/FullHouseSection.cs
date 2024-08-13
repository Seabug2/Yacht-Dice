using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class FullHouseSection : PointSlot
{
    public override void UpdateSlot(int[] _points)
    {
        //중복 제거를 위해 List로 변형
        List<int> list = _points.ToList<int>();
        //중복 요소 제거
        list = list.Distinct().ToList<int>();

        //중복 제거를 했는데 요소가 3개 이상이면 2, 3이 성립하지 않음
        if (list.Count > 2)
        {
            point = 0;
            return;
        }

        //요소가 2개인경우 1, 4가 아닌 경우에 2, 3이 성립
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

            //한 요소의 개수를 세어 2나 3이 나오면 풀하우스
            if(count == 2 || count == 3)
            {
                //2개와 3개인 값을 전부 더함
                point = list[0] * count + list[1] * (5 - count); 
            }
        }
        //중복 제거를 했는데 
        else
        {

        }
    }
}
