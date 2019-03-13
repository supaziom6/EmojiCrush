using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TextManager : MonoBehaviour {

    public TextMeshProUGUI ScoreText;
    private int Score;
    public TextMeshProUGUI MovesLeft;
    private int Moves;
    public TextMeshProUGUI EmojisText;
    private int Emojis;
    public bool GoalReached;

    private void Start()
    {
        GoalReached = false;

        Score = 0;
        /*
         * load emoji from level file
         * load move limit from level file
         * 
         * */
         Moves = 45;
         Emojis = 30;

        RegisterUpdate();

    }

    void RegisterUpdate()
    {
        ScoreText.text = "Score: " + Score;
        EmojisText.text = "Emojis Left: " + Emojis;
        MovesLeft.text = "Moves Left: " + Moves;
    }
    public void GoalEmojis(int emojidestroyed)
    {
        Emojis -= emojidestroyed;
        if(Emojis <= 0)
        {
            GoalReached = true;
        }
        RegisterUpdate();
    }
    public void UseMove()
    {
        Moves -= 1;
        RegisterUpdate();
    }

    public void UpdateScore(int score)
    {
        Score += score;
        RegisterUpdate();
    }
}
