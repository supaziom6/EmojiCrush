using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenuHandler : MonoBehaviour
{
    // Main Menu Buttons
    public GameObject LevelButtons; // buttons for the levels
    public GameObject Levels; // button to select levels
    public GameObject LevelButtonParent;
    public GameObject LevelButtonPrefab;
    public GameObject ReturnButton;
    public GameObject OptionsButton;
    public GameObject CreditsButton;
    public GameObject Credits;
    public GameObject ExitButton;
    public GameObject ShopButton;
    public GameObject TutorialButton;
    public GameObject NightMode;
    // Parent Objects
    public GameObject OptionsMenu;
    // Sprites
    public GameObject BackgroundImage;
    public GameObject DiamondImage;
    public GameObject OptionsBackground;

    public List<LevelInfo> LevelInfos;

    void Start()
    {
        int length = LevelInfos.Count;
        int highestLevelcomplete = SavingManager.PersistantData.HighestLevelComplete;

        for(int i = 0; i < length; i++)
        {
            
            GameObject temp = Instantiate(LevelButtonPrefab);
            temp.GetComponent<RectTransform>().sizeDelta =new Vector2((Screen.width*0.7f) ,(Screen.height*0.1f));
            temp.transform.SetParent(LevelButtonParent.transform);
            temp.transform.localScale = Vector3.one;
            if(highestLevelcomplete+1 < LevelInfos[i].LevelNumber)
            {
                temp.GetComponent<Button>().interactable = false;
            }

            temp.GetComponent<LevelButton>().info = LevelInfos[i];
        }
    }


    // Methods for Menu
    public void LevelsMenu()
    {
        Levels.SetActive(false);
        Credits.SetActive(false);
        CreditsButton.SetActive(false);
        ExitButton.SetActive(false);
        DiamondImage.SetActive(false);
        ShopButton.SetActive(false);
        TutorialButton.SetActive(false);
        LevelButtons.SetActive(true);
        OptionsBackground.SetActive(true);
        ReturnButton.SetActive(true);
    }
    public void Options()
    {
        NightMode.SetActive(false);
        Levels.SetActive(false);
        Credits.SetActive(false);
        CreditsButton.SetActive(false);
        DiamondImage.SetActive(false);
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
        Credits.SetActive(false);
        CreditsButton.SetActive(true);
        LevelButtons.SetActive(false);
        ShopButton.SetActive(true);
        TutorialButton.SetActive(true);
        Levels.SetActive(true);
        OptionsButton.SetActive(true);
        OptionsMenu.SetActive(false);
        ExitButton.SetActive(true);
        DiamondImage.SetActive(true);
        ReturnButton.SetActive(false);
        NightMode.SetActive(true);
    }
    public void ShowCredits()
    {
        OptionsBackground.SetActive(false);
        NightMode.SetActive(false);
        Credits.SetActive(true);
        CreditsButton.SetActive(false);
        LevelButtons.SetActive(false);
        ShopButton.SetActive(false);
        TutorialButton.SetActive(false);
        Levels.SetActive(false);
        OptionsButton.SetActive(false);
        OptionsMenu.SetActive(false);
        ExitButton.SetActive(false);
        DiamondImage.SetActive(false);
        ReturnButton.SetActive(true);
    }
    public void Shop()
    {
        SceneManager.LoadScene("Shop");
    }
}
