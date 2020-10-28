using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class hitControlEnemy : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //Os colisores da arma tem a propriedade trigger tem que estar marcada
    void OnTriggerEnter2D(Collider2D col)
    {
        switch (col.gameObject.tag)
        {
            case "Weapon":
                print("Tomei dano");
                break;
        }
    }
}
