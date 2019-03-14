using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ExitToMainMenu : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        LoadLevel();
	}
    public void LoadLevel()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
