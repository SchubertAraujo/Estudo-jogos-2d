using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameController : MonoBehaviour
{
    public  string[]        typesDamage;
    public  GameObject[]    fxDamage;
    public  GameObject      fxDeath;
    public  int             gold;
    public  TextMeshProUGUI goldTxt;
    private FadeScript      fade;

    [Header("Banco de Dados de Armas")]

    public Sprite[] sWeapons0;
    public Sprite[] sWeapons1;
    public Sprite[] sWeapons2;

    public int[] damageMin;
    public int[] damageMax;
    public int[] typeDamage;
    public string[] weaponName;
    public int[] weaponPrice;

    public void Start()
    {
        fade = FindObjectOfType(typeof(FadeScript)) as FadeScript;
        fade.FadeOut();
    }
    public void Update()
    {
        goldTxt.text = gold.ToString("N0");
    }
}

