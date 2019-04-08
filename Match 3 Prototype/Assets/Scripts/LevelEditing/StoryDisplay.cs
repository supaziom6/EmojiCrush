using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StoryDisplay : MonoBehaviour
{

    [HideInInspector]
    public SpawnIcons Initializer;
    public Image ContactImage;
    public Image PlayerImage;
    //public GameObject emoji;
    public Text contactMessage;
    //public TextMesh playerResponse;
    public Text ResponsMesage;
    public LevelInfo level;

    public GameObject contacTextBox;
    public GameObject PlayerTextBox;


    void Start()
    {
        level = LoadLoadingInfo.currentLevel;
        UpdateConversationInformation();
    }

    public void UpdateConversationInformation()
    {
        PlayerTextBox.SetActive(false);
        //emoji = i.goalEmoji;
        ContactImage.sprite = level.convo.contactImage;
        PlayerImage.sprite = level.convo.playerImage;
        contactMessage.text = ContactManager.ContactMessagesValue(level.convo.cN);
        
    }

    public void updatePostGameConvo(bool won)
    {
        if(won)
        {
            ResponsMesage.text = ContactManager.CorrectResponsesValue(level.convo.cN);
        }
        else{
            ResponsMesage.text = ContactManager.IncorrectResponsesValue(level.convo.cN);
        }
        PlayerTextBox.SetActive(true);
    }


    public void Continue()
    {

        Initializer.StartGame();
        // Put code here to make story dissapear

    }
}
