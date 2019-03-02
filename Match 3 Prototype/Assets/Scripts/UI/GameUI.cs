using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameUI : MonoBehaviour
{
    private GameObject BackButton;
    private GameObject PauseButton;
    public void Pause()
    {
        BackButton.SetActive(true);
        PauseButton.SetActive(false);
    }
    public void Back()
    {
        BackButton.SetActive(false);
        PauseButton.SetActive(true);
    }
    public void ShowPowerUps()
    {
        BackButton.SetActive(false);
        PauseButton.SetActive(false);
    }

    public void showUI(ButtonActionRequest[] itemsTurnedon)
    {
        return;
    }
}
