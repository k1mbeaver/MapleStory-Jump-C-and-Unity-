using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move : MonoBehaviour
{
    private Rigidbody2D player;
    float walkForce = 20.0f;
    float maxWalkSpeed = 1.0f;
    int key = 0;
    public float jumpForce = 300.0f;
    private bool jumpCount = false;
    Animator animator;
    bool ground = false;

    // Start is called before the first frame update
    void Start()
    {
        player = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        // 애니메이션 부분
        if(Input.GetAxis("Horizontal") == 0)
        {
            animator.SetBool("isMoving", false);
        }
        else if(Input.GetAxis("Horizontal") < 0)
        {
            animator.SetFloat("Move X", -1.0f); // 왼쪽
            animator.SetBool("isMoving", true);
        }
        else if(Input.GetAxis("Horizontal") > 0)
        {
            animator.SetFloat("Move X", 1.0f); // 오른쪽
            animator.SetBool("isMoving", true);
        }

        // 경사면 안 미끄러지게
        if(Input.GetAxis("Horizontal") == 0)
        {
            player.constraints = RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezeRotation;
        }
        else
        {
            player.constraints = RigidbodyConstraints2D.FreezeRotation;
        }

        // 캐릭터가 화면 밖을 안나가게
        Vector3 pos = Camera.main.WorldToViewportPoint(this.transform.position);

        if (pos.x < 0f)
        {
            pos.x = 0f;
        }

        if (pos.x > 1f)
        {
            pos.x = 1f;
        }

        if (pos.y < 0f)
        {
            pos.y = 0f;
        }

        if (pos.y > 1f)
        {
            pos.y = 1f;
        }

        this.transform.position = Camera.main.ViewportToWorldPoint(pos);

        // 캐릭터가 화면 아래로 내려갔을 때 초기 위치로 재배치
        if (this.transform.position.y < 0.0f)
        {
            Debug.Log("화면 밖으로 나갔다!");
            this.transform.position = new Vector3(-6.0f, 0.665f, 5);
        }

        // 점프
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if(ground == true)
            {
                jumpCount = true;
            }
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
            ground = false;
        }

        float speedx = Mathf.Abs(player.velocity.x);

        if(speedx < maxWalkSpeed)
        {
            player.AddForce(transform.right * key * walkForce);
        }
    }

    void OnTriggerEnter2D(Collider2D tile)
    {
        if(tile.gameObject.layer == 6 && player.velocity.y < 0)
        {
            ground = true;
        }
    }

    void OnTriggerExit2D()
    {
        ground = false;
    }
}
