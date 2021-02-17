using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorScript : MonoBehaviour
{
    private FadeScript  fade;
    public  Transform   destiny;
    public  Transform   tPlayer;
    public  bool        isDark;
    public  Material    light2D, default2D;

    // Start is called before the first frame update
    public void Start()
    {
        fade = FindObjectOfType(typeof(FadeScript)) as FadeScript;
    }
    public void Interaction()
    {
        StartCoroutine("DoorIn");
    }
    
    IEnumerator DoorIn() 
    {

        fade.FadeIn();
        yield return new WaitWhile(() => fade.fume.color.a < 0.9f); // Uma condiçao que faz com que a corotina verifique se o color está completamente escuro, quando finalizado executas as açoes abaixo
        tPlayer.gameObject.SetActive(false);
        if (isDark)
            tPlayer.gameObject.GetComponent<SpriteRenderer>().material = light2D;
        else
            tPlayer.gameObject.GetComponent<SpriteRenderer>().material = default2D;

        tPlayer.position = destiny.position;
        yield return new WaitForSeconds(0.5f);
        tPlayer.gameObject.SetActive(true);
        fade.FadeOut();
    }
}
