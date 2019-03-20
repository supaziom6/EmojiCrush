using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TransitionScreen : MonoBehaviour {

    public GameObject cube;
    public Text contactNameText;
    public Text contactMessageText;


	// Use this for initialization
	void Start () {
        //Initially disable text
        contactNameText.gameObject.SetActive(false);
        contactMessageText.gameObject.SetActive(false);
        StartCoroutine(WaitForCubeToRotate());
    }
	
	// Update is called once per frame
	void Update () {
        HasCubeRotated();
        contactNameText.gameObject.SetActive(true);
        contactMessageText.gameObject.SetActive(true);
    }
    public bool HasCubeRotated()
    {
        float currentYRotation = 0;
        float startYRotation = 0;
        float endYRotation = 180f;
        float rotateSpeed = 5f;

        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(0, endYRotation, 0), Time.deltaTime * rotateSpeed);
        //currentYRotation++;
        //currentYRotation = Mathf.Clamp(currentYRotation, startYRotation, endYRotation);
        //transform.rotation = Quaternion.Euler(0, currentYRotation, 0);

        return true;

    }
    IEnumerator WaitForCubeToRotate()
    {
        yield return new WaitForSeconds(5);
        HasCubeRotated();
    }
}
