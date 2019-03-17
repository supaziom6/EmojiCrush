using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Falling : MonoBehaviour
{
    public Sprite[] Sprites;
    public bool GoLeft;
    public Vector2 startPos;
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
        rotation = Random.Range(-40, 40);
        Speed = Random.Range(1f, 4f);
        startPos = transform.position;
        transform.Rotate(0,0,rotation);
    }

    void FixedUpdate()
    {
        if(transform.position.y < -bounds[0]/2)
        {
            rotation = Random.Range(-40, 40);
            transform.Rotate(0, 0, rotation);
            Speed = Random.Range(1f, 4f);
            transform.position = new Vector2 (startPos.x,bounds[0]/2 + 1);
        }
        transform.Translate(Vector2.down * (Speed * Time.deltaTime), Space.World);

        if(GoLeft)
        {
            transform.Translate(Vector2.left * ((Speed*Random.Range(1f,2f)) * Time.deltaTime), Space.World);
        }
        else
        {
            transform.Translate(Vector2.right * ((Speed * Random.Range(1f, 2f)) * Time.deltaTime), Space.World);
        }
        if (transform.position.x < -bounds[1]/2 + (currentSprite.bounds.size.x*transform.localScale.x/2))
        {
            GoLeft = false;
        }
        if (transform.position.x > bounds[1]/2 - (currentSprite.bounds.size.x*transform.localScale.x/2))
        {
            GoLeft = true;
        }
    }
}
