using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TransitionScreen : MonoBehaviour {

    StoryDisplay sD;

    public GameObject cube;
    public Text contactNameText;
    public Text contactMessageText;
    public Button continueButton;
    public RectTransform MasterUI;
    

    //Flags
    private bool hasCubeRotated = false;
    private bool hasTransitionEnded = false;
    private bool hasGameFinished = false;
    private bool gameWon = false;
    private bool gameLost = false;


	// Use this for initialization
	void Start () {
        sD = GetComponent<StoryDisplay>();
        //Disable button on startup
        continueButton.enabled = true;
        hasCubeRotated = false;
    }
	
    public void Rotate()
    {
        //Wait 3 seconds
        StartCoroutine(RotateTo(transform, Quaternion.Euler(0, 90, 0), 1));
        hasCubeRotated = true;
        continueButton.enabled = false;
    }
    public void StoryScene()
    {
        continueButton.enabled = true; 

        if (hasCubeRotated)
        {
            hasTransitionEnded = true;
        }
    }
    public void GameScene()
    {
        EnableGameUI();

    }
    public void WinGameScene()
    {
        DisableGameUI();
    }
    public void LostGameScene()
    {
        DisableGameUI();
    }
    //Must be called after either the player loses or wins
    public void ResetRotation()
    {
        float endYRotation = 180f;
        float rotateSpeed = 5f;

        //Might need tweeking
        //if this method is called, it should should always reset the cube to the story bit
        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(0, endYRotation, 0), Time.deltaTime * rotateSpeed);
    }
    IEnumerator RotateTo(Transform rotator, Quaternion endrotation, float TimeToRotate)
    {
        Quaternion baseRotation = rotator.rotation;
        for(float i = 0; i < TimeToRotate ; i += Time.deltaTime)
        {
            print("Now I'mHere");
            rotator.rotation = Quaternion.Lerp(baseRotation, endrotation, i/TimeToRotate);
            yield return new WaitForSeconds(Time.deltaTime);
        }
        rotator.rotation = endrotation;
        print("finished rotating");
        EnableGameUI();
        sD.Continue();
        
    }
    //enable the game UI once the player has transitioned to the game bit
    public void EnableGameUI()
    {
        // Set MasterUI Y anchors to min 0 max 1 
        //Code here to enable the game's UI (Score, goal emoji etc)
        MasterUI.anchorMax = new Vector2(1,1);
        MasterUI.anchorMin = new Vector2(0,0);
        MasterUI.sizeDelta = new Vector2(1000,1000);
        print("UI is about to move");
        StartCoroutine(MoveUIIntoPosition());
        

    }
    public void DisableGameUI()
    {
        //Set MAasterUI Y ancors to 2
        MasterUI.anchorMax = new Vector2(1,2);
        MasterUI.anchorMin = new Vector2(0,2);
        MasterUI.sizeDelta = new Vector2(500,500);
        StartCoroutine(MoveUIIntoPosition());
        //Code here to disable the game's UI (Score, goal emoji etc)
    }


    IEnumerator MoveUIIntoPosition()
    {
        float TimetoMove = 2;
        Vector2 BaseSize = MasterUI.sizeDelta;
        for(float i = 0; i < TimetoMove ; i += Time.deltaTime)
        {
            print("Now I'mHere");
            MasterUI.sizeDelta = Vector2.Lerp(BaseSize, Vector2.zero, i/TimetoMove);
            yield return new WaitForSeconds(Time.deltaTime);
        }
        MasterUI.sizeDelta = Vector2.zero;

    }
    //Disable text's until they are needed
    //public void DisableText()
    //{
    //}
}
