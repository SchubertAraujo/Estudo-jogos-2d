using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FadeScript : MonoBehaviour
{
    public GameObject   painelFume;
    public Image        fume;
    public Color[]      colorTransition;
    public float        step;
    


    public void FadeIn()
    {
        painelFume.SetActive(true);
        StartCoroutine("IFadeI");
    }

    public void FadeOut()
    {
        StartCoroutine("IFadeO");
        
    }

    IEnumerator IFadeI()
    {
        for (float i = 0; i <= 1; i += step)
        {
            fume.color = Color.Lerp(colorTransition[0], colorTransition[1], i);
            yield return new WaitForEndOfFrame();
        }
        

    }

    IEnumerator IFadeO()
    {
        for (float i = 0; i <= 1; i += step)
        {
            fume.color = Color.Lerp(colorTransition[1], colorTransition[0], i);
            yield return new WaitForEndOfFrame();
        }
        painelFume.SetActive(false);
    }

}
