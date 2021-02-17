using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using UnityEngine;

public class ChestScript : MonoBehaviour
{
    private     SpriteRenderer  spriteRenderer;
    public      Sprite[]          imgObject;
    public      bool            isOpened;
    public      GameObject[]      loots;
    public      bool            lootGenerated;

    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        
    }
    public void Interaction()
    {
        //Abre e fecha o baú
        isOpened = !isOpened;
        if (isOpened)
        {
            spriteRenderer.sprite = imgObject[1];
            //LOOT
            if (lootGenerated == false)
            {
                lootGenerated = true;
                LootsScript lootObject = gameObject.AddComponent<LootsScript>();
                StartCoroutine(lootObject.generateLoots(loots));
                GetComponent<Collider2D>().enabled = false;
            }
        } /* else
        {
            spriteRenderer.sprite = imgObject[0];
        } */

    }
 
}
