using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine;

public class NoButton : MonoBehaviour
{
    private Collider boxCollider;
    public Sprite[] noButtonAnimation;
    SpriteRenderer noButtonRend;
    int buttonOn = 0;

    void Start()
    {
        noButtonRend = GetComponent<SpriteRenderer>();
        boxCollider = GetComponent<Collider>();
    }

    void Update()
    {
        if (buttonOn == 1)
        {
            noButtonRend.sprite = noButtonAnimation[buttonOn];
        }

        else if (buttonOn == 0)
        {
            noButtonRend.sprite = noButtonAnimation[buttonOn];
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
        if (buttonOn == 1)
        {
            #if UNITY_EDITOR
                UnityEditor.EditorApplication.isPlaying = false;
            #else  
                App;ication.Quit();
            #endif
        }
    }
}
