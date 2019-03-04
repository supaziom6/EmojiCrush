using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameUI : MonoBehaviour
{
    public GameObject BackButton;
    public GameObject PauseButton;
    public GameObject Score_Text;
    public GameObject Cash_Text;
    public GameObject Shop;
    public GameObject Shop_Button;
    public GameObject Power_Up1;
    public GameObject Power_Up2;
    public void Pause()
    {
        BackButton.SetActive(true);
        PauseButton.SetActive(false);
    }
    public void Back()
    {
        BackButton.SetActive(false);
        Shop.SetActive(false);
        Shop_Button.SetActive(true);
        Power_Up1.SetActive(false);
        Power_Up2.SetActive(false);
        PauseButton.SetActive(true);
    }
    public void ShowShop()
    {
        BackButton.SetActive(true);
        Shop_Button.SetActive(false);
        Shop.SetActive(true);
        Power_Up1.SetActive(true);
        Power_Up2.SetActive(true);
        PauseButton.SetActive(false);
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
