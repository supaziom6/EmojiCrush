using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tutorial : MonoBehaviour {

    public GameObject MatchingContinue, UIContinue, Match4Continue, Match5Continue, PowerUpsContinue,Title;
    public GameObject tutorial_1, tutorial_2, tutorial_3, tutorial_4, tutorial_5, PlayButton,TutorialButton,ShopButton;


    public void TutorialStart()
    {
        Time.timeScale = 1;
        if(PlayButton != null)
            PlayButton.SetActive(false);
        TutorialButton.SetActive(false);
        if (ShopButton != null)
            ShopButton.SetActive(false);
        if (Title != null)
            Title.SetActive(false);
        tut_1();
    }


   public void tut_1()
    {
        tutorial_1.SetActive(true);
        MatchingContinue.SetActive(true);
    }

    public void tut_2()
    {
        tutorial_1.SetActive(false);
        MatchingContinue.SetActive(false);
        tutorial_2.SetActive(true);
        UIContinue.SetActive(true);
    }

    public void tut_3()
    {
        tutorial_2.SetActive(false);
        UIContinue.SetActive(false);
        tutorial_3.SetActive(true);
        Match4Continue.SetActive(true);
    }

    public void tut_4()
    {
        tutorial_3.SetActive(false);
        Match4Continue.SetActive(false);
        tutorial_4.SetActive(true);
        Match5Continue.SetActive(true);
    }

    public void tut_5()
    {
        Match5Continue.SetActive(false);
        tutorial_4.SetActive(false);
        tutorial_5.SetActive(true);
        PowerUpsContinue.SetActive(true);
    }

    public void tut_over()
    {
        tutorial_5.SetActive(false);
        PowerUpsContinue.SetActive(false);
        if(PlayButton != null)
            PlayButton.SetActive(true);
        TutorialButton.SetActive(true);
        if (ShopButton != null)
            ShopButton.SetActive(true);
        if (Title != null)
            Title.SetActive(true);
        Time.timeScale = 0;
    }
}
