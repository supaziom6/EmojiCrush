using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContactManager : MonoBehaviour {

    //Public arrays
    public string[] contactName = new string[10];
    public string[] contactMessage = new string[10];
    public string[] correctResponses = new string[20];
    public string[] incorrectResponses = new string[20];

    //Game state flag - THIS IS ONLY TEMPORARY FLAG
    bool hasWon = false;
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
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
                correctResponses[8] = "I'll wait outside";
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

    }
    public void TextManager()
    {

    }
}
