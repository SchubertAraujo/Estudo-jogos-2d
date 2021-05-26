using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class ReSkin : MonoBehaviour
{
    private SpriteRenderer sRender;

    public Sprite[] sprites;
    public string spriteSheetNames; //Nome do spritesheep que sera usado.
    public string loadSpriteSheetNames;

    private Dictionary<string, Sprite> spriteSheet;

    // Start is called before the first frame update
    void Start()
    {
        sRender = GetComponent<SpriteRenderer>();
        LoadSpriteSheet();
    }

    // Update é chamado depois do update normal
    void LateUpdate()
    {
        if(loadSpriteSheetNames != spriteSheetNames)
        {
            LoadSpriteSheet();
        }

        sRender.sprite = spriteSheet[sRender.sprite.name];

    }

    private void LoadSpriteSheet()
    {
        sprites = Resources.LoadAll<Sprite>(spriteSheetNames);
        spriteSheet = sprites.ToDictionary(x => x.name, x => x);
        loadSpriteSheetNames = spriteSheetNames;
    }
}
