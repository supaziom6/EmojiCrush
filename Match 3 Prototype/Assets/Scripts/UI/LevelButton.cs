using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LevelButton : MonoBehaviour {

	public LevelInfo info;
	public Text Title;
	public Text HighScore;
	public Image contactImage;


	void Start()
	{
		Title.text = info.ContactName;
		if(SavingManager.PersistantData.HighScores != null && SavingManager.PersistantData.HighScores.Count >= info.LevelNumber)
		{
			HighScore.text = "HighScore: "+SavingManager.PersistantData.HighScores[info.LevelNumber-1];
		}
		else
		{
			HighScore.text = "HighScore: 0";
		}
		contactImage.sprite = info.ContactImage;

	}

	public void Level()
    {
        LoadLoadingInfo.currentLevel = info;
        SceneManager.LoadScene("Game");
    }
}
