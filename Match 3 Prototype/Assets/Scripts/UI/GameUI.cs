using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameUI : MonoBehaviour
{
    public static bool Paused = false;
    public GameObject BackButton;
    public GameObject PauseButton;
    public GameObject MainMenuButton;
    public GameObject PauseMenu;
    public GameObject Score_Text;
    public GameObject Cash_Text;
    public GameObject Shop;
    public GameObject Shop_Button;
    public GameObject Power_Up1;
    public GameObject Power_Up2;

    public void Pause()
    {
        Paused = true;
        BackButton.SetActive(true);
        MainMenuButton.SetActive(true);
        PauseMenu.SetActive(true);
        PauseButton.SetActive(false);
    }
    public void Back()
    {
        Paused = false;
        BackButton.SetActive(false);
        MainMenuButton.SetActive(false);
        Shop.SetActive(false);
        Shop_Button.SetActive(true);
        Power_Up1.SetActive(false);
        PauseMenu.SetActive(false);
        Power_Up2.SetActive(false);
        PauseButton.SetActive(true);
    }
    public void ShowShop()
    {
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
        SceneManager.LoadScene("MainMenu");
    }
    public void PowerUp1()
    {
        //Do Something Here
    }
    public void PowerUp2()
    {
        //Do Something Here
    }
}
