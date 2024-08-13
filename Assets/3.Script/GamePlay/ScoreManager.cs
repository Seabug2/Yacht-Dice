using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
/*
public enum ScoreSuit
{
    Aces,
    Deuces,
    Threes,
    Fours,
    Fives,
    Sixes,
    Subtotal,
    Bonus,
    Chance,
    Four_Of_A_Kind,
    Full_House,
    S_Straight,
    L_Straight,
    Yahtzee,
    Total
}
*/
public class ScoreManager : MonoBehaviour
{
    public int Dice_Num;
    public int[] Dice_Hand;
    private int[] Dice_Check;

    [SerializeField] private Text D1_Text;
    [SerializeField] private Text D2_Text;
    [SerializeField] private Text D3_Text;
    [SerializeField] private Text D4_Text;
    [SerializeField] private Text D5_Text;

    public int Aces = 0;
    public int Deuces = 0;
    public int Threes = 0;
    public int Fours = 0;
    public int Fives = 0;
    public int Sixes = 0;
    public int SubTotal = 0;
    public int Bonus = 0;
    public int Chance = 0;
    public int FOAK = 0;
    public int FullHouse = 0;
    public int Straight_s = 0;
    public int Straight_l = 0;
    public int Yahtzee = 0;
    public int Total = 0;

    [SerializeField] private Text Aces_Text;
    [SerializeField] private Text Deuces_Text;
    [SerializeField] private Text Threes_Text;
    [SerializeField] private Text Fours_Text;
    [SerializeField] private Text Fives_Text;
    [SerializeField] private Text Sixes_Text;
    [SerializeField] private Text SubTotal_Text;
    [SerializeField] private Text Bonus_Text;
    [SerializeField] private Text Chance_Text;
    [SerializeField] private Text FOAK_Text;
    [SerializeField] private Text FullHouse_Text;
    [SerializeField] private Text Straight_s_Text;
    [SerializeField] private Text Straight_l_Text;
    [SerializeField] private Text Yahtzee_Text;
    [SerializeField] private Text Total_Text;

    public bool isKept = false;
    public bool isFilled = false;

    private void Awake()
    {
        Dice_Hand = new int[5];
        for (int i = 0; i < 5; i++)
        {
            Dice_Hand[i] = 1;
        }
        Dice_Check = new int[6];
    }

    private void Update()
    {
        D1_Text.text = $"{Dice_Hand[0]}";
        D2_Text.text = $"{Dice_Hand[1]}";
        D3_Text.text = $"{Dice_Hand[2]}";
        D4_Text.text = $"{Dice_Hand[3]}";
        D5_Text.text = $"{Dice_Hand[4]}";

        Aces_Text.text = $"{Aces}";
        Deuces_Text.text = $"{Deuces}";
        Threes_Text.text = $"{Threes}";
        Fours_Text.text = $"{Fours}";
        Fives_Text.text = $"{Fives}";
        Sixes_Text.text = $"{Sixes}";
        SubTotal = Aces + Deuces + Threes + Fours + Fives + Sixes;
        if (SubTotal >= 63)
        {
            Bonus = 35;
        }
        SubTotal_Text.text = $"{SubTotal}";
        Bonus_Text.text = $"{Bonus}";
        Chance_Text.text = $"{Chance}";
        FOAK_Text.text = $"{FOAK}";
        FullHouse_Text.text = $"{FullHouse}";
        Straight_s_Text.text = $"{Straight_s}";
        Straight_l_Text.text = $"{Straight_l}";
        Yahtzee_Text.text = $"{Yahtzee}";
        Total_Text.text = $"{Total}";
        Total = SubTotal + Bonus + Chance + FOAK + FullHouse + Straight_s + Straight_l + Yahtzee;
    }

    public void CalcScore()
    {
        Aces = CalcAces();
        Deuces = CalcDeuces();
        Threes = CalcThrees();
        Fours = CalcFours();
        Fives = CalcFives();
        Sixes = CalcSixes();
        Chance = CalcChance();
        FOAK = CalcFOAK();
        FullHouse = CalcFullHouse();
        Straight_s = CalcStraight_s();
        Straight_l = CalcStraight_l();
        Yahtzee = CalcYahtzee();
    }

    public void RollButton()
    {
        for (int i = 0; i < 5; i++)
        {
            Dice_Hand[i] = (int)Random.Range(1, 6);
        }
        CalcScore();
    }

    public int CalcAces()
    {
        int sum = 0;
        for (int i = 0; i < 5; i++)
        {
            if (Dice_Hand[i] == 1)
            {
                sum += 1;
            }
        }
        return sum;
    }

    public int CalcDeuces()
    {
        int sum = 0;
        for (int i = 0; i < 5; i++)
        {
            if (Dice_Hand[i] == 2)
            {
                sum += 2;
            }
        }
        return sum;
    }

    public int CalcThrees()
    {
        int sum = 0;
        for (int i = 0; i < 5; i++)
        {
            if (Dice_Hand[i] == 3)
            {
                sum += 3;
            }
        }
        return sum;
    }

    public int CalcFours()
    {
        int sum = 0;
        for (int i = 0; i < 5; i++)
        {
            if (Dice_Hand[i] == 4)
            {
                sum += 4;
            }
        }
        return sum;
    }

    public int CalcFives()
    {
        int sum = 0;
        for (int i = 0; i < 5; i++)
        {
            if (Dice_Hand[i] == 5)
            {
                sum += 5;
            }
        }
        return sum;
    }

    public int CalcSixes()
    {
        int sum = 0;
        for (int i = 0; i < 5; i++)
        {
            if (Dice_Hand[i] == 6)
            {
                sum += 6;
            }
        }
        return sum;
    }

    public int CalcChance()
    {
        int sum = 0;
        for (int i = 0; i < 5; i++)
        {
            sum += Dice_Hand[i];
        }
        return sum;
    }

    public int CalcYahtzee()
    {
        int yahtzee = 0;
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
                yahtzee = 50;
            }
        }
        return yahtzee;
    }

    public int CalcFOAK()
    {
        int foak = 0;
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
                    foak += Dice_Hand[j];
                }
            }
        }
        return foak;
    }

    public int CalcFullHouse()
    {
        int sum = 0;
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
        }

        if (trips && pair)
        {
            Debug.Log("FULL HOUSE!");
            for (int i = 0; i < 5; i++)
            {
                sum += Dice_Hand[i];
            }
        }

        return sum;
    }

    public int CalcStraight_s()
    {
        int straight_s = 0;
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
            straight_s = 15;
        }

        return straight_s;
    }

    public int CalcStraight_l()
    {
        int straight_l = 0;
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
            straight_l = 30;
        }

        return straight_l;
    }
}
