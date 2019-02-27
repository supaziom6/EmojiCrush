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

    void Start()
    {
        GetComponent<SpriteRenderer>().sprite= Sprites[(Random.Range(0,Sprites.Length))];
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
        if(Sprite.transform.position.y < -5.5)
        {
            rotation = Random.Range(-40, 40);
            Sprite.transform.Rotate(0, 0, rotation);
            Xposition = Random.Range(-2.5f, 2.5f);
            Speed = Random.Range(1f, 4f);
            Sprite.transform.position = startPos;
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
        if (Sprite.transform.position.x < -2.50)
        {
            GoLeft = false;
        }
        if (Sprite.transform.position.x > 2.5)
        {
            GoLeft = true;
        }
    }
}
