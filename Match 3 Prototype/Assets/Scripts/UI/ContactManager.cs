using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

// [System.Serializable]
// public struct ContactDetails
// {
//     //public TextMesh contactName;
//     //public TextMesh contactMessage;
//     //public TextMesh playerResponse;
//     //public Image contactImage;
//     //public Image playerImage;
//     //public Image PlayerResponseEmoji;
// }

[System.Serializable]
public enum ContactNames
{
    Mom,Brother,Doctor,Boss,Colleague,Cousin,Friend,GrandMa,Uber,Dominos
}

[System.Serializable]
public enum ContactMessages
{
    MomMessage,BrotherMessage,DoctorMessage,BossMessage,ColleagueMessage,CousinMessage,FriendMessage,GrandmaMessage,UberMessage,DominosMessage
}

[System.Serializable]
public enum CorrectResponses
{
    CorrectResponseToMom,CorrectResponseToBrother,CorrectResponseToDoctor,CorrectResponseToBoss,CorrectResponseToColleague,
    CorrectResponseToCousin,CorrectResponseToFriend,CorrectResponseToGrandma,CorrectResponseToUber,CorrectResponseToDominos
}
[System.Serializable]
public enum IncorrectResponses
{
    IncorrectResponseToMom,IncorrectResponseToBrother,IncorrectResponseToDoctor,IncorrectResponseToBoss,IncorrectResponseToColleague,
    IncorrectResponseToCousin,IncorrectResponseToFriend,IncorrectResponseToGrandma,IncorrectResponseToUber,IncorrectResponseToDominos
}

[CreateAssetMenu(fileName = "ContactManager", menuName = "LevelEditor", order = 0)]
[System.Serializable]
public class ContactManager : ScriptableObject
{

    //Contacts mycontact;
    /// <summary>
    ///	Used to create contact information for each level
    /// </summary>
    [Header("Contact Info")]
    public Image contactImage;
    public TextMesh contactName;
    public TextMesh contactMessage;

[Header("Player Info")]
    public Image playerImage;
    public TextMesh playerResponse;
    public TextMesh ResponseEmoji;
}

public static class myClass
{
    public static string ContactMessagesValue(ContactMessages cM)
    {
        switch (cM)
        {
            case ContactMessages.MomMessage:
                return "Pick up the phone!";
            case ContactMessages.BrotherMessage:
                return "Wanna get some food?";
            case ContactMessages.DoctorMessage:
                return "Hello! Just letting you know your annual checkup is coming up soon";
            case ContactMessages.BossMessage:
                return "Please meet me tomorrow in my office";
            case ContactMessages.ColleagueMessage:
                return "Can you cover my shift tomorrow?";
            case ContactMessages.CousinMessage:
                return "We should hang out sometime, message me if you are available";
            case ContactMessages.FriendMessage:
                return "Wanna get pizza and watch a movie tonight?";
            case ContactMessages.GrandmaMessage:
                return "YOU KNOW I AM STILL ALIVE DEAR YOU SHOULD VISIT SOMETIMES";
            case ContactMessages.UberMessage:
                return "Will be at your house soon";
            case ContactMessages.DominosMessage:
                return "What toppings would you like on your pizza?";
            default:
                return null;
        }
    }
    public static string CorrectResponsesValue(CorrectResponses cR)
    {
        switch(cR)
        {
            case CorrectResponses.CorrectResponseToMom:
                return "I will call you back soon";
            case CorrectResponses.CorrectResponseToBrother:
                return "Sure, let me know where you want to eat";
            case CorrectResponses.CorrectResponseToDoctor:
                return "Thanks for the reminder";
            case CorrectResponses.CorrectResponseToBoss:
                return "I will be in your office first thing in the morning";
            case CorrectResponses.CorrectResponseToColleague:
                return "I sure can";
            case CorrectResponses.CorrectResponseToCousin:
                return "We could hang out friday";
            case CorrectResponses.CorrectResponseToFriend:
                return "I'll meet you at your place tonight";
            case CorrectResponses.CorrectResponseToGrandma:
                return "I know grandma, but I have been super busy, much love";
            case CorrectResponses.CorrectResponseToUber:
                return "I'll be right outside";
            case CorrectResponses.CorrectResponseToDominos:
                return "Bananas and cheese";
            default:
                return null;
        }
    }
    public static string IncorrectResponsesValue(IncorrectResponses iR)
    {
        switch(iR)
        {
            case IncorrectResponses.IncorrectResponseToMom:
                return "I'm busy mom";
            case IncorrectResponses.IncorrectResponseToBrother:
                return "I can't, also Mr Whiskers died, sorry to tell you this now";
            case IncorrectResponses.IncorrectResponseToDoctor:
                return "I don't recognise this number";
            case IncorrectResponses.IncorrectResponseToBoss:
                return "I'm not going to work anymore";
            case IncorrectResponses.IncorrectResponseToColleague:
                return "No Brian, you keep on talking about Bitcoin and it honestly upsets me";
            case IncorrectResponses.IncorrectResponseToCousin:
                return "I'm busy this week";
            case IncorrectResponses.IncorrectResponseToFriend:
                return "Already made plans tonight";
            case IncorrectResponses.IncorrectResponseToGrandma:
                return "I will come sometime next week to visit";
            case IncorrectResponses.IncorrectResponseToUber:
                return "Who is this?";
            case IncorrectResponses.IncorrectResponseToDominos:
                return "Pineapples";
            default:
                return null;
        }
    }
}
