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

        //score.CalcScore();
    }

    public void KeepDice(int index)
    {
        if (!isKept[index])
        {
            isKept[index] = true;
        }
        else
        {
            isKept[index] = false;
        }
    }
}
