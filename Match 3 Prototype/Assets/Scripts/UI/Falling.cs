using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Falling : MonoBehaviour
{
    public Sprite[] Sprites;
    public bool GoLeft;
    public GameObject Sprite;
    public Vector2 startPos;
    public float Xposition;
    public float YPosition;
    public float rotation;
    public float Speed;
    public static float[] bounds;

    private Sprite currentSprite;

    void Start()
    {
        bounds = new float[2];
        bounds[0] = Camera.main.orthographicSize * 2.0f;
        bounds[1] = bounds[0] * Camera.main.aspect;
        currentSprite = Sprites[(Random.Range(0, Sprites.Length))];
        GetComponent<SpriteRenderer>().sprite = currentSprite;
        if (Random.Range(0f, 1f) < 0.5f)
        {
            GoLeft = false;
        }
        else
        {
            GoLeft = true;
        }
        Xposition = Random.Range(-2.5f, 2.5f);
        rotation = Random.Range(-40, 40);
        Speed = Random.Range(1f, 4f);
        YPosition = 339;
        startPos = Sprite.transform.position;
        Sprite.transform.Rotate(0,0,rotation);
    }

    void Update()
    {
        if(Sprite.transform.position.y < -bounds[0]/2)
        {
            rotation = Random.Range(-40, 40);
            Sprite.transform.Rotate(0, 0, rotation);
            Xposition = Random.Range(-2.5f, 2.5f);
            Speed = Random.Range(1f, 4f);
            Sprite.transform.position = new Vector2 (startPos.x,bounds[0]/2 + 1);
        }
        Sprite.transform.Translate(Vector2.down * (Speed * Time.deltaTime), Space.World);

        if(GoLeft)
        {
            Sprite.transform.Translate(Vector2.left * ((Speed*Random.Range(1f,2f)) * Time.deltaTime), Space.World);
        }
        else
        {
            Sprite.transform.Translate(Vector2.right * ((Speed * Random.Range(1f, 2f)) * Time.deltaTime), Space.World);
        }
        print("Half the bounds"+ -bounds[1] / 2 + "Half the scaled Image size"+(currentSprite.bounds.size.x * transform.localScale.x));
        if (Sprite.transform.position.x < -bounds[1]/2 + (currentSprite.bounds.size.x*transform.localScale.x))
        {
            GoLeft = false;
        }
        if (Sprite.transform.position.x > bounds[1]/2 - (currentSprite.bounds.size.x*transform.localScale.x))
        {
            GoLeft = true;
        }
    }
}
