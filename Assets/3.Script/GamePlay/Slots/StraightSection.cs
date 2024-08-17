using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class StraightSection : PointSlot
{
    [SerializeField]
    int checkSize = 0;

    int[,] ints = {
        { 4, 5},  // 첫 번째 항: 연속된 숫자의 기준 (4: Small Straight, 5: Large Straight)
        { 15, 30} // 두 번째 항: 점수 (15점: Small Straight, 30점: Large Straight)
    };

    public override int CalculateScore(int[] pips)
    {
        // 주사위 값을 List로 변환하고 정렬
        List<int> nums = pips.ToList<int>();
        nums.Sort();

        int straightCount = 1; // 연속된 숫자 수를 계산할 변수
        int maxStraightCount = 1; // 최대 연속된 숫자 수를 저장할 변수

        // 연속된 숫자의 길이 계산
        for (int i = 1; i < nums.Count; i++)
        {
            if (nums[i] == nums[i - 1] + 1) // 연속된 숫자인 경우
            {
                straightCount++;
                maxStraightCount = Mathf.Max(maxStraightCount, straightCount);
            }
            else if (nums[i] != nums[i - 1]) // 연속되지 않은 경우
            {
                straightCount = 1; // 카운트 초기화
            }
        }

        // 연속된 숫자가 기준에 미치지 못하는 경우 0점 반환
        if (maxStraightCount < ints[0, checkSize]) return 0;

        // 기준을 충족하는 경우 해당 점수 반환
        return ints[1, checkSize];
    }
}
