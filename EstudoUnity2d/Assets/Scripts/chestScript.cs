using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using UnityEngine;

public class ChestScript : MonoBehaviour
{
    private     SpriteRenderer  spriteRenderer;
    public      Sprite[]          imgObject;
    public      bool            isOpened;

    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        
    }
    public void interactionChest()
    {
        //Abre e fecha o baú
        isOpened = !isOpened;
        if (isOpened)
        {
            spriteRenderer.sprite = imgObject[1];
        } else
        {
            spriteRenderer.sprite = imgObject[0];
        }

    }
 
}
