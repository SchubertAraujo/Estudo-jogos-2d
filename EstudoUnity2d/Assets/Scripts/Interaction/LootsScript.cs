using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Classe não esta na aula porem preferir coloca-la para codigos repetidos
public class LootsScript : MonoBehaviour
{
    
    public IEnumerator generateLoots(GameObject[] loot)
    {
        int AmountCoins = Random.Range(1, 5);
        for (int l = 0; l < AmountCoins; l++)
        {
            int rand = 0;
            int idLoot = 0;
            rand = Random.Range(0,100); //Possibilita trabalhar com porcentagem de chance de drop
            if (rand >= 75)             //25% de chance de moeda laranja
                idLoot = 1;
            
            GameObject lootTemp = Instantiate(loot[idLoot], transform.position, transform.localRotation);
            lootTemp.GetComponent<Rigidbody2D>().AddForce(new Vector2(Random.Range(-25, 25), 75));
            yield return new WaitForSeconds(0.1f);
        }
    }
}
