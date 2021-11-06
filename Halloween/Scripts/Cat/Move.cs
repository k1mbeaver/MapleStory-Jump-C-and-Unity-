using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move : MonoBehaviour
{
    private Rigidbody2D player;
    float walkForce = 30.0f;
    float maxWalkSpeed = 2.0f;
    int key = 0;
    public float jumpForce = 150.0f;
    private bool jumpCount = false;
    bool ground = false;
    Animator animator;
    Vector2 lookDirection = new Vector2(1, 0);
    private float vertical = 0;
    private float horizontal = 0;

    // Start is called before the first frame update
    void Start()
    {
        player = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        
        horizontal = Input.GetAxis("Horizontal");
        vertical = Input.GetAxis("Vertical");
        Vector2 move = new Vector2(horizontal, vertical);
        if (!Mathf.Approximately(move.x, 0.0f) || !Mathf.Approximately(move.y, 0.0f))
        {
            lookDirection.Set(move.x, move.y);
            lookDirection.Normalize();
        }

        animator.SetFloat("Move X", lookDirection.x);
        animator.SetFloat("Move Y", lookDirection.y);
        animator.SetFloat("Speed", move.magnitude);
        
        if (Input.GetKeyDown(KeyCode.Space))
        {
            jumpCount = true;
            //animator.SetBool("isJumping", true);
            //animator.SetTrigger("doJumping");
        }
        key = 0;
        if (Input.GetKey(KeyCode.RightArrow)) key = 1;
        if (Input.GetKey(KeyCode.LeftArrow)) key = -1;
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        if(jumpCount)
        {
            player.AddForce(transform.up * jumpForce);
            jumpCount = false;
            //animator.SetBool("isJumping", false);
        }

        float speedx = Mathf.Abs(player.velocity.x);

        if(speedx < maxWalkSpeed)
        {
            player.AddForce(transform.right * key * walkForce);
        }
    }
}
