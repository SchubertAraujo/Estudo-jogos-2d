using System.Collections;
using System.Collections.Generic;
using System.Security;
using UnityEngine;
using UnityEngine.Video;

public class HitControlEnemy : MonoBehaviour
{
    private GameController gameController;
    private PlayerScript playerScript;
    private SpriteRenderer sRender;

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

   




    // Start is called before the first frame update
    void Start()
    {
        gameController = FindObjectOfType(typeof(GameController)) as GameController;
        playerScript = FindObjectOfType(typeof(PlayerScript)) as PlayerScript;
        sRender = GetComponent<SpriteRenderer>();
        lifeBars.SetActive(true);
        sRender.color = colorEnemy[0];
        currentLifeEnemy = lifeEnemy;
        lifeBar.localScale = new Vector3(1, 1, 1);
       
        if (enemylookingLeft)
         {            
            float x = transform.localScale.x;
            x *= -1; //Inverte o sinal de scale x
            transform.localScale = new Vector3(x, transform.localScale.y, 0);
         }
    }

    // Update is called once per frame
    void Update()
    {
        float xPlayer = playerScript.transform.position.x;
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
        }

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

        
        knockPosition.localPosition = new Vector3(kx, knockPosition.localPosition.y, 0);

    }

    //Os colisores da arma tem a propriedade trigger tem que estar marcada
    void OnTriggerEnter2D(Collider2D col)
    {

        switch (col.gameObject.tag)
        {
            case "Weapon":
                if (!getHit)
                {
                    getHit = true;
                    lifeBars.SetActive(true);
                    weaponDamage = col.gameObject.GetComponent<WeaponDamage>();
                    float damageWeapon = Random.Range(weaponDamage.damageMin, weaponDamage.damageMax);
                    int typeDamage = weaponDamage.typeDamage;
                    

                    float damageTaken = damageWeapon + (damageWeapon * (resistAjdusment[typeDamage] / 100));
                    currentLifeEnemy -= Mathf.RoundToInt(damageTaken);
                    percLife =   (float)currentLifeEnemy / (float)lifeEnemy;

                    if (percLife < 0)
                        percLife = 0;

                    lifeBar.localScale = new Vector3(percLife,1,1);

                    if (percLife <= 0)
                        Destroy(this.gameObject);

                    //Vai instanciar o objeto para que ele possa gerar a força
                    //Depois vai destrui lo para nao ficar tomando dano eterno.
                    //Como a taxa de atualização da unity é de 0.02 segundos o tempo destruição
                    //tem q ser maior ou igual 0.02
                    KnockTime = Instantiate(knockBackForcePrefab, knockPosition.position, knockPosition.rotation);
                    Destroy(KnockTime, 0.02f);


                    GameObject hitTemp = Instantiate(hitTXTPrefab, transform.position, transform.localRotation);
                    hitTemp.GetComponent<TextMesh>().text = Mathf.RoundToInt(damageTaken).ToString();
                    hitTemp.GetComponent<MeshRenderer>().sortingLayerName = "HUD";
                    
                    int forcaX = 50;
                    if (!playerLeft)
                        forcaX *= -1;
                    hitTemp.GetComponent<Rigidbody2D>().AddForce(new Vector2(forcaX, 200));
                    Destroy(hitTemp, 0.8f);

                    StartCoroutine("invulnerable"); //não entendi muito bem essa funçoa
                }
                break;
        }
    }

    //Alterana cores do inimigo para simular um hit
    IEnumerator invulnerable()
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
