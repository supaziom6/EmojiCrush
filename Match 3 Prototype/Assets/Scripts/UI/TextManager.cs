using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class TextManager : MonoBehaviour {

    public TextMeshProUGUI ScoreText;
    private int Score;
    public TextMeshProUGUI MovesLeft;
    private int Moves;
    public TextMeshProUGUI EmojisText;
    public Image GoalImage;
    private int Emojis;
    public bool GoalReached;
    public static bool LevelEnded;
    public LevelInfo currentLevel;

    private void Awake()
    {
        LevelEnded = false;
        GoalReached = false;
        currentLevel = LoadLoadingInfo.currentLevel;
        Score = 0;
        /*
         * load emoji from level file
         * load move limit from level file
         * 
         * */
        GoalImage.sprite = currentLevel.goalEmoji.GetComponent<SpriteRenderer>().sprite;
        Moves = currentLevel.movesAvailable;
        Emojis = currentLevel.RequiredEmojiAmmount;
        RegisterUpdate();

    }

    void RegisterUpdate()
    {
        ScoreText.text = "Score: " + Score;
        EmojisText.text = "" + Emojis;
        MovesLeft.text = "" + Moves;
        if(Moves <= 0 || Emojis <= 0)
        {
            GameOver();
        }
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

    public void GameOver()
    {
        LevelEnded = true;
        if(Emojis >= 0)
        {
            GoalReached = true;
        }


        //Do end game stuff here
        //Temp
        SceneManager.LoadScene("MainMenu");
    }
}
