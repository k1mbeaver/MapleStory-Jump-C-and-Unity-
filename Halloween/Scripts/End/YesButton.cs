using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine;

public class YesButton : MonoBehaviour
{
    private Collider boxCollider;
    public Sprite[] YesButtonAnimation;
    SpriteRenderer yesButtonRend;
    int buttonOn = 0;

    void Start()
    {
        yesButtonRend = GetComponent<SpriteRenderer>();
        boxCollider = GetComponent<Collider>();
    }

    void Update()
    {
        if(buttonOn == 1)
        {
            yesButtonRend.sprite = YesButtonAnimation[buttonOn];
        }

        else if(buttonOn == 0)
        {
            yesButtonRend.sprite = YesButtonAnimation[buttonOn];
        }
    }
    void OnMouseOver()
    {
        buttonOn = 1;
    }

    void OnMouseExit()
    {
        buttonOn = 0;
    }

    void OnMouseDown()
    {
        Debug.Log("Click");
        if (buttonOn == 1)
        {
            SceneManager.LoadScene("Halloween");
        }
    }
}
