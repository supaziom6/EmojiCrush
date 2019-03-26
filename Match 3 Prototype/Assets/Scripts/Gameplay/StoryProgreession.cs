using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


/// <summary>
///  This script handles how story gets played, It both initializes the begining andd ending part of the story
/// </summary>
public class StoryProgreession : MonoBehaviour {


	/// <summary>
	///  Reference to the main Game Script Which allows for the game to start and receive callbacks when the game is finished
	/// </summary>
	public SpawnIcons Initializer;
	/// <summary>
	/// Temporary Screen for the end of the game
	/// </summary>
	public GameObject EndGameScreen;
	public Button nextLevel;
	public TextMeshProUGUI Score;
	public TextMeshProUGUI EndGameTitle;
	private bool EndGameHandeled;
	// Use this for initialization
	void Start () {
		// Take stuff from the story here and initialize it

		startGame();
	}

	/// <summary>
	///  when everything is done call game start to begin the game phase
	/// </summary>
	void startGame()
	{
		Initializer.StartGame();
	}

	void Update()
	{
		if(!EndGameHandeled&&TextManager.LevelEnded)
		{
			if(TextManager.WonTheGame)
			{
				int currentLevelNumber = LoadLoadingInfo.currentLevel.LevelNumber;

				SavingManager.PersistantData.HighestLevelComplete = currentLevelNumber;
				if(SavingManager.PersistantData.HighScores == null)
				{
					SavingManager.PersistantData.HighScores = new List<int>();
					SavingManager.PersistantData.HighScores.Add(Initializer.UIController.Score);
				}
				else if(SavingManager.PersistantData.HighScores.Count < currentLevelNumber)
				{
					SavingManager.PersistantData.HighScores.Add(Initializer.UIController.Score);
				}
				else	
				{
					SavingManager.PersistantData.HighScores[currentLevelNumber-1]  = Initializer.UIController.Score;
				}
				EndGameTitle.text = "Winner";
			}
			else
			{
				EndGameTitle.text = "You Lost";
			}

			if(LoadLoadingInfo.currentLevel.LevelNumber < SavingManager.PersistantData.HighestLevelComplete || LoadLoadingInfo.currentLevel.nextLevel == null)
			{
				nextLevel.interactable = false;
			}
			Score.text = Initializer.UIController.Score.ToString();

			// Put end game code here based on Initializer.UIController.GoalReached
			EndGameHandeled = true;
			EndGameScreen.SetActive(true);
		}
	}

	/// <summary>
	/// User decides to goback to mainmenu
	/// </summary>
	public void endLevelWithMainMenu()
	{
		SceneManager.LoadScene("MainMenu");
	}

	/// <summary>
	/// Users decides to replay the level
	/// </summary>
	public void endLevelWithRetry()
	{
		SceneManager.LoadScene(SceneManager.GetActiveScene().name);
	}

	/// <summary>
	/// User decides to continue to the next level
	/// </summary>
	public void endLevelWithNextLevel()
	{
		LoadLoadingInfo.currentLevel = LoadLoadingInfo.currentLevel.nextLevel;
		SceneManager.LoadScene(SceneManager.GetActiveScene().name);
	}


	

}
