using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class ScoreManager : MonoBehaviour
{
    [SerializeField] private DiceController Dice;
    [SerializeField] private Button[] Dice_arr; // How to get Buttons from DiceController instead of direct assignment
    
    public int[] Dice_Hand;
    private int[] Dice_Check;

    #region Scores
    public int[] Scoreboard_Scores;
    #endregion

    #region Scoreboard buttons
    [SerializeField] private Button[] ScoreSelection_BTN;
    #endregion

    #region Scoreboard selections
    public bool[] isScoreSelected;
    public bool isUpperComplete;
    #endregion

    #region Scoreboard Texts
    [SerializeField] private Text[] Scoreboard_Text;
    #endregion

    public bool isGameOver;

    private void Awake()
    {
        Dice_Hand = new int[5];
        Dice_Check = new int[6];

        ScoreSelection_BTN = GetComponentsInChildren<Button>();
        isScoreSelected = new bool[12];
        for (int i = 0; i < 12; i++)
        {
            isScoreSelected[i] = false;
        }
        Scoreboard_Scores = new int[15];
        Scoreboard_Text = GetComponentsInChildren<Text>();

        isUpperComplete = false;
        isGameOver = false;
    }

    private void Update()
    {
        // Check completion -> If all 12 hands completed: Game Over
        int totalSelectionCount = 0;
        for (int i = 0; i < 12; i++)
        {
            if (isScoreSelected[i])
            {
                totalSelectionCount++;
            }
        }
        if (totalSelectionCount == 12)
        {
            isGameOver = true;
            // If player 1 & player 2 are BOTH isGamveOver=true -----> Result UI & End game
        }
    }


    // Calculate score for current hand for all unselected scores
    // If selected, score will not change
    public void CalcScore()
    {
        for (int i = 0; i < 12; i++)
        {
            if (isScoreSelected[i])
            {
                Scoreboard_Scores[i] = Scoreboard_Scores[i];
            }
            else
            {
                Scoreboard_Scores[i] = CalcHand(i);
            }
        }

        // Calculate subtotal when upper is finished
        int isUpperComplete = 0;
        for (int i = 0; i < 6; i++)
        {
            if (isScoreSelected[i])
            {
                isUpperComplete++;
            }
        }
        if (isUpperComplete == 6)
        {
            for (int i = 0; i < 6; i++)
            {
                Scoreboard_Scores[12] += Scoreboard_Scores[i];
            }
            if (Scoreboard_Scores[12] >= 63)
            {
                Scoreboard_Scores[13] = 35;
                Scoreboard_Scores[14] += Scoreboard_Scores[13];
            }
        }

        for (int i = 0; i < 15; i++)
        {
            Scoreboard_Text[i].text = $"{Scoreboard_Scores[i]}";
        }
    }


    #region Calculation of scores for each type
    public int CalcHand(int index)
    {
        int sum = 0;
        switch (index)
        {
            case 0: // Aces
                for (int i = 0; i < 5; i++)
                {
                    if (Dice_Hand[i] == 1)
                    {
                        sum += 1;
                    }
                }
                break;
            case 1: // Deuces
                for (int i = 0; i < 5; i++)
                {
                    if (Dice_Hand[i] == 2)
                    {
                        sum += 2;
                    }
                }
                break;
            case 2: // Threes
                for (int i = 0; i < 5; i++)
                {
                    if (Dice_Hand[i] == 3)
                    {
                        sum += 3;
                    }
                }
                break;
            case 3: // Fours
                for (int i = 0; i < 5; i++)
                {
                    if (Dice_Hand[i] == 4)
                    {
                        sum += 4;
                    }
                }
                break;
            case 4: // Fives
                for (int i = 0; i < 5; i++)
                {
                    if (Dice_Hand[i] == 5)
                    {
                        sum += 5;
                    }
                }
                break;
            case 5: // Sixes
                for (int i = 0; i < 5; i++)
                {
                    if (Dice_Hand[i] == 6)
                    {
                        sum += 6;
                    }
                }
                break;
            case 6: // Chance
                for (int i = 0; i < 5; i++)
                {
                    sum += Dice_Hand[i];
                }
                break;
            case 7: // FOAK
                for (int i = 0; i < 6; i++)
                {
                    Dice_Check[i] = 0;
                }

                for (int i = 0; i < 5; i++)
                {
                    Dice_Check[Dice_Hand[i] - 1] += 1;
                }
                Debug.Log($"FOAK Check : {Dice_Hand[0]} | {Dice_Hand[1]} | {Dice_Hand[2]} | {Dice_Hand[3]} | {Dice_Hand[4]}");
                Debug.Log($"FOAK Check : {Dice_Check[0]} | {Dice_Check[1]} | {Dice_Check[2]} | {Dice_Check[3]} | {Dice_Check[4]} | {Dice_Check[5]}");

                for (int i = 0; i < 6; i++)
                {
                    if (Dice_Check[i] >= 4)
                    {
                        Debug.Log("Four of a kind");
                        for (int j = 0; j < 5; j++)
                        {
                            sum += Dice_Hand[j];
                        }
                    }
                }
                break;
            case 8: // Full House
                bool trips = false;
                bool pair = false;
                for (int i = 0; i < 6; i++)
                {
                    Dice_Check[i] = 0;
                }

                Debug.Log($"Full House Check : {Dice_Hand[0]} | {Dice_Hand[1]} | {Dice_Hand[2]} | {Dice_Hand[3]} | {Dice_Hand[4]}");

                for (int i = 0; i < 5; i++)
                {
                    Dice_Check[Dice_Hand[i] - 1] += 1;
                }
                Debug.Log($"Full House Check : {Dice_Check[0]} | {Dice_Check[1]} | {Dice_Check[2]} | {Dice_Check[3]} | {Dice_Check[4]} | {Dice_Check[5]}");

                for (int i = 0; i < 6; i++)
                {
                    if (Dice_Check[i] == 3)
                    {
                        trips = true;
                    }
                    if (Dice_Check[i] == 2)
                    {
                        pair = true;
                    }
                    if (Dice_Check[i] == 5)
                    {
                        trips = true;
                        pair = true;
                    }
                }

                if (trips && pair)
                {
                    Debug.Log("FULL HOUSE!");
                    for (int i = 0; i < 5; i++)
                    {
                        sum += Dice_Hand[i];
                    }
                }
                break;
            case 9: //SStraight
                for (int i = 0; i < 6; i++)
                {
                    Dice_Check[i] = 0;
                }

                for (int i = 0; i < 5; i++)
                {
                    Dice_Check[Dice_Hand[i] - 1] += 1;
                }
                Debug.Log($"Small Straight Check : {Dice_Hand[0]} | {Dice_Hand[1]} | {Dice_Hand[2]} | {Dice_Hand[3]} | {Dice_Hand[4]}");
                Debug.Log($"Small Straight Check : {Dice_Check[0]} | {Dice_Check[1]} | {Dice_Check[2]} | {Dice_Check[3]} | {Dice_Check[4]} | {Dice_Check[5]}");

                bool checkss = false;

                if (Dice_Check[0] >= 1 && Dice_Check[1] >= 1 && Dice_Check[2] >= 1 && Dice_Check[3] >= 1)
                {
                    checkss = true;
                }
                if (Dice_Check[1] >= 1 && Dice_Check[2] >= 1 && Dice_Check[3] >= 1 && Dice_Check[4] >= 1)
                {
                    checkss = true;
                }
                if (Dice_Check[2] >= 1 && Dice_Check[3] >= 1 && Dice_Check[4] >= 1 && Dice_Check[5] >= 1)
                {
                    checkss = true;
                }

                Debug.Log($"Small Straight Check : {checkss}");
                if (checkss)
                {
                    sum = 15;
                }
                break;
            case 10: // LStraight
                for (int i = 0; i < 6; i++)
                {
                    Dice_Check[i] = 0;
                }

                for (int i = 0; i < 5; i++)
                {
                    Dice_Check[Dice_Hand[i] - 1] += 1;
                }
                Debug.Log($"Large Straight Check : {Dice_Hand[0]} | {Dice_Hand[1]} | {Dice_Hand[2]} | {Dice_Hand[3]} | {Dice_Hand[4]}");
                Debug.Log($"Large Straight Check : {Dice_Check[0]} | {Dice_Check[1]} | {Dice_Check[2]} | {Dice_Check[3]} | {Dice_Check[4]} | {Dice_Check[5]}");

                bool checksl = false;

                if (Dice_Check[0] >= 1 && Dice_Check[1] >= 1 && Dice_Check[2] >= 1 && Dice_Check[3] >= 1 && Dice_Check[4] >= 1)
                {
                    checksl = true;
                }
                if (Dice_Check[1] >= 1 && Dice_Check[2] >= 1 && Dice_Check[3] >= 1 && Dice_Check[4] >= 1 && Dice_Check[5] >= 1)
                {
                    checksl = true;
                }

                Debug.Log($"Large Straight Check : {checksl}");
                if (checksl)
                {
                    sum = 30;
                }
                break;
            case 11: // Yahtzee
                for (int i = 0; i < 6; i++)
                {
                    Dice_Check[i] = 0;
                }

                for (int i = 0; i < 5; i++)
                {
                    Dice_Check[Dice_Hand[i] - 1] += 1;
                }
                Debug.Log($"YAHTZEE Check : {Dice_Hand[0]} | {Dice_Hand[1]} | {Dice_Hand[2]} | {Dice_Hand[3]} | {Dice_Hand[4]}");
                Debug.Log($"YAHTZEE Check : {Dice_Check[0]} | {Dice_Check[1]} | {Dice_Check[2]} | {Dice_Check[3]} | {Dice_Check[4]} | {Dice_Check[5]}");

                for (int i = 0; i < 6; i++)
                {
                    if (Dice_Check[i] == 5)
                    {
                        Debug.Log("YAHTZEE!");
                        sum = 50;
                    }
                }
                break;
        }
        return sum;
    }
    #endregion

    #region Score selection buttons
    public void ScoreSelectionBTN(int index)
    {
        isScoreSelected[index] = true;
        Dice.rollCount = 0; // Reset roll count
        for (int i = 0; i < 5; i++)
        {
            if (Dice.isKept[i])
            {
                Dice.isKept[i] = false;
            }
            Dice_arr[i].interactable = false;
        }
        // Disable button if selected
        if (ScoreSelection_BTN[index].interactable)
        {
            ScoreSelection_BTN[index].interactable = false;
        }

        Scoreboard_Scores[14] += Scoreboard_Scores[index];

        // Turn over ---> Switch to next player
    }
    #endregion
}
