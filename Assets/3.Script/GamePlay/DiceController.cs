using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DiceController : MonoBehaviour
{
    [SerializeField] private ScoreManager score;

    public int[] Dice_Hand;

    [SerializeField] private Text D1_Text;
    [SerializeField] private Text D2_Text;
    [SerializeField] private Text D3_Text;
    [SerializeField] private Text D4_Text;
    [SerializeField] private Text D5_Text;

    public bool isKept = false;

    private void Awake()
    {
        Dice_Hand = new int[5];
        for (int i = 0; i < 5; i++)
        {
            Dice_Hand[i] += 1;
        }
    }

    private void Update()
    {
        D1_Text.text = $"{Dice_Hand[0]}";
        D2_Text.text = $"{Dice_Hand[1]}";
        D3_Text.text = $"{Dice_Hand[2]}";
        D4_Text.text = $"{Dice_Hand[3]}";
        D5_Text.text = $"{Dice_Hand[4]}";
    }

    public void RollButton()
    {
        for (int i = 0; i < 5; i++)
        {
            Dice_Hand[i] = (int)Random.Range(1, 6);
        }
        score.CalcScore();
    }
}
