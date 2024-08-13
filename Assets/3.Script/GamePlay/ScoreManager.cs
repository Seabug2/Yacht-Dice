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
    [SerializeField] private DiceController Dice;
    [SerializeField] private Button Dice1;
    [SerializeField] private Button Dice2;
    [SerializeField] private Button Dice3;
    [SerializeField] private Button Dice4;
    [SerializeField] private Button Dice5;
    
    public int[] Dice_Hand;
    private int[] Dice_Check;

    #region Scores
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
    #endregion

    #region Scoreboard buttons
    [SerializeField] private Button Aces_BTN;
    [SerializeField] private Button Deuces_BTN;
    [SerializeField] private Button Threes_BTN;
    [SerializeField] private Button Fours_BTN;
    [SerializeField] private Button Fives_BTN;
    [SerializeField] private Button Sixes_BTN;
    [SerializeField] private Button Chance_BTN;
    [SerializeField] private Button FOAK_BTN;
    [SerializeField] private Button FullHouse_BTN;
    [SerializeField] private Button Straight_s_BTN;
    [SerializeField] private Button Straight_l_BTN;
    [SerializeField] private Button Yahtzee_BTN;
    #endregion

    #region Scoreboard selections
    public bool isAcesSelected;
    public bool isDeucesSelected;
    public bool isThreesSelected;
    public bool isFoursSelected;
    public bool isFivesSelected;
    public bool isSixesSelected;
    public bool isChanceSelected;
    public bool isFOAKSelected;
    public bool isFullHouseSelected;
    public bool isStraight_sSelected;
    public bool isStraight_lSelected;
    public bool isYahtzeeSelected;
    #endregion

    #region Scoreboard Texts
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
    #endregion

    private void Awake()
    {
        Dice_Hand = Dice.Dice_Hand;
        Dice_Check = new int[6];
        isAcesSelected = false;
        isDeucesSelected = false;
        isThreesSelected = false;
        isFoursSelected = false;
        isFivesSelected = false;
        isSixesSelected = false;
        isChanceSelected = false;
        isFOAKSelected = false;
        isFullHouseSelected = false;
        isStraight_sSelected = false;
        isStraight_lSelected = false;
        isYahtzeeSelected = false;
    }

    private void Update()
    {
        Aces_Text.text = $"{Aces}";
        Deuces_Text.text = $"{Deuces}";
        Threes_Text.text = $"{Threes}";
        Fours_Text.text = $"{Fours}";
        Fives_Text.text = $"{Fives}";
        Sixes_Text.text = $"{Sixes}";
        SubTotal_Text.text = $"{SubTotal}";
        Bonus_Text.text = $"{Bonus}";
        Chance_Text.text = $"{Chance}";
        FOAK_Text.text = $"{FOAK}";
        FullHouse_Text.text = $"{FullHouse}";
        Straight_s_Text.text = $"{Straight_s}";
        Straight_l_Text.text = $"{Straight_l}";
        Yahtzee_Text.text = $"{Yahtzee}";
        Total_Text.text = $"{Total}";

        if (isAcesSelected && isDeucesSelected && isThreesSelected && isFoursSelected && isFivesSelected && isSixesSelected)
        {
            SubTotal = Aces + Deuces + Threes + Fours + Fives + Sixes;
            if (SubTotal >= 63)
            {
                Bonus = 35;
                GameManager.Instance.AddScore(Bonus);
            }
            if (isChanceSelected && isFOAKSelected && isFullHouseSelected && isStraight_sSelected && isStraight_lSelected && isYahtzeeSelected)
            {
                GameManager.Instance.GameOver();
            }
        }
        Total = GameManager.Instance.Score_Total;
    }


    // Calculate score for current hand for all unselected scores
    // If selected, score will not change
    public void CalcScore()
    {
        if (isAcesSelected)
        {
            Aces = Aces;
        }
        else
        {
            Aces = CalcAces();
        }
        if (isDeucesSelected)
        {
            Deuces = Deuces;
        }
        else
        {
            Deuces = CalcDeuces();
        }
        if (isThreesSelected)
        {
            Threes = Threes;
        }
        else
        {
            Threes = CalcThrees();
        }
        if (isFoursSelected)
        {
            Fours = Fours;
        }
        else
        {
            Fours = CalcFours();
        }
        if (isFivesSelected)
        {
            Fives = Fives;
        }
        else
        {
            Fives = CalcFives();
        }
        if (isSixesSelected)
        {
            Sixes = Sixes;
        }
        else
        {
            Sixes = CalcSixes();
        }
        if (isChanceSelected)
        {
            Chance = Chance;
        }
        else
        {
            Chance = CalcChance();
        }
        if (isFOAKSelected)
        {
            FOAK = FOAK;
        }
        else
        {
            FOAK = CalcFOAK();
        }
        if (isFullHouseSelected)
        {
            FullHouse = FullHouse;
        }
        else
        {
            FullHouse = CalcFullHouse();
        }
        if (isStraight_sSelected)
        {
            Straight_s = Straight_s;
        }
        else
        {
            Straight_s = CalcStraight_s();
        }
        if (isStraight_lSelected)
        {
            Straight_l = Straight_l;
        }
        else
        {
            Straight_l = CalcStraight_l();
        }
        if (isYahtzeeSelected)
        {
            Yahtzee = Yahtzee;
        }
        else
        {
            Yahtzee = CalcYahtzee();
        }
    }


    #region Calculation of scores for each type
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
    #endregion

    #region Score selection buttons
    public void AcesBTN()
    {
        isAcesSelected = true;
        Dice.rollCount = 0;
        if (Dice.isD1Kept)
        {
            Dice.isD1Kept = false;
        }
        if (Dice.isD2Kept)
        {
            Dice.isD2Kept = false;
        }
        if (Dice.isD3Kept)
        {
            Dice.isD3Kept = false;
        }
        if (Dice.isD4Kept)
        {
            Dice.isD4Kept = false;
        }
        if (Dice.isD5Kept)
        {
            Dice.isD5Kept = false;
        }
        if (Aces_BTN.interactable)
        {
            Aces_BTN.interactable = false;
        }
        Dice1.interactable = false;
        Dice2.interactable = false;
        Dice3.interactable = false;
        Dice4.interactable = false;
        Dice5.interactable = false;

        GameManager.Instance.AddScore(Aces);

        // Turn over ---> Switch to next player
    }

    public void DeucesBTN()
    {
        isDeucesSelected = true;
        Dice.rollCount = 0;
        if (Dice.isD1Kept)
        {
            Dice.isD1Kept = false;
        }
        if (Dice.isD2Kept)
        {
            Dice.isD2Kept = false;
        }
        if (Dice.isD3Kept)
        {
            Dice.isD3Kept = false;
        }
        if (Dice.isD4Kept)
        {
            Dice.isD4Kept = false;
        }
        if (Dice.isD5Kept)
        {
            Dice.isD5Kept = false;
        }
        if (Deuces_BTN.interactable)
        {
            Deuces_BTN.interactable = false;
        }
        Dice1.interactable = false;
        Dice2.interactable = false;
        Dice3.interactable = false;
        Dice4.interactable = false;
        Dice5.interactable = false;

        GameManager.Instance.AddScore(Deuces);

        // Turn over ---> Switch to next player
    }

    public void ThreesBTN()
    {
        isThreesSelected = true;
        Dice.rollCount = 0;
        if (Dice.isD1Kept)
        {
            Dice.isD1Kept = false;
        }
        if (Dice.isD2Kept)
        {
            Dice.isD2Kept = false;
        }
        if (Dice.isD3Kept)
        {
            Dice.isD3Kept = false;
        }
        if (Dice.isD4Kept)
        {
            Dice.isD4Kept = false;
        }
        if (Dice.isD5Kept)
        {
            Dice.isD5Kept = false;
        }
        if (Threes_BTN.interactable)
        {
            Threes_BTN.interactable = false;
        }
        Dice1.interactable = false;
        Dice2.interactable = false;
        Dice3.interactable = false;
        Dice4.interactable = false;
        Dice5.interactable = false;

        GameManager.Instance.AddScore(Threes);

        // Turn over ---> Switch to next player
    }

    public void FoursBTN()
    {
        isFoursSelected = true;
        Dice.rollCount = 0;
        if (Dice.isD1Kept)
        {
            Dice.isD1Kept = false;
        }
        if (Dice.isD2Kept)
        {
            Dice.isD2Kept = false;
        }
        if (Dice.isD3Kept)
        {
            Dice.isD3Kept = false;
        }
        if (Dice.isD4Kept)
        {
            Dice.isD4Kept = false;
        }
        if (Dice.isD5Kept)
        {
            Dice.isD5Kept = false;
        }
        if (Fours_BTN.interactable)
        {
            Fours_BTN.interactable = false;
        }
        Dice1.interactable = false;
        Dice2.interactable = false;
        Dice3.interactable = false;
        Dice4.interactable = false;
        Dice5.interactable = false;

        GameManager.Instance.AddScore(Fours);

        // Turn over ---> Switch to next player
    }

    public void FivesBTN()
    {
        isFivesSelected = true;
        Dice.rollCount = 0;
        if (Dice.isD1Kept)
        {
            Dice.isD1Kept = false;
        }
        if (Dice.isD2Kept)
        {
            Dice.isD2Kept = false;
        }
        if (Dice.isD3Kept)
        {
            Dice.isD3Kept = false;
        }
        if (Dice.isD4Kept)
        {
            Dice.isD4Kept = false;
        }
        if (Dice.isD5Kept)
        {
            Dice.isD5Kept = false;
        }
        if (Fives_BTN.interactable)
        {
            Fives_BTN.interactable = false;
        }
        Dice1.interactable = false;
        Dice2.interactable = false;
        Dice3.interactable = false;
        Dice4.interactable = false;
        Dice5.interactable = false;

        GameManager.Instance.AddScore(Fives);

        // Turn over ---> Switch to next player
    }

    public void SixesBTN()
    {
        isSixesSelected = true;
        Dice.rollCount = 0;
        if (Dice.isD1Kept)
        {
            Dice.isD1Kept = false;
        }
        if (Dice.isD2Kept)
        {
            Dice.isD2Kept = false;
        }
        if (Dice.isD3Kept)
        {
            Dice.isD3Kept = false;
        }
        if (Dice.isD4Kept)
        {
            Dice.isD4Kept = false;
        }
        if (Dice.isD5Kept)
        {
            Dice.isD5Kept = false;
        }
        if (Sixes_BTN.interactable)
        {
            Sixes_BTN.interactable = false;
        }
        Dice1.interactable = false;
        Dice2.interactable = false;
        Dice3.interactable = false;
        Dice4.interactable = false;
        Dice5.interactable = false;

        GameManager.Instance.AddScore(Sixes);

        // Turn over ---> Switch to next player
    }

    public void ChanceBTN()
    {
        isChanceSelected = true;
        Dice.rollCount = 0;
        if (Dice.isD1Kept)
        {
            Dice.isD1Kept = false;
        }
        if (Dice.isD2Kept)
        {
            Dice.isD2Kept = false;
        }
        if (Dice.isD3Kept)
        {
            Dice.isD3Kept = false;
        }
        if (Dice.isD4Kept)
        {
            Dice.isD4Kept = false;
        }
        if (Dice.isD5Kept)
        {
            Dice.isD5Kept = false;
        }
        if (Chance_BTN.interactable)
        {
            Chance_BTN.interactable = false;
        }
        Dice1.interactable = false;
        Dice2.interactable = false;
        Dice3.interactable = false;
        Dice4.interactable = false;
        Dice5.interactable = false;

        GameManager.Instance.AddScore(Chance);

        // Turn over ---> Switch to next player
    }

    public void FOAKBTN()
    {
        isFOAKSelected = true;
        Dice.rollCount = 0;
        if (Dice.isD1Kept)
        {
            Dice.isD1Kept = false;
        }
        if (Dice.isD2Kept)
        {
            Dice.isD2Kept = false;
        }
        if (Dice.isD3Kept)
        {
            Dice.isD3Kept = false;
        }
        if (Dice.isD4Kept)
        {
            Dice.isD4Kept = false;
        }
        if (Dice.isD5Kept)
        {
            Dice.isD5Kept = false;
        }
        if (FOAK_BTN.interactable)
        {
            FOAK_BTN.interactable = false;
        }
        Dice1.interactable = false;
        Dice2.interactable = false;
        Dice3.interactable = false;
        Dice4.interactable = false;
        Dice5.interactable = false;

        GameManager.Instance.AddScore(FOAK);

        // Turn over ---> Switch to next player
    }

    public void Straight_sBTN()
    {
        isStraight_sSelected = true;
        Dice.rollCount = 0;
        if (Dice.isD1Kept)
        {
            Dice.isD1Kept = false;
        }
        if (Dice.isD2Kept)
        {
            Dice.isD2Kept = false;
        }
        if (Dice.isD3Kept)
        {
            Dice.isD3Kept = false;
        }
        if (Dice.isD4Kept)
        {
            Dice.isD4Kept = false;
        }
        if (Dice.isD5Kept)
        {
            Dice.isD5Kept = false;
        }
        if (Straight_s_BTN.interactable)
        {
            Straight_s_BTN.interactable = false;
        }
        Dice1.interactable = false;
        Dice2.interactable = false;
        Dice3.interactable = false;
        Dice4.interactable = false;
        Dice5.interactable = false;

        GameManager.Instance.AddScore(Straight_s);

        // Turn over ---> Switch to next player
    }

    public void Straight_lBTN()
    {
        isStraight_lSelected = true;
        Dice.rollCount = 0;
        if (Dice.isD1Kept)
        {
            Dice.isD1Kept = false;
        }
        if (Dice.isD2Kept)
        {
            Dice.isD2Kept = false;
        }
        if (Dice.isD3Kept)
        {
            Dice.isD3Kept = false;
        }
        if (Dice.isD4Kept)
        {
            Dice.isD4Kept = false;
        }
        if (Dice.isD5Kept)
        {
            Dice.isD5Kept = false;
        }
        if (Straight_l_BTN.interactable)
        {
            Straight_l_BTN.interactable = false;
        }
        Dice1.interactable = false;
        Dice2.interactable = false;
        Dice3.interactable = false;
        Dice4.interactable = false;
        Dice5.interactable = false;

        GameManager.Instance.AddScore(Straight_l);

        // Turn over ---> Switch to next player
    }

    public void FullHouseBTN()
    {
        isFullHouseSelected = true;
        Dice.rollCount = 0;
        if (Dice.isD1Kept)
        {
            Dice.isD1Kept = false;
        }
        if (Dice.isD2Kept)
        {
            Dice.isD2Kept = false;
        }
        if (Dice.isD3Kept)
        {
            Dice.isD3Kept = false;
        }
        if (Dice.isD4Kept)
        {
            Dice.isD4Kept = false;
        }
        if (Dice.isD5Kept)
        {
            Dice.isD5Kept = false;
        }
        if (FullHouse_BTN.interactable)
        {
            FullHouse_BTN.interactable = false;
        }
        Dice1.interactable = false;
        Dice2.interactable = false;
        Dice3.interactable = false;
        Dice4.interactable = false;
        Dice5.interactable = false;

        GameManager.Instance.AddScore(FullHouse);

        // Turn over ---> Switch to next player
    }

    public void YahtzeeBTN()
    {
        isYahtzeeSelected = true;
        Dice.rollCount = 0;
        if (Dice.isD1Kept)
        {
            Dice.isD1Kept = false;
        }
        if (Dice.isD2Kept)
        {
            Dice.isD2Kept = false;
        }
        if (Dice.isD3Kept)
        {
            Dice.isD3Kept = false;
        }
        if (Dice.isD4Kept)
        {
            Dice.isD4Kept = false;
        }
        if (Dice.isD5Kept)
        {
            Dice.isD5Kept = false;
        }
        if (Yahtzee_BTN.interactable)
        {
            Yahtzee_BTN.interactable = false;
        }
        Dice1.interactable = false;
        Dice2.interactable = false;
        Dice3.interactable = false;
        Dice4.interactable = false;
        Dice5.interactable = false;

        GameManager.Instance.AddScore(Yahtzee);

        // Turn over ---> Switch to next player
    }
    #endregion
}
