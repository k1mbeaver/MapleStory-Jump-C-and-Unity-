using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jump : MonoBehaviour
{
    public float jump;
    bool ground = false;
    bool inputJump = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            if(ground)
            {
                inputJump = true;
            }
        }
    }

    void FixedUpdate()
    {
        if (true)
        {
            GetComponent<Rigidbody2D>().velocity = new Vector2(GetComponent<Rigidbody2D>().velocity.x, jump);
            inputJump = false;
        }
    }

    void OnTriggerEnter2D()
    {
        ground = true;
    }

    void OnTriggerExit2D()
    {
        ground = false;
    }
}
