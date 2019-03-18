using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


/// <summary>
///  This script handles how story gets played, It both initializes the begining andd ending part of the story
/// </summary>
public class StoryProgreession : MonoBehaviour {


	/// <summary>
	///  Reference to the main Game Script Which allows for the game to start and receive callbacks when the game is finished
	/// </summary>
	public SpawnIcons Initializer;

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
		if(TextManager.LevelEnded)
		{
			// Put end game code here based on Initializer.UIController.GoalReached
			endLevelWithMainMenu();
		}
	}

	/// <summary>
	/// User decides to goback to mainmenu
	/// </summary>
	void endLevelWithMainMenu()
	{
		SceneManager.LoadScene("MainMenu");
	}

	/// <summary>
	/// Users decides to replay the level
	/// </summary>
	void endLevelWithRetry()
	{
		SceneManager.LoadScene(SceneManager.GetActiveScene().name);
	}

	/// <summary>
	/// User decides to continue to the next level
	/// </summary>
	void endLevelWithNextLevel()
	{
		LoadLoadingInfo.currentLevel = LoadLoadingInfo.currentLevel.nextLevel;
		SceneManager.LoadScene(SceneManager.GetActiveScene().name);
	}


	

}
