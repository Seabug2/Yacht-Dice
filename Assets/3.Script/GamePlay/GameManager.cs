using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    Dice[] dices;

    [SerializeField]
    PointSlot[] mySlots;
    [SerializeField]
    PointSlot[] oppentSlots;

    private void Awake()
    {
        dices = FindObjectsOfType<Dice>();
    }

    public void Reroll()
    {
        foreach(Dice d in dices)
        {
            d.Reroll();
        }

        UpdateSlot();
    }

    void UpdateSlot()
    {
        //각 슬롯에게 전달하려는 주사위의 결과값
        int[] nums = new int[5];
        
        for(int i = 0; i < dices.Length; i ++)
        {
            nums[i] = dices[i].MyNum;
        }

        foreach(PointSlot ps in mySlots)
        {
            ps.UpdateSlot(nums);
        }
    }

    /// <summary>
    /// 점수를 확정
    /// </summary>
    public void Fixed()
    {

    }
}
