using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterController : MonoBehaviour
{
    public float speed;
    public bool vertical;
    public float changeTime;

    SpriteRenderer monsterRend;
    public Sprite[] monsterAnimation; //  애니메이션
    int frameCount = 0;
    int spriteCount = 0;

    Rigidbody2D monster;
    float timer;
    int direction = 1;

    // Start is called before the first frame update
    void Start()
    {
        monster = GetComponent<Rigidbody2D>();
        monsterRend = GetComponent<SpriteRenderer>();
        timer = changeTime;
    }

    void Update()
    {
        // 애니메이션
        // 오른쪽 
        if (direction == 1)
        {
            monsterRend.flipX = true;
            monsterRend.sprite = monsterAnimation[spriteCount];
            frameCount++;
            if (frameCount % 60 == 0)
            {
                spriteCount++;
            }

            if (frameCount == 360)
            {
                frameCount = 0;
            }

            if (spriteCount == 6)
            {
                spriteCount = 0;
            }
        }
        // 왼쪽
        else if (direction == -1)
        {
            monsterRend.flipX = false;
            monsterRend.sprite = monsterAnimation[spriteCount];
            frameCount++;
            if (frameCount % 60 == 0)
            {
                spriteCount++;
            }

            if (frameCount == 360)
            {
                frameCount = 0;
            }

            if (spriteCount == 6)
            {
                spriteCount = 0;
            }
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        timer -= Time.deltaTime;

        if(timer < 0)
        {
            direction = -direction;
            timer = changeTime;
        }
        Vector2 position = monster.position;
        if (vertical)
        {
            position.y = position.y + Time.deltaTime * speed * direction;
        }

        else
        {
            position.x = position.x + Time.deltaTime * speed * direction;
        }
        monster.MovePosition(position);
    }
}
