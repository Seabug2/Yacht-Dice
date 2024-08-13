using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DiceController : MonoBehaviour
{
    [SerializeField] private ScoreManager score;
    [SerializeField] private GameObject rollBtn;

    [SerializeField] private Button Dice1;
    [SerializeField] private Button Dice2;
    [SerializeField] private Button Dice3;
    [SerializeField] private Button Dice4;
    [SerializeField] private Button Dice5;

    public int[] Dice_Hand;

    [SerializeField] private Text D1_Text;
    [SerializeField] private Text D2_Text;
    [SerializeField] private Text D3_Text;
    [SerializeField] private Text D4_Text;
    [SerializeField] private Text D5_Text;

    public bool isD1Kept = false;
    public bool isD2Kept = false;
    public bool isD3Kept = false;
    public bool isD4Kept = false;
    public bool isD5Kept = false;

    public int rollCount;

    private void Awake()
    {
        rollCount = 0;
        Dice_Hand = new int[5];
        for (int i = 0; i < 5; i++)
        {
            Dice_Hand[i] = i + 1;
        }
        Dice1.interactable = false;
        Dice2.interactable = false;
        Dice3.interactable = false;
        Dice4.interactable = false;
        Dice5.interactable = false;
    }

    private void Update()
    {
        D1_Text.text = $"{Dice_Hand[0]}";
        D2_Text.text = $"{Dice_Hand[1]}";
        D3_Text.text = $"{Dice_Hand[2]}";
        D4_Text.text = $"{Dice_Hand[3]}";
        D5_Text.text = $"{Dice_Hand[4]}";

        if (rollCount == 0)
        {
            rollBtn.SetActive(true);
        }
    }

    public void RollButton()
    {
        Dice1.interactable = true;
        Dice2.interactable = true;
        Dice3.interactable = true;
        Dice4.interactable = true;
        Dice5.interactable = true;

        if (isD1Kept)
        {
            Dice_Hand[0] = Dice_Hand[0];
        }
        else
        {
            Dice_Hand[0] = (int)Random.Range(1, 7);
        }

        if (isD2Kept)
        {
            Dice_Hand[1] = Dice_Hand[1];
        }
        else
        {
            Dice_Hand[1] = (int)Random.Range(1, 7);
        }

        if (isD3Kept)
        {
            Dice_Hand[2] = Dice_Hand[2];
        }
        else
        {
            Dice_Hand[2] = (int)Random.Range(1, 7);
        }

        if (isD4Kept)
        {
            Dice_Hand[3] = Dice_Hand[3];
        }
        else
        {
            Dice_Hand[3] = (int)Random.Range(1, 7);
        }

        if (isD5Kept)
        {
            Dice_Hand[4] = Dice_Hand[4];
        }
        else
        {
            Dice_Hand[4] = (int)Random.Range(1, 7);
        }
        
        rollCount++;
        if (rollCount >= 3)
        {
            rollBtn.SetActive(false);
        }

        //score.CalcScore();
    }

    public void KeepDice1()
    {
        if (!isD1Kept)
        {
            isD1Kept = true;
        }
        else
        {
            isD1Kept = false;
        }
    }

    public void KeepDice2()
    {
        if (!isD2Kept)
        {
            isD2Kept = true;
        }
        else
        {
            isD2Kept = false;
        }
    }

    public void KeepDice3()
    {
        if (!isD3Kept)
        {
            isD3Kept = true;
        }
        else
        {
            isD3Kept = false;
        }
    }

    public void KeepDice4()
    {
        if (!isD4Kept)
        {
            isD4Kept = true;
        }
        else
        {
            isD4Kept = false;
        }
    }

    public void KeepDice5()
    {
        if (!isD5Kept)
        {
            isD5Kept = true;
        }
        else
        {
            isD5Kept = false;
        }
    }
}
