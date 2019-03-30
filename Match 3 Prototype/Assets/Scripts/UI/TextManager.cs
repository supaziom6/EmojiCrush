using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class TextManager : MonoBehaviour {

    public Text ScoreText;
    public int Score;
    public Text MovesLeft;
    private int Moves;
    public Text EmojisText;
    public Image GoalImage;
    private int Emojis;
    public static bool WonTheGame;
    public static bool LevelEnded;
    public LevelInfo currentLevel;

    private void Awake()
    {
        LevelEnded = false;
        WonTheGame = false;
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
            WonTheGame = true;
        }
        RegisterUpdate();
    }
    public void UseMove()
    {
        Moves -= 1;
        RegisterUpdate();
    }

    public void UndoMove()
    {
        Moves += 1;
        RegisterUpdate();
    }

    public void UpdateScore(int score)
    {
        Score += score;
        RegisterUpdate();
    }

    public void GameOver()
    {
        if(Emojis <= 0)
        {
            WonTheGame = true;
        }
        else{
            WonTheGame = false;
        }
        LevelEnded = true;
        
    }
}
