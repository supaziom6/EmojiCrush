using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TransitionScreen : MonoBehaviour {

    StoryDisplay sD;
    public delegate void callback();
    public callback displayPostGameScreen;
    public GameObject cube;
    public Button continueButton;
    public Button continueEndButton;
    public RectTransform MasterUI;
    

    // Flags
    private bool doneHidingUI;
    //private bool hasGameFinished = false;
    //private bool gameWon = false;
    //private bool gameLost = false;



	// Use this for initialization
	void Start () {
        sD = GetComponent<StoryDisplay>();
        //Disable button on startup
        MasterUI.anchorMax = new Vector2(1,2);
        MasterUI.anchorMin = new Vector2(0,2);
        MasterUI.sizeDelta = new Vector2(500,500);
    }
	
    public void Rotate()
    {
        if(TextManager.LevelEnded)
        {
            StartCoroutine(RotateTo(transform, Quaternion.Euler(0, 0, 0), 1,true));
            sD.updatePostGameConvo(TextManager.WonTheGame);
        }
        else
        {
            StartCoroutine(RotateTo(transform, Quaternion.Euler(0, 90, 0), 1, false));
            
        }
        
    }

    public void displayPostGame()
    {
        displayPostGameScreen();
    }

    IEnumerator RotateTo(Transform rotator, Quaternion endrotation, float TimeToRotate, bool goingOut)
    {
        if(!goingOut)
        {
            
            Quaternion baseRotation = rotator.rotation;
            for(float i = 0; i < TimeToRotate ; i += Time.deltaTime)
            {
                rotator.rotation = Quaternion.Lerp(baseRotation, endrotation, i/TimeToRotate);
                yield return new WaitForSeconds(Time.deltaTime);
            }
            rotator.rotation = endrotation;
            continueButton.gameObject.SetActive(false);
            EnableGameUI();
        }
        else
        {
            DisableGameUI();
            doneHidingUI = false;
            yield return new WaitUntil(DoneHidingUI);
            Quaternion baseRotation = rotator.rotation;
            for(float i = 0; i < TimeToRotate ; i += Time.deltaTime)
            {
                rotator.rotation = Quaternion.Lerp(baseRotation, endrotation, i/TimeToRotate);
                yield return new WaitForSeconds(Time.deltaTime);
            }
            rotator.rotation = endrotation;
            continueEndButton.gameObject.SetActive(true);
        }
        
        
    }
    //enable the game UI once the player has transitioned to the game bit
    public void EnableGameUI()
    {
        // Set MasterUI Y anchors to min 0 max 1 
        //Code here to enable the game's UI (Score, goal emoji etc)
        MasterUI.anchorMax = new Vector2(1,1);
        MasterUI.anchorMin = new Vector2(0,0);
        MasterUI.sizeDelta = new Vector2(500,500);
        print("UI is about to move");
        StartCoroutine(MoveUIIntoPosition(false));
        

    }
    public void DisableGameUI()
    {
        //Set MAasterUI Y ancors to 2
        Vector3 temp = MasterUI.sizeDelta;
        MasterUI.anchorMax = new Vector2(1,2);
        MasterUI.anchorMin = new Vector2(0,2);
        MasterUI.sizeDelta = temp;
        StartCoroutine(MoveUIIntoPosition(true));
        //Code here to disable the game's UI (Score, goal emoji etc)
    }


    IEnumerator MoveUIIntoPosition(bool gameOver)
    {
        float TimetoMove = 0.2f;
        Vector2 BaseSize = MasterUI.sizeDelta;
        for(float i = 0; i < TimetoMove ; i += Time.deltaTime)
        {
            print(i+" < "+TimetoMove);
            MasterUI.sizeDelta = Vector2.Lerp(BaseSize, Vector2.zero, i/TimetoMove);
            yield return new WaitForSeconds(Time.deltaTime);
        }
        MasterUI.sizeDelta = Vector2.zero;
        if(!gameOver)
        {
            sD.Continue();
        }
        else
        {
            sD.Initializer.ParentGameBoard.SetActive(false);
        }
        doneHidingUI = true;
    }

    bool DoneHidingUI()
    {
        return doneHidingUI;
    }
    //Disable text's until they are needed
    //public void DisableText()
    //{
    //}
}
