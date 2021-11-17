using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterController : MonoBehaviour
{
    public float speed;
    public bool vertical;
    public float changeTime;

    Animator animator;
    Rigidbody2D monster;
    float timer;
    int direction = 1;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        monster = GetComponent<Rigidbody2D>();
        timer = changeTime;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        timer -= Time.deltaTime;

        if(timer < 0)
        {
            direction = -direction;
            timer = changeTime;
            animator.SetInteger("Direction", direction);
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
