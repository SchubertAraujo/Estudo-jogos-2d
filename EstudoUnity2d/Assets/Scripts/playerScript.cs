using System.Collections;
using System.Collections.Generic;
using System.Runtime.ExceptionServices;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    private     Animator    playerAnimator;
    public      Transform   groundedCheck;//Verificar se ha uma colisão com personagem.. lembrando que é necessário puxar o tranform para a variavel la no unity 
    public      Rigidbody2D playerRb;
    public      Collider2D  standing, crounching;
    public      LayerMask   whatIsGround;

    //Interação com objetos
    public      Transform   hand;     //define o local aonde o ray cast 2d será inicializado.
    private     Vector3     direction = Vector3.right; //Inicia um Vector3 para o lado direito
    public      LayerMask   interactionLayer;
    public      GameObject  objectInteracion;

    //Sistema de armas
    public      GameObject[] weapons;


    public      bool        Grounded;       //Indica se o personagem está pisando em alguma superfice
    public      bool        lookingLeft;    //Verifica se o personagem está olhando para direita
    public      int         idAnimation;    //Indica o id da animação
    public      float       speed;          //Velocidade do player
    public      float       forceJump;      //Velocidade do pulo
    public      bool        attacking;
    private     float       h, v;




    // Start is called before the first frame update
    void Start()
    {
        //Passar para variavel o componente do objeto
        playerAnimator = GetComponent<Animator>();
        playerRb = GetComponent<Rigidbody2D>();

        //Desabilita o objeto weapons
        disable();
       
    }

    //Tem uma atualização fixada a cada 0.2 segundos;
   void FixedUpdate()
   {
        //Gera um circulo de colição com um raio escolhido
        Grounded = Physics2D.OverlapCircle(groundedCheck.position, 0.02f, whatIsGround);
        //Faz o paler movimentar-se
        playerRb.velocity = new Vector2(h * speed, playerRb.velocity.y);
        interaction();
    }


    // Update is called once per frame (Update é mais rapido que o fixed update)
    void Update()
    {
        //Vai passar se o esta para para esquerda(-1) ou direita(1) de acordo com teclas;
        //Ja esta configurado para "ad" e setas
        h = Input.GetAxisRaw("Horizontal");


        //Vai passar se esta para cima(1) ou para baixo(-1) de acordo com teclas
        //Ja esta configurado "ws"  seta
        v = Input.GetAxisRaw("Vertical");

        ChangeSides();
        ChangeAnimation();
        inputsSelect();


        //Para atacar parado
        if (attacking && Grounded)
        {
            h = 0;
        }
        
        if (v < 0 && Grounded)
            ChangeCollider(true, false);
        else
            ChangeCollider(false, true);
        

        //Passa a variavel do animator
        playerAnimator.SetBool("grounded", Grounded);
        playerAnimator.SetInteger("idAnimation", idAnimation);
        playerAnimator.SetFloat("speedY", playerRb.velocity.y);

        
    }

    void flip()
    {
        lookingLeft = !lookingLeft; //Inverte o valor booleano
        float x = transform.localScale.x;
        x *= -1; //Inverte o sinal de scale x
        transform.localScale = new  Vector3(x, transform.localScale.y, 0);

        //Muda a direção do drawray
        direction.x = x;
        

    }

    void disable()
    {
        foreach(GameObject o in weapons)
        {
            o.SetActive(false);
        }

    }

    //Muda de colisor quando o personagem está abaixado
    void ChangeCollider(bool crounching, bool standing )
    {
        this.crounching.enabled = crounching;
        this.standing.enabled = standing;
    }

    //Muda a direçao do pesonagem está "olhando"
    void ChangeSides()
    {
        if (h > 0 && lookingLeft && !attacking)
        {
            flip();
        }
        else if (h < 0 && !lookingLeft && !attacking)
        {
            flip();
        }
    }

    //Muda a id animaçao;
    void ChangeAnimation()
    {
        if (v < 0)
        {
            idAnimation = 2;
            if (Grounded)
            {
                h = 0;
            }

        }
        else if (h != 0)
        {
            idAnimation = 1;
        }
        else
        {
            idAnimation = 0;
        }
    }

    void atack(int atk)
    {
        switch(atk) 
        {
            case 0:
               attacking = false;
                weapons[2].SetActive(false);
                break;
            case 1:
                attacking = true;
                break;

        }
    }

    void inputsSelect()
    {
        //Botão preconfigurado em 
        if (Input.GetButtonDown("Fire1") && v >= 0 && attacking == false && objectInteracion == null)
        {
            playerAnimator.SetTrigger("atack");
        }
        
        if (Input.GetButtonDown("Fire1") && v >= 0 && attacking == false && objectInteracion != null)
        {
            //Faz com que caso não exita o objeto interação, nao retorna mensagem de erro
            //Primeiro parametro busca o metodo do chestScript interacionChest
            //Isso acontece pois o objeto "Silver chest" esta na layer iteraction que é setada na
            //variavel objectInteracion no unity
            objectInteracion.SendMessage("interactionChest", SendMessageOptions.DontRequireReceiver);
        }

        //faz o player pular so quando ele esta colidindo com o chão
        if (Input.GetButtonDown("Jump") && Grounded && !attacking)
        {
            playerRb.AddForce(new Vector2(0, forceJump));
        }
    }

    void interaction()
    {
        //Desenha uma linha q fara interação com objetos desejado
        //Será mostrada com a cor selecionada
        //Ela irá colidir com o primeiro objeto, caso seja uma linha grande e ela colida com mais de um objeto sem sua linha
        //os demais são ignorados
        Debug.DrawRay(hand.position, direction * 0.15f, Color.red);
        //Direção da linha desenhada esquerda(x= -1), direita(x = 1), para cima(y=0), para baixo(y= -1)
        //O parametro vector, pode ser escrito "Vector(1,0,0)
        // Primeiro parametro eixo x, Segundo eixo y, e terceiro 
        //O 
        RaycastHit2D hit = Physics2D.Raycast(hand.position, direction, 0.2f, interactionLayer);
        if (hit == true)
        {
            objectInteracion = hit.collider.gameObject;
            
        } 
        else
        {
            objectInteracion = null;
        }
       

        
    }

    //é usando como evento na animação de ataque

    void weaponControl(int id)
    {
        disable();
        weapons[id].SetActive(true);
       
    }
}

