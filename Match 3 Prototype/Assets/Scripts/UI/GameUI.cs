using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameUI : MonoBehaviour
{
    public static bool Paused = false;
    public static bool EmojiCrushActivated = false;
    public GameObject BackButton;
    public GameObject PauseButton;
    public GameObject MainMenuButton;
    public GameObject TutorialButton;
    public GameObject PauseMenu;
    public GameObject Score_Text;
    public GameObject Cash_Text;
    public GameObject Shop;
    public GameObject Shop_Button;
    public GameObject Power_Up1;
    public Text powerUp1Ammount;
    public GameObject Power_Up2;
    public Text powerUp2Ammount;
    public GameObject EmojiCrushExplanation;

    public void Awake()
    {
        Time.timeScale = 1;
        Paused = false;
        powerUp1Ammount.text = SavingManager.PersistantData.AutoCorrectsOwned.ToString();
        powerUp2Ammount.text = SavingManager.PersistantData.EmojiCrushOwned.ToString();
        if(SavingManager.PersistantData.AutoCorrectsOwned == 0)
        {
            Power_Up1.GetComponent<Button>().interactable = false;
        }
        else
        {
            Power_Up1.GetComponent<Button>().interactable = true;
        }
        if(SavingManager.PersistantData.EmojiCrushOwned == 0)
        {
            Power_Up2.GetComponent<Button>().interactable = false;
        }
        else
        {
            Power_Up2.GetComponent<Button>().interactable = true;
        }
    }

    public void Pause()
    {
        Time.timeScale = 0;
        Paused = true;
        BackButton.SetActive(true);
        MainMenuButton.SetActive(true);
        TutorialButton.SetActive(true);       
        PauseMenu.SetActive(true);
        PauseButton.SetActive(false);
    }
    public void Back()
    {
        Time.timeScale = 1;
        Paused = false;
        BackButton.SetActive(false);
        MainMenuButton.SetActive(false);
        TutorialButton.SetActive(false);     
        Shop.SetActive(false);
        Shop_Button.SetActive(true);
        Power_Up1.SetActive(false);
        PauseMenu.SetActive(false);
        Power_Up2.SetActive(false);
        PauseButton.SetActive(true);
        EmojiCrushExplanation.SetActive(false);
    }
    public void ShowShop()
    {
        Time.timeScale = 0;
        Paused = true;
        BackButton.SetActive(true);
        Shop_Button.SetActive(false);
        Shop.SetActive(true);
        Power_Up1.SetActive(true);
        Power_Up2.SetActive(true);
        PauseButton.SetActive(false);
    }
    public void ReturnToMainMenu()
    {
        Time.timeScale = 1;
        SavingManager.Save();
        SceneManager.LoadScene("MainMenu");
    }
    public void PowerUp1()
    {
        Shop.SetActive(false);
        Power_Up1.SetActive(false);
        PauseMenu.SetActive(false);
        Power_Up2.SetActive(false);
        SavingManager.PersistantData.AutoCorrectsOwned -= 1;
        powerUp1Ammount.text = SavingManager.PersistantData.AutoCorrectsOwned.ToString();
        if(SavingManager.PersistantData.AutoCorrectsOwned == 0)
        {
            Power_Up1.GetComponent<Button>().interactable = false;
        }
        else
        {
            Power_Up1.GetComponent<Button>().interactable = true;
        }
        SavingManager.Save();
        Back();
    }
    public void PowerUp2()
    {
        Shop.SetActive(false);
        Power_Up1.SetActive(false);
        PauseMenu.SetActive(false);
        Power_Up2.SetActive(false);
        EmojiCrushExplanation.SetActive(true);
        EmojiCrushActivated = true;
        SavingManager.PersistantData.EmojiCrushOwned -= 1;
        powerUp2Ammount.text = SavingManager.PersistantData.EmojiCrushOwned.ToString();
        if(SavingManager.PersistantData.EmojiCrushOwned == 0)
        {
            Power_Up2.GetComponent<Button>().interactable = false;
        }
        else
        {
            Power_Up2.GetComponent<Button>().interactable = true;
        }
        
    }

    public void cancelPowerUp()
    {
        SavingManager.PersistantData.EmojiCrushOwned += 1;
        powerUp2Ammount.text = SavingManager.PersistantData.EmojiCrushOwned.ToString();
        Shop.SetActive(true);
        Power_Up1.SetActive(true);
        Power_Up2.SetActive(true);
        EmojiCrushExplanation.SetActive(false);
        EmojiCrushActivated = false;
    }
    

}
