using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinScript : MonoBehaviour
{
    private GameController gameController;

    public int valueCoin;
    // Start is called before the first frame update
    void Start()
    {
        gameController = FindObjectOfType(typeof(GameController)) as GameController;
        
    }

    public void Collect()
    {
        gameController.gold += valueCoin;
        Destroy(this.gameObject);
    }
}
