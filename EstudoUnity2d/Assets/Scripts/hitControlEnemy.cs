using System.Collections;
using System.Collections.Generic;
using System.Security;
using UnityEngine;
using UnityEngine.Video;
using TMPro;

public class HitControlEnemy : MonoBehaviour
{
    private GameController gameController;
    private PlayerScript playerScript;
    private LootsScript lootsScript;
    private SpriteRenderer sRender;

    private Animator animator;
    

    [Header("Configuração de vida")]
    public Color[] colorEnemy;
    public int lifeEnemy;
    public int currentLifeEnemy;
    public GameObject lifeBars; // OBJETO CONTENDO TODAS AS BARRAS DE VIDA
    public Transform lifeBar; // OBJETO INDICADOR DA QUANTIDADE DE VIDA
    private float percLife;
    public GameObject hitTXTPrefab; // Objeto que ira exibir o dano tomado


    [Header("Configuração Resistência/Fraqueza")]
    private WeaponDamage weaponDamage;
    public float[] resistAjdusment;  // Sistema de resitência e fraqueza


    [Header("Configuração KnockBack")]
    private float kx;
    private GameObject KnockTime;
    public GameObject knockBackForcePrefab;
    public Transform knockPosition;
    public float knockX;
    public bool enemylookingLeft , playerLeft;
    public bool getHit;  //Indica se tomou um hit
    private bool died;  //Indica se esta morto


    [Header("Configuração Colisão com o chão")]
    public Transform groundCheck;
    public LayerMask whatIsGround; //Verifica quais layers/Camadas que o personagem vai interagir


    [Header("Configuração de loot")]
    public GameObject[] loots;



    // Start is called before the first frame update
    void Start()
    {
        //Usado para achar outros scripts
        gameController = FindObjectOfType(typeof(GameController)) as GameController;
        playerScript = FindObjectOfType(typeof(PlayerScript)) as PlayerScript;
        
        sRender = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();

        lifeBars.SetActive(true);
        sRender.color = colorEnemy[0];
        currentLifeEnemy = lifeEnemy;
        lifeBar.localScale = new Vector3(1, 1, 1);

        EnemyLookingLeft();
     
    }

    // Update is called once per frame
    void Update()
    {
        float xPlayer = playerScript.transform.position.x;
        /*If da aula
        //Verifica se player está a esquerta ou a direita

        //Vai passar as informações do trasform do knockback e inverter a posição
        //dependendo do lado que player está
        if (xPlayer < transform.position.x) 
        {
            playerLeft = true;
            
        } 
        else if (xPlayer > transform.position.x)
        {
            playerLeft = false;
        }*/

        //Melhorado para
        EnemySide(xPlayer < transform.position.x);


        //If da aula
        /*
        if (enemylookingLeft && playerLeft)
        {
            kx = knockX;
        }
        else if (!enemylookingLeft && playerLeft)
        {
            kx = knockX * -1;
        }
        else if (enemylookingLeft && !playerLeft)
        {
            kx = knockX * -1;
        }
        else if (!enemylookingLeft && !playerLeft)
        {
            kx = knockX;
        }
        */

        //Melhorado para
        knockBackSide((enemylookingLeft && playerLeft),(!enemylookingLeft && !playerLeft));


        knockPosition.localPosition = new Vector3(kx, knockPosition.localPosition.y, 0);
        animator.SetBool("grounded", true);

    }
    public void EnemySide(bool condition)
    {
        if (condition)
            playerLeft = true;
        else
            playerLeft = false;

    }

    public void knockBackSide(bool condition1, bool condition2)
    {
        if (condition1 || condition2)
            kx = knockX;
        else
            kx = knockX * -1;
    }

    //Os colisores da arma tem a propriedade trigger tem que estar marcada
    void OnTriggerEnter2D(Collider2D col)
    {
        if (died) { return; }

        switch (col.gameObject.tag)
        {
            case "Weapon":
                if (!getHit)
                {
                    getHit = true;
                    lifeBars.SetActive(true);
                    weaponDamage = col.gameObject.GetComponent<WeaponDamage>();

                    animator.SetTrigger("hit");

                    float damageWeapon = Random.Range(weaponDamage.damageMin, weaponDamage.damageMax);
                    int typeDamage = weaponDamage.typeDamage;
                    

                    float damageTaken = damageWeapon + (damageWeapon * (resistAjdusment[typeDamage] / 100));
                    currentLifeEnemy -= Mathf.RoundToInt(damageTaken);
                    percLife =   (float)currentLifeEnemy / (float)lifeEnemy;

                    if (percLife < 0)
                        percLife = 0;

                    lifeBar.localScale = new Vector3(percLife,1,1);

                    if (percLife <= 0)
                    {
                        died = true;
                        animator.SetInteger("idAnimation", 3);
                        StartCoroutine("Death");
                        
                    }

                    //Vai instanciar o objeto para que ele possa gerar a força
                    //Depois vai destrui lo para nao ficar tomando dano eterno.
                    //Como a taxa de atualização da unity é de 0.02 segundos o tempo destruição
                    //tem q ser maior ou igual 0.02
                    KnockTime = Instantiate(knockBackForcePrefab, knockPosition.position, knockPosition.rotation);
                    Destroy(KnockTime, 0.02f);


                    //Controle do numero de dano que aparece na tela
                    GameObject hitTemp = Instantiate(hitTXTPrefab, transform.position, transform.localRotation);
                    hitTemp.GetComponentInChildren<TextMeshPro>().text = Mathf.RoundToInt(damageTaken).ToString();
                    hitTemp.GetComponentInChildren<MeshRenderer>().sortingLayerName = "HUD";


                    GameObject fxTemp = Instantiate(gameController.fxDamage[typeDamage], transform.position, transform.localRotation);
                    Destroy(fxTemp, 1);

                    

                    
                    int forcaX = 50;
                    if (!playerLeft)
                        forcaX *= -1;
                    hitTemp.GetComponentInChildren<Rigidbody2D>().AddForce(new Vector2(forcaX, 200));
                    Destroy(hitTemp, 0.8f);

                    StartCoroutine("Invulnerable"); //não entendi muito bem essa função
                }
                break;
        }
    }

    void EnemyLookingLeft()
    {
        if (enemylookingLeft)
        {
            float x = transform.localScale.x;
            x *= -1; //Inverte o sinal de scale x
            transform.localScale = new Vector3(x, transform.localScale.y, 0);
        }
    }
        

    IEnumerator Death()
    {
        yield return new WaitForSeconds(1);
        GameObject fxDeath = Instantiate(gameController.fxDeath, groundCheck.position, transform.localRotation);
        yield return new WaitForSeconds(0.5f);
        sRender.enabled = false;

        //LOOT
        LootsScript lootObject = gameObject.AddComponent<LootsScript>();
        StartCoroutine(lootObject.generateLoots(loots)); 


        yield return new WaitForSeconds(1);
        Destroy(fxDeath);
        Destroy(this.gameObject);
    }


    //Alterana cores do inimigo para simular um hit
    IEnumerator Invulnerable()
    {
        sRender.color = colorEnemy[1];
        yield return new WaitForSeconds(0.2f);
        sRender.color = colorEnemy[0];
        yield return new WaitForSeconds(0.2f);
        sRender.color = colorEnemy[1];
        yield return new WaitForSeconds(0.2f);
        sRender.color = colorEnemy[0];
        yield return new WaitForSeconds(0.2f);
        sRender.color = colorEnemy[1];
        yield return new WaitForSeconds(0.2f);
        sRender.color = colorEnemy[0];
        getHit = false;
        lifeBars.SetActive(false);
    }

}
