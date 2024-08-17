using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class StraightSection : PointSlot
{
    [SerializeField]
    int checkSize = 0;

    int[,] ints = {
        { 4, 5},  // ù ��° ��: ���ӵ� ������ ���� (4: Small Straight, 5: Large Straight)
        { 15, 30} // �� ��° ��: ���� (15��: Small Straight, 30��: Large Straight)
    };

    public override int CalculateScore(int[] pips)
    {
        // �ֻ��� ���� List�� ��ȯ�ϰ� ����
        List<int> nums = pips.ToList<int>();
        nums.Sort();

        int straightCount = 1; // ���ӵ� ���� ���� ����� ����
        int maxStraightCount = 1; // �ִ� ���ӵ� ���� ���� ������ ����

        // ���ӵ� ������ ���� ���
        for (int i = 1; i < nums.Count; i++)
        {
            if (nums[i] == nums[i - 1] + 1) // ���ӵ� ������ ���
            {
                straightCount++;
                maxStraightCount = Mathf.Max(maxStraightCount, straightCount);
            }
            else if (nums[i] != nums[i - 1]) // ���ӵ��� ���� ���
            {
                straightCount = 1; // ī��Ʈ �ʱ�ȭ
            }
        }

        // ���ӵ� ���ڰ� ���ؿ� ��ġ�� ���ϴ� ��� 0�� ��ȯ
        if (maxStraightCount < ints[0, checkSize]) return 0;

        // ������ �����ϴ� ��� �ش� ���� ��ȯ
        return ints[1, checkSize];
    }
}
