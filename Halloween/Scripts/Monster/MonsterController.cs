using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterController : MonoBehaviour
{
    public float speed;
    public bool vertical;
    public float changeTime;

    Animator animator;
    Rigidbody2D rigidbody2D;
    float timer;
    int direction = 1;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        rigidbody2D = GetComponent<Rigidbody2D>();
        timer = changeTime;
    }

    // Update is called once per frame
    void Update()
    {
        timer -= Time.deltaTime;

        if(timer < 0)
        {
            direction = -direction;
            timer = changeTime;
            animator.SetInteger("Direction", direction);
        }
        Vector2 position = rigidbody2D.position;
        if (vertical)
        {
            position.y = position.y + Time.deltaTime * speed * direction;
        }

        else
        {
            position.x = position.x + Time.deltaTime * speed * direction;
        }
        rigidbody2D.MovePosition(position);
    }
}
