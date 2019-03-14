using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadScene : MonoBehaviour {

    ContactManager cM;
    //TEST
    int testScene;

    // Use this for initialization
    void Start () {
        cM = GetComponent<ContactManager>();
        //TEST
        testScene = SceneManager.GetActiveScene().buildIndex;
    }
	
	// Update is called once per frame
	void Update () {
		
	}
    public void LoadLevel()
    {
        //Mom
        if (testScene == 1)
        {
            cM.conName.text = cM.contactName[0].ToString();
            cM.conMessage.text = cM.contactMessage[0].ToString();
        }
        //Brother
        if (testScene == 2)
        {
            cM.conName.text = cM.contactName[1].ToString();
            cM.conMessage.text = cM.contactMessage[1].ToString();
        }
        //Doctor
        if (testScene == 3)
        {
            cM.conName.text = cM.contactName[2].ToString();
            cM.conMessage.text = cM.contactMessage[2].ToString();
        }
        //Boss
        if (testScene == 4)
        {
            cM.conName.text = cM.contactName[3].ToString();
            cM.conMessage.text = cM.contactMessage[3].ToString();
        }
        //Colleague
        if (testScene == 5)
        {
            cM.conName.text = cM.contactName[4].ToString();
            cM.conMessage.text = cM.contactMessage[4].ToString();
        }
        //Cousin
        if (testScene == 6)
        {
            cM.conName.text = cM.contactName[5].ToString();
            cM.conMessage.text = cM.contactMessage[5].ToString();
        }
        //Friend
        if (testScene == 7)
        {
            cM.conName.text = cM.contactName[6].ToString();
            cM.conMessage.text = cM.contactMessage[6].ToString();
        }
        //GrandMa
        if (testScene == 8)
        {
            cM.conName.text = cM.contactName[7].ToString();
            cM.conMessage.text = cM.contactMessage[7].ToString();
        }
        //Uber
        if (testScene == 9)
        {
            cM.conName.text = cM.contactName[8].ToString();
            cM.conMessage.text = cM.contactMessage[8].ToString();
        }
        //Dominos
        if (testScene == 10)
        {
            cM.conName.text = cM.contactName[9].ToString();
            cM.conMessage.text = cM.contactMessage[9].ToString();
        }
    }

}
