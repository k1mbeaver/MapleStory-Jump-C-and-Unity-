using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Move : MonoBehaviour
{
    AudioSource audioSource;
    private Rigidbody2D player;

    public AudioClip JumpClip;

    float walkForce = 20.0f;
    float maxWalkSpeed = 1.0f;
    int key = 0; // 캐릭터의 방향 = 시작 방향은 오른쪽 1 (왼쪽은 -1)
    public float jumpForce = 300.0f;
    private bool jumpCount = false;
    Vector3 startVector = new Vector3(-6.0f, 0.665f, 5);

    public int maxHealth = 5;
    public int health { get { return currentHealth; } }
    int currentHealth;
    bool isInvincible;
    int InvincibleTime = 0;

    SpriteRenderer playerRend;
    public Sprite[] standCharacter; // 서있는 애니메이션
    public Sprite[] walkCharacter; // 걷는 애니메이션
    public Sprite[] jumpCharacter; // 점프 스프라이트
    public Sprite[] deathCharacter; // 사망 스프라이트
    int spriteCount = 0;
    int frameCount = 0;
    bool directionCharacter = true; // 캐릭터의 방향 = 시작 방향은 오른쪽 true (왼쪽은 false)

    bool ground = true;
    bool hit = false;
    bool death = false;

    // Start is called before the first frame update
    void Start()
    {
        player = GetComponent<Rigidbody2D>();
        currentHealth = maxHealth;
        audioSource = GetComponent<AudioSource>();

        playerRend = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        // 죽었을 때
        
        if(death)
        {
            // 오른쪽 
            if (directionCharacter == true)
            {
                playerRend.flipX = true;
                playerRend.sprite = deathCharacter[spriteCount];
                frameCount++;
                if (frameCount % 60 == 0)
                {
                    spriteCount++;
                }

                if (frameCount == 600)
                {
                    frameCount = 0;
                }

                if (spriteCount == 10)
                {
                    SceneManager.LoadScene("End");
                }
            }
            // 왼쪽
            else if (directionCharacter == false)
            {
                playerRend.flipX = false;
                playerRend.sprite = deathCharacter[spriteCount];
                frameCount++;
                if (frameCount % 60 == 0)
                {
                    spriteCount++;
                }

                if (frameCount == 600)
                {
                    frameCount = 0;
                }

                if (spriteCount == 10)
                {
                    SceneManager.LoadScene("End");
                }
            }
            //transform.position = startVector;
            //death = false;
        }
        else
        {
            // 좌우 이동 ( FixedUpdate용 )
            key = 0;
            if (Input.GetKey(KeyCode.RightArrow))
            {
                key = 1;
                directionCharacter = true;
            }
            if (Input.GetKey(KeyCode.LeftArrow))
            {
                key = -1;
                directionCharacter = false;
            }

            // 애니메이션 부분

            // 가만히 서있을 때
            if (ground == true && Input.GetAxis("Horizontal") == 0)
            {
                // 오른쪽 
                if (directionCharacter == true)
                {
                    playerRend.flipX = true;
                    playerRend.sprite = standCharacter[spriteCount];
                    frameCount++;
                    if (frameCount % 30 == 0)
                    {
                        spriteCount++;
                    }

                    if (frameCount == 180)
                    {
                        frameCount = 0;
                    }

                    if (spriteCount == 6)
                    {
                        spriteCount = 0;
                    }
                }
                // 왼쪽
                else if (directionCharacter == false)
                {
                    playerRend.flipX = false;
                    playerRend.sprite = standCharacter[spriteCount];
                    frameCount++;
                    if (frameCount % 30 == 0)
                    {
                        spriteCount++;
                    }

                    if (frameCount == 180)
                    {
                        frameCount = 0;
                    }

                    if (spriteCount == 6)
                    {
                        spriteCount = 0;
                    }
                }
            }

            // 왼쪽으로 걸어 갈 때
            else if (ground == true && Input.GetAxis("Horizontal") < 0)
            {
                playerRend.flipX = false;
                playerRend.sprite = walkCharacter[spriteCount];
                frameCount++;
                if (frameCount % 30 == 0)
                {
                    spriteCount++;
                }

                if (frameCount == 180)
                {
                    frameCount = 0;
                }

                if (spriteCount == 6)
                {
                    spriteCount = 0;
                }
            }

            // 오른쪽으로 걸어 갈 때
            else if (ground == true && Input.GetAxis("Horizontal") > 0)
            {
                playerRend.flipX = true;
                playerRend.sprite = walkCharacter[spriteCount];
                frameCount++;
                if (frameCount % 30 == 0)
                {
                    spriteCount++;
                }

                if (frameCount == 180)
                {
                    frameCount = 0;
                }

                if (spriteCount == 6)
                {
                    spriteCount = 0;
                }
            }
            // 점프 할 때나 떨어 질 때
            else if (ground == false)
            {
                // 오른쪽 
                if (directionCharacter == true)
                {
                    playerRend.flipX = true;
                    playerRend.sprite = jumpCharacter[0];
                }
                // 왼쪽
                else if (directionCharacter == false)
                {
                    playerRend.flipX = false;
                    playerRend.sprite = jumpCharacter[0];
                }
            }

            // 경사면 안 미끄러지게
            if (Input.GetAxis("Horizontal") == 0)
            {
                player.constraints = RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezeRotation;
            }
            else
            {
                player.constraints = RigidbodyConstraints2D.FreezeRotation;
            }

            // 캐릭터가 화면 밖을 안나가게
            Vector3 pos = Camera.main.WorldToViewportPoint(transform.position);

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

            transform.position = Camera.main.ViewportToWorldPoint(pos);

            // 캐릭터가 화면 아래로 내려갔을 때 초기 위치로 재배치
            if (transform.position.y < 0.0f)
            {
                transform.position = startVector;
            }

            // 점프
            if (Input.GetKeyDown(KeyCode.LeftAlt))
            {
                if (ground == true)
                {
                    jumpCount = true;
                    PlaySound(JumpClip);
                }
            }

            // 캐릭터가 npc와 만났을 때
            if (Input.GetKeyDown(KeyCode.Space))
            {
                Vector2 lookDirection = new Vector2(key, 0);
                RaycastHit2D hit = Physics2D.Raycast(player.position + Vector2.right * 0.2f, lookDirection, 1.5f, LayerMask.GetMask("Npc"));

                if (hit.collider != null)
                {
                    NpcController npc = hit.collider.GetComponent<NpcController>();
                    if (npc != null)
                    {
                        npc.DisplayDialog();
                    }
                }
            }
            // 캐릭터가 포탈 위에서 윗 방향키를 눌렀을 떄
            if (Input.GetKeyDown(KeyCode.UpArrow))
            {
                Vector2 lookDirection = new Vector2(key, 1);
                RaycastHit2D hit = Physics2D.Raycast(player.position + Vector2.down * 1.0f, lookDirection, 1.5f, LayerMask.GetMask("Portal"));

                if (hit.collider != null)
                {
                    startPortal myStart = hit.collider.GetComponent<startPortal>();
                    endPortal myEnd = hit.collider.GetComponent<endPortal>();
                    if (myStart != null)
                    {
                        myStart.DisplayDialog();
                    }
                    else if (myEnd != null)
                    {
                        myEnd.DisplayDialog();
                    }
                }
            }
        }   
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(death == false)
        {
            // 데미지를 받았을 때 무적 시간
            if (isInvincible)
            {
                if(InvincibleTime % 30 == 0)
                {
                    playerRend.color = new Color32(255, 255, 255, 90);
                }

                else
                {
                    playerRend.color = new Color32(255, 255, 255, 180);
                }

                InvincibleTime++;
                if (InvincibleTime == 300)
                {
                    isInvincible = false;
                    InvincibleTime = 0;
                    playerRend.color = new Color32(255, 255, 255, 255);
                    return;
                }
            }

            // 점프
            if (jumpCount)
            {
                player.AddForce(transform.up * jumpForce);
                jumpCount = false;
                //ground = false;
            }

            // 캐릭터 이동
            float speedx = Mathf.Abs(player.velocity.x);

            if (speedx < maxWalkSpeed)
            {
                player.AddForce(transform.right * key * walkForce);
            }
        }
    }

    // 땅에 닿였을 때
    void OnTriggerEnter2D(Collider2D obj)
    {
        if(obj.gameObject.layer == 6)
        {
            ground = true;
        }

        else if (obj.gameObject.layer == 11 && isInvincible == false)
        {
            ChangeHealth(-1);
        }
    }

    // 땅에서 떨어져 있을 때(점프나 추락)
    void OnTriggerExit2D()
    {
        if(player.velocity.y != 0) // 수직으로 움직이는 현상이 있을 때나 점프 중이면
        {
            ground = false;
        }
    }

    // 캐릭터 체력 변화
    public void ChangeHealth(int amount)
    {
        if(amount < 0)
        {
            if (isInvincible)
                return;

            isInvincible = true;
            InvincibleTime = 0;
        }

        currentHealth = Mathf.Clamp(currentHealth + amount, 0, maxHealth);
        if (currentHealth == 0)
        {
            death = true;
        }
        CatHealthBar.instance.SetValue(currentHealth / (float)maxHealth);
    }

    // 점프소리
    public void PlaySound(AudioClip clip)
    {
        audioSource.PlayOneShot(clip);
    }
}
