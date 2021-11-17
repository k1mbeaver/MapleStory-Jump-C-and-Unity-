using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spin : MonoBehaviour
{
    public float speed;
    public bool vertical;
    public float changeTime;

    SpriteRenderer spinRend;
    public Sprite[] spinAnimation; //  애니메이션
    int frameCount = 0;
    int spriteCount = 0;

    Rigidbody2D spinObject;
    float timer;
    int direction = 1;

    // Start is called before the first frame update
    void Start()
    {
        spinRend = GetComponent<SpriteRenderer>();
        spinObject = GetComponent<Rigidbody2D>();
        timer = changeTime;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        // 팽이 애니메이션
        spinRend.sprite = spinAnimation[spriteCount];
        frameCount++;
        if (frameCount % 10 == 0)
        {
            spriteCount++;
        }

        if (frameCount == 30)
        {
            frameCount = 0;
        }

        if (spriteCount == 3)
        {
            spriteCount = 0;
        }

        // 팽이 방향 전환
        timer -= Time.deltaTime;

        if (timer < 0)
        {
            direction = -direction;
            timer = changeTime;
        }
        Vector2 position = spinObject.position;
        if (vertical)
        {
            position.y = position.y + Time.deltaTime * speed * direction;
        }

        else
        {
            position.x = position.x + Time.deltaTime * speed * direction;
        }
        spinObject.MovePosition(position);
    }
}
