using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoryDisplay : MonoBehaviour
{

    [HideInInspector]
    public SpawnIcons Initializer;
    public SpriteRenderer ContactImage;
    public SpriteRenderer PlayerImage;
    //public GameObject emoji;
    public TextMesh contactMessage;
    //public TextMesh playerResponse;
    public TextMesh correctResponse;
    public TextMesh inCorrectResponse;
    public SpriteRenderer phoneTop;
    public SpriteRenderer phoneButtom;

    public void UpdateConversationInformation(LevelInfo i)
    {
        //emoji = i.goalEmoji;
        ContactImage.sprite = i.convo.contactImage;
        PlayerImage.sprite = i.convo.playerImage;
        contactMessage.text = ContactManager.ContactMessagesValue(i.convo.cN);
        correctResponse.text = ContactManager.CorrectResponsesValue(i.convo.cN);
        inCorrectResponse.text = ContactManager.IncorrectResponsesValue(i.convo.cN);
    }
    public void UpdateUI(LevelInfo lI)
    {
        phoneTop.sprite = lI.convo.phoneTop;
        phoneButtom.sprite = lI.convo.phoneButtom;
    }

    public void Continue()
    {

        Initializer.StartGame();
        // Put code here to make story dissapear

    }
}
