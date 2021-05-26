using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorScript : MonoBehaviour
{


    private FadeScript  fade;
    private PlayerScript playerScript;
    public  Transform   destiny;
 
    public  bool        isDark;
    public  Material    light2D, default2D;

    // Start is called before the first frame update
    public void Start()
    {
        fade = FindObjectOfType(typeof(FadeScript)) as FadeScript;
        playerScript = FindObjectOfType(typeof(PlayerScript)) as PlayerScript;
    }
    public void Interaction()
    {
        StartCoroutine("DoorIn");
    }
    
    IEnumerator DoorIn() 
    {

        fade.FadeIn();
        yield return new WaitWhile(() => fade.fume.color.a < 0.9f); // Uma condiçao que faz com que a corotina verifique se o color está completamente escuro, quando finalizado executas as açoes abaixo
        playerScript.gameObject.SetActive(false);
        if (isDark)
            playerScript.ChangeMaterial(light2D);
        else
            playerScript.ChangeMaterial(default2D);

        playerScript.transform.position = destiny.position;
        yield return new WaitForSeconds(0.5f);
        playerScript.gameObject.SetActive(true);
        fade.FadeOut();
    }
}
