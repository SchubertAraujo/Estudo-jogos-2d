using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HudScript : MonoBehaviour
{
    private PlayerScript playerScript;
    private float percetualHealth;
    public string teste;
    public Image[] hpBar;
    public Sprite halfHpSprite, fullHpSprite;
    Dictionary<string, Action> dicHpControl = new Dictionary<string, Action>();
    // Start is called before the first frame update
    void Start()
    {
        playerScript = FindObjectOfType(typeof(PlayerScript)) as PlayerScript;


        //Solução para if aninhados
        InitiateDictionary();

    }

    void InitiateDictionary()
    {
        dicHpControl.Add("0", PercentualZero);
        dicHpControl.Add("1", HpFull);
        dicHpControl.Add("0,9", hp9);
        dicHpControl.Add("0,8", hp8);
        dicHpControl.Add("0,7", hp7);
        dicHpControl.Add("0,6", hp6);
        dicHpControl.Add("0,5", hp5);
        dicHpControl.Add("0,4", hp4);
        dicHpControl.Add("0,3", hp3);
        dicHpControl.Add("0,2", hp2);
        dicHpControl.Add("0,1", hp1);
    }
    // Update is called once per frame
    void Update()
    {
        controlHealthBar();
    }

    void controlHealthBar()
    {
        percetualHealth = (float)playerScript.currentHealthPlayer / (float)playerScript.maxHealthPlayer;

        //Segundo a aula é para ter certeza que todos as imagens do hp aparecam no inicio
        foreach (Image img in hpBar)
        {
            img.enabled = true;
            img.sprite = fullHpSprite;
        }

        /*
        // IF DA AULA
        if (percetualHealth == 1)//Colocado para que quando tomar um hit perder hp, senao o 0.9f>=
        {

        }
        else if (percetualHealth >= 0.9f) // De 0.99 ate 0.90
        {
            hpBar[4].sprite = halfHpSprite;
        }
        else if (percetualHealth >= 0.8f)
        {
            hpBar[4].enabled = false;
        }
        else if (percetualHealth >= 0.7f)
        {
            hpBar[4].enabled = false;
            hpBar[3].sprite = halfHpSprite;
        }
        else if (percetualHealth >= 0.6f)
        {
            hpBar[4].enabled = false;
            hpBar[3].enabled = false;
            
        }
        else if (percetualHealth >= 0.5f)
        {
            hpBar[4].enabled = false;
            hpBar[3].enabled = false;
            hpBar[2].sprite = halfHpSprite;
        }
        else if (percetualHealth >= 0.4f)
        {
            hpBar[4].enabled = false;
            hpBar[3].enabled = false;
            hpBar[2].enabled = false;
            
        }
        else if (percetualHealth >= 0.3f)
        {
            hpBar[4].enabled = false;
            hpBar[3].enabled = false;
            hpBar[2].enabled = false;
            hpBar[1].sprite = halfHpSprite;

        }
        else if (percetualHealth >= 0.2f)
        {
            hpBar[4].enabled = false;
            hpBar[3].enabled = false;
            hpBar[2].enabled = false;
            hpBar[1].enabled = false;

        }
        else if (percetualHealth >= 0.01f)
        {
            hpBar[4].enabled = false;
            hpBar[3].enabled = false;
            hpBar[2].enabled = false;
            hpBar[1].enabled = false;
            hpBar[0].sprite = halfHpSprite;

        }
        else if (percetualHealth <= 0f)
        {
            hpBar[4].enabled = false;
            hpBar[3].enabled = false;
            hpBar[2].enabled = false;
            hpBar[1].enabled = false;
            hpBar[0].enabled = false;

        }*/

        //Reduçao dos ifs feita por mim
        if (percetualHealth <= 0.0)
            percetualHealth = 0;
        var PercReduced = string.Format("{0:0.#}", percetualHealth);
        if (dicHpControl.ContainsKey(PercReduced)) //Verifica se o dicnary existe pra nao dar erro
            dicHpControl[PercReduced].Invoke();

    }

    public void HpFull()
    {

    }
    public void PercentualZero()
    {

        DisableEnableHpBar(0, 4, false);
    }

    public void hp9() //? Segundo a aula é colocado isso pois se um numero der 0.991 ele vai entender q  é igual a 1(Contestavel verificar alteraçã
    {
        hpBar[4].sprite = halfHpSprite;

    }
    public void hp8() //? Segundo a aula é colocado isso pois se um numero der 0.991 ele vai entender q  é igual a 1(Contestavel verificar alteraçã
    {
        DisableEnableHpBar(4, 4, false);
    }
    public void hp7()
    {
        DisableEnableHpBar(4, 4, false);
        hpBar[3].sprite = halfHpSprite;
    }
    public void hp6()
    {
        DisableEnableHpBar(3, 4, false);
    }

    public void hp5()
    {
        DisableEnableHpBar(3, 4, false);
        hpBar[2].sprite = halfHpSprite;
    }

    public void hp4()
    {
        DisableEnableHpBar(2, 4, false);
    }

    public void hp3()
    {
        DisableEnableHpBar(2, 4, false);
        hpBar[1].sprite = halfHpSprite;
    }

    public void hp2()
    {
        DisableEnableHpBar(1, 4, false);
        hpBar[1].sprite = halfHpSprite;
    }

    public void hp1()
    {
        hpBar[4].enabled = false;
        hpBar[3].enabled = false;
        hpBar[2].enabled = false;
        hpBar[1].enabled = false;
        hpBar[0].sprite = halfHpSprite;
    }

    void DisableEnableHpBar(int minValueMatriz, int maxValueMatriz, bool status)
    {
        for (int i = minValueMatriz; i <= maxValueMatriz; i++)
            hpBar[i].enabled = status;
    } 

}
