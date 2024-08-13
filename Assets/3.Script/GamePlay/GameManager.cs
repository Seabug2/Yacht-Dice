using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] private ScoreManager Score;
    [SerializeField] private DiceController Dice;

    public static int Score_Total;
    public static bool isGameOver = false;

    private static GameManager instance = null;
    public static GameManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<GameManager>();
            }
            return instance;
        }
    }

    private void Awake()
    {
        if (Instance == null)
        {
            Destroy(gameObject);
        }

        Score_Total = 0;
    }

    public void AddScore(int score)
    {
        Score_Total += score;
    }

    public void GameOver()
    {
        isGameOver = true;
        // If player 1 & player 2 are BOTH isGamveOver=true -----> Result UI & End game
    }
}
