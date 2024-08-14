using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DiceController : MonoBehaviour
{
    [SerializeField] private ScoreManager score;
    [SerializeField] private GameObject rollBtn;

    [SerializeField] private Button[] Dice_BTN;

    public int[] Dice_Hand;

    [SerializeField] private Text[] Dice_Text;

    public bool[] isKept;

    public bool isD1Kept = false;
    public bool isD2Kept = false;
    public bool isD3Kept = false;
    public bool isD4Kept = false;
    public bool isD5Kept = false;

    public int rollCount;

    private void Awake()
    {
        rollCount = 0;
        Dice_BTN = GetComponentsInChildren<Button>();
        Dice_Text = GetComponentsInChildren<Text>();
        Dice_Hand = new int[5];
        isKept = new bool[5];
        for (int i = 0; i < 5; i++)
        {
            Dice_Hand[i] = i + 1;
            Dice_Text[i].text = $"{Dice_Hand[i]}";
            Dice_BTN[i].interactable = false;
            isKept[i] = false;
        }
    }

    private void Update()
    {
        if (rollCount == 0)
        {
            rollBtn.SetActive(true);
        }
    }

    public void RollButton()
    {
        for (int i = 0; i < 5; i++)
        {
            Dice_BTN[i].interactable = true;
            if (isKept[i])
            {
                Dice_Hand[i] = Dice_Hand[i];
            }
            else
            {
                Dice_Hand[i] = (int)Random.Range(1, 7);
            }
            score.Dice_Hand[i] = Dice_Hand[i];
        }
        
        for (int i = 0; i < 5; i++)
        {
            Dice_Text[i].text = $"{Dice_Hand[i]}";
        }

        rollCount++;
        if (rollCount >= 3)
        {
            rollBtn.SetActive(false);
        }

        score.CalcScore();
    }

    public void KeepDice1()
    {
        if (!isKept[0])
        {
            isKept[0] = true;
        }
        else
        {
            isKept[0] = false;
        }
    }

    public void KeepDice2()
    {
        if (!isKept[1])
        {
            isKept[1] = true;
        }
        else
        {
            isKept[1] = false;
        }
    }

    public void KeepDice3()
    {
        if (!isKept[2])
        {
            isKept[2] = true;
        }
        else
        {
            isKept[2] = false;
        }
    }

    public void KeepDice4()
    {
        if (!isKept[3])
        {
            isKept[3] = true;
        }
        else
        {
            isKept[3] = false;
        }
    }

    public void KeepDice5()
    {
        if (!isKept[4])
        {
            isKept[4] = true;
        }
        else
        {
            isKept[4] = false;
        }
    }
}
