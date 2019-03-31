using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StoryDisplay : MonoBehaviour
{

    [HideInInspector]
    public SpawnIcons Initializer;
    public SpriteRenderer ContactImage;
    public SpriteRenderer PlayerImage;
    //public GameObject emoji;
    public Text contactMessage;
    //public TextMesh playerResponse;
    public Text correctResponse;
    public Text inCorrectResponse;


    public void UpdateConversationInformation(LevelInfo i)
    {
        //emoji = i.goalEmoji;
        ContactImage.sprite = i.convo.contactImage;
        PlayerImage.sprite = i.convo.playerImage;
        contactMessage.text = ContactManager.ContactMessagesValue(i.convo.cN);
        correctResponse.text = ContactManager.CorrectResponsesValue(i.convo.cN);
        inCorrectResponse.text = ContactManager.IncorrectResponsesValue(i.convo.cN);
    }


    public void Continue()
    {

        Initializer.StartGame();
        // Put code here to make story dissapear

    }
}
