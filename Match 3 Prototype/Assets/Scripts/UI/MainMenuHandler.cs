using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuHandler : MonoBehaviour
{
    // Main Menu Buttons
    public GameObject LevelButtons; // buttons for the levels
    public GameObject Levels; // button to select levels
    public GameObject ReturnButton;
    public GameObject OptionsButton;
    public GameObject ExitButton;
    public GameObject ShopButton;
    public GameObject TutorialButton;
    // Parent Objects
    public GameObject OptionsMenu;
    // Sprites
    public GameObject BackgroundImage;
    public GameObject OptionsBackground;

    // Methods for Menu
    public void LevelsMenu()
    {
        Levels.SetActive(false);
        ExitButton.SetActive(false);
        ShopButton.SetActive(false);
        TutorialButton.SetActive(false);
        LevelButtons.SetActive(true);
        OptionsBackground.SetActive(false);
        ReturnButton.SetActive(true);
    }
    public void Options()
    {
        Levels.SetActive(false);
        OptionsButton.SetActive(false);
        ShopButton.SetActive(false);
        ExitButton.SetActive(false);
        LevelButtons.SetActive(false);
        TutorialButton.SetActive(false);
        OptionsMenu.SetActive(true);
        OptionsBackground.SetActive(true);
        ReturnButton.SetActive(true);
    }
    public void Exit()
    {
        Application.Quit();
    }
    public void Return()
    {
        OptionsBackground.SetActive(false);
        LevelButtons.SetActive(false);
        ShopButton.SetActive(true);
        TutorialButton.SetActive(true);
        Levels.SetActive(true);
        OptionsButton.SetActive(true);
        OptionsMenu.SetActive(false);
        ExitButton.SetActive(true);
        ReturnButton.SetActive(false);
    }
    public void Level()
    {
        SceneManager.LoadScene("Game");
    }
}
