using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public enum ContactNames
{
    Mom, Brother, Doctor, Boss, Colleague, Cousin, Friend, Grandma, Uber, Dominos
}
public class ContactManager
{
    public static string ContactMessagesValue(ContactNames cN)
    {
        switch (cN)
        {
            case ContactNames.Mom:
                return "Pick up the phone!";
            case ContactNames.Brother:
                return "Wanna get some food?";
            case ContactNames.Doctor:
                //return "Hello! Just letting you know your annual checkup is coming up soon";
                return "Your appointment is tomorrow";
            case ContactNames.Boss:
                //return "Please meet me tomorrow in my office";
                return "Meet me tomorrow in my office";
            case ContactNames.Colleague:
                return "Can you cover my shift tomorrow?";
            case ContactNames.Cousin:
                //return "We should hang out sometime, message me if you are available";
                return "Let's hang out";
            case ContactNames.Friend:
                //return "Wanna get pizza and watch a movie tonight?";
                return "Wanna get pizza?";
            case ContactNames.Grandma:
                //return "YOU KNOW I AM STILL ALIVE DEAR YOU SHOULD VISIT SOMETIMES";
                return "Can you visit me soon?";
            case ContactNames.Uber:
                return "Will be at your house soon";
            case ContactNames.Dominos:
                //return "What toppings would you like on your pizza?";
                return "How many do you need?";
            default:
                return null;
        }
    }
    public static string CorrectResponsesValue(ContactNames cN)
    {
        switch (cN)
        {
            case ContactNames.Mom:
                return "I will call you back soon";
            case ContactNames.Brother:
                //return "Sure, let me know where you want to eat";
                return "Sure";
            case ContactNames.Doctor:
                return "Thanks for the reminder";
            case ContactNames.Boss:
                //return "I will be in your office first thing in the morning";
                return "Sure";
            case ContactNames.Colleague:
                return "I sure can";
            case ContactNames.Cousin:
                return "We could hang out friday";
            case ContactNames.Friend:
                //return "I'll meet you at your place tonight";
                return "I'll meet you tonight";
            case ContactNames.Grandma:
                //return "I know grandma, but I have been super busy, much love";
                return "I'll visit soon";
            case ContactNames.Uber:
                return "I'll be right outside";
            case ContactNames.Dominos:
                return "Bananas and cheese";
            default:
                return null;
        }
    }
    public static string IncorrectResponsesValue(ContactNames cN)
    {
        switch (cN)
        {
            case ContactNames.Mom:
                return "I'm busy mom";
            case ContactNames.Brother:
                //return "I can't, also Mr Whiskers died, sorry to tell you this now";
                return "I can't tonight";
            case ContactNames.Doctor:
                return "I don't recognise this number";
            case ContactNames.Boss:
                return "I'm not going to work anymore";
            case ContactNames.Colleague:
                //return "No Brian, you keep on talking about Bitcoin and it honestly upsets me";
                return "I'm busy";
            case ContactNames.Cousin:
                return "I'm busy this week";
            case ContactNames.Friend:
                return "Already made plans tonight";
            case ContactNames.Grandma:
                //return "I will come sometime next week to visit";
                return "I can't come visit";
            case ContactNames.Uber:
                return "Who is this?";
            case ContactNames.Dominos:
                return "Pineapples";
            default:
                return null;
        }
    }
}
