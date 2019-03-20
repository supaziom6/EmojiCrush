using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScreenScroll : MonoBehaviour {

    public Scrollbar sb;
    public RectTransform ItemsList;

    private Touch touch;
    private float maxHeight;
    private float currentHeight;


    // Use this for initialization
    void Start () {
        sb.value = 0;
        maxHeight = ItemsList.rect.height;
        currentHeight = 0;
	}
	
	// Update is called once per frame
	void Update () {
        /* NEEDS TO BE TESTED ON MOBILE */
        if (Input.touchCount > 0)
        {
            touch = Input.touches[0];
            if (touch.phase == TouchPhase.Moved)
            {
                currentHeight += touch.deltaPosition.y;
                if(currentHeight >= maxHeight+10){
                    currentHeight = maxHeight + 10;
                }
                else if(currentHeight <= 0)
                {
                    currentHeight = 0;
                }

                sb.value = 1-(currentHeight / maxHeight);
            }
        }
    }
}
