using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ContactManager : MonoBehaviour {

    //Public arrays
    public string[] contactName = new string[10];
    public string[] contactMessage = new string[10];
    public string[] correctResponses = new string[20];
    public string[] incorrectResponses = new string[20];

    //Text
    public Text conName;
    public Text conMessage;
    public Text playerCorrectResponse;
    public Text playerIncorrectResponse;

    //Game state flag - THIS IS ONLY TEMPORARY FLAG
    bool hasWon = false;

    //Buttons
    public GameObject continueButton;
    //TEST
    int testScene;
    // Use this for initialization
    void Start () {
        //TEST
        testScene = SceneManager.GetActiveScene().buildIndex;
    }
	
	// Update is called once per frame
	void Update () {
        ContactNames();
        ContactMessage();
        UpdateText();
        print(testScene);
	}
    public void ContactNames()
    {
        contactName = new string[]
        {
            "Mom",
            "Brother",
            "Doctor",
            "Boss",
            "Colleague",
            "Cousin",
            "Friend",
            "GrandMa",
            "Uber",
            "Dominos",
        };
    }
    public void ContactMessage()
    {
        for(int i = 0; i < contactName.Length; i++)
        {
            //Mom
            if(contactName[i].Equals("Mom"))
            {
                contactMessage[0] = "Pick up the phone!";
            }
            //Brother
            else if (contactName[i].Equals("Brother"))
            {
                contactMessage[1] = "Wanna get some food?";
            }
            //Doctor
            else if (contactName[i].Equals("Doctor"))
            {
                contactMessage[2] = "Hello! Just letting you know your annual checkup is coming up soon";
            }
            //Boss
            else if (contactName[i].Equals("Boss"))
            {
                contactMessage[3] = "Please meet me tomorrow in my office";
            }
            //Colleague
            else if (contactName[i].Equals("Colleague"))
            {
                contactMessage[4] = "Can you cover my shift tomorrow?";
            }
            //Cousin
            else if (contactName[i].Equals("Cousin"))
            {
                contactMessage[5] = "We should hang out sometime, message me if you are available";
            }
            //Friend
            else if (contactName[i].Equals("Friend"))
            {
                contactMessage[6] = "Wanna get pizza and watch a movie tonight?";
            }
            //Grandma
            else if (contactName[i].Equals("GrandMa"))
            {
                contactMessage[7] = "YOU KNOW I AM STILL ALIVE DEAR YOU SHOULD VISIT SOMETIMES";
            }
            //Uber
            else if (contactName[i].Equals("Uber"))
            {
                contactMessage[8] = "Will be at your house soon";
            }
            //Dominos
            else if (contactName[i].Equals("Dominos"))
            {
                contactMessage[9] = "What toppings would you like on your pizza?";
            }
        }
    }
    public void CorrectResponses()
    {
        for(int i = 0; i < correctResponses.Length; i++)
        {
            //Mom
            if (contactName[i].Equals("Mom") && hasWon)
            {
                correctResponses[0] = "I will call you back soon";
            }
            //Brother
            else if (contactName[i].Equals("Brother") && hasWon)
            {
                correctResponses[1] = "Sure, let me know where you want to eat";
            }
            //Doctor
            else if (contactName[i].Equals("Doctor") && hasWon)
            {
                correctResponses[2] = "Thanks for the reminder";
            }
            //Boss
            else if (contactName[i].Equals("Boss") && hasWon)
            {
                correctResponses[3] = "I will be in your office first thing in the morning";
            }
            //Colleague
            else if (contactName[i].Equals("Colleague") && hasWon)
            {
                correctResponses[4] = "I sure can";
            }
            //Cousin
            else if (contactName[i].Equals("Cousin") && hasWon)
            {
                correctResponses[5] = "We could hang out friday";
            }
            //Friend
            else if (contactName[i].Equals("Friend") && hasWon)
            {
                correctResponses[6] = "I'll meet you at your place tonight";
            }
            //Grandma
            else if (contactName[i].Equals("GrandMa") && hasWon)
            {
                correctResponses[7] = "I know grandma, but I have been super busy, much love";
            }
            //Uber
            else if (contactName[i].Equals("Uber") && hasWon)
            {
                correctResponses[8] = "I'll be right outside";
            }
            //Dominos
            else if (contactName[i].Equals("Dominos") && hasWon)
            {
                correctResponses[9] = "Bananas and cheese";
            }
        }
    }
    public void IncorrectResponses()
    {
        for(int i = 0; i < incorrectResponses.Length; i++)
        {
            //Mom
            if (contactName[i].Equals("Mom") && hasWon == false)
            {
                incorrectResponses[0] = "I'm busy mom";
            }
            //Brother
            else if (contactName[i].Equals("Brother") && hasWon == false)
            {
                incorrectResponses[1] = "I can't, also Mr Whiskers died, sorry to tell you this now";
            }
            //Doctor
            else if (contactName[i].Equals("Doctor") && hasWon == false)
            {
                incorrectResponses[2] = "I don't recognise this number";
            }
            //Boss
            else if (contactName[i].Equals("Boss") && hasWon == false)
            {
                correctResponses[3] = "I'm not going to work anymore";
            }
            //Colleague
            else if (contactName[i].Equals("Colleague") && hasWon == false)
            {
                incorrectResponses[4] = "No Brian, you keep on talking about Bitcoin and it honestly upsets me";
            }
            //Cousin
            else if (contactName[i].Equals("Cousin") && hasWon == false)
            {
                incorrectResponses[5] = "I'm busy this week";
            }
            //Friend
            else if (contactName[i].Equals("Friend") && hasWon == false)
            {
                incorrectResponses[6] = "Already made plans tonight";
            }
            //Grandma
            else if (contactName[i].Equals("GrandMa") && hasWon == false)
            {
                incorrectResponses[7] = "I will come sometime next week to visit";
            }
            //Uber
            else if (contactName[i].Equals("Uber") && hasWon == false)
            {
                incorrectResponses[8] = "Who is this?";
            }
            //Dominos
            else if (contactName[i].Equals("Dominos") && hasWon == false)
            {
                incorrectResponses[9] = "Pineapples";
            }
        }
    }
    public void UpdateText()
    {
        //Mom
        if (testScene == 1)
        {
            conName.text = contactName[0].ToString();
            conMessage.text = contactMessage[0].ToString();
        }
        //Brother
        if (testScene == 2)
        {
            conName.text = contactName[1].ToString();
            conMessage.text = contactMessage[1].ToString();
        }
        //Doctor
        if (testScene == 3)
        {
            conName.text = contactName[2].ToString();
            conMessage.text = contactMessage[2].ToString();
        }
        //Boss
        if (testScene == 4)
        {
            conName.text = contactName[3].ToString();
            conMessage.text = contactMessage[3].ToString();
        }
        //Colleague
        if (testScene == 5)
        {
            conName.text = contactName[4].ToString();
            conMessage.text = contactMessage[4].ToString();
        }
        //Cousin
        if (testScene == 6)
        {
            conName.text = contactName[5].ToString();
            conMessage.text = contactMessage[5].ToString();
        }
        //Friend
        if (testScene == 7)
        {
            conName.text = contactName[6].ToString();
            conMessage.text = contactMessage[6].ToString();
        }
        //GrandMa
        if (testScene == 8)
        {
            conName.text = contactName[7].ToString();
            conMessage.text = contactMessage[7].ToString();
        }
        //Uber
        if (testScene == 9)
        {
            conName.text = contactName[8].ToString();
            conMessage.text = contactMessage[8].ToString();
        }
        //Dominos
        if (testScene == 10)
        {
            conName.text = contactName[9].ToString();
            conMessage.text = contactMessage[9].ToString();
        }
    }
}
