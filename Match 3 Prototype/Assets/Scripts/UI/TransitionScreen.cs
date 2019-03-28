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

    public RectTransform PhoneTop;
    public RectTransform PhoneButtom;

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
	
	// Update is called once per frame
	void Update () {

        if(hasTransitionEnded)
        {
            Rotate();
        }
        //if the game was finished by winning
        else if(hasGameFinished && gameWon)
        {
            Rotate();
        }
        else if(hasGameFinished && gameLost)
        {
            //Called twice to rotate the cube twice
            //Might cause unforseen bugs
            for(int i = 0; i < 2; i++)
            {
                Rotate();
            }
        }

    }
    public void Rotate()
    {
        float endYRotation = 90f;
        float rotateSpeed = 5f;

        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(0, endYRotation, 0), Time.deltaTime * rotateSpeed);

        //Wait 3 seconds
        StartCoroutine("Wait");
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
    IEnumerator Wait()
    {
        yield return new WaitForSeconds(3);
    }
    public void EnablePhoneUI()
    {
        PhoneTop.anchorMax =  new Vector2(1f, 0.12f);


        PhoneButtom.anchorMax = new Vector2(0f, 0.12f);

    }
    //enable the game UI once the player has transitioned to the game bit
    public void EnableGameUI()
    {
        //Code here to enable the game's UI (Score, goal emoji etc)
    }
    public void DisableGameUI()
    {
        //Code here to disable the game's UI (Score, goal emoji etc)
    }
    //Disable text's until they are needed
    //public void DisableText()
    //{
    //}
}
