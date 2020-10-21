using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerScript : MonoBehaviour
{
    private     Animator    playerAnimator;
    public      Transform   groundedCheck;//Verificar se ha uma colisão com personagem.. lembrando que é necessário puxar o tranform para a variavel la no unity 
    public      Rigidbody2D playerRb;
    public      Collider2D standing, crounching;
   
    
    public      bool        Grounded;       //Indica se o personagem está pisando em alguma superfice
    public      bool        lookingLeft;    //Verifica se o personagem está olhando para direita
    public      int         idAnimation;    //Indica o id da animação
    public      float       speed;          //Velocidade do player
    public      float       forceJump;      //Velocidade do pulo
    public      bool        attacking;
    public     float       h, v;



    // Start is called before the first frame update
    void Start()
    {
        //Passar para variavel o componente do objeto
        playerAnimator = GetComponent<Animator>();
        playerRb = GetComponent<Rigidbody2D>();
    }

    //Tem uma atualização fixada a cada 0.2 segundos;
   void FixedUpdate()
   {
        Grounded = Physics2D.OverlapCircle(groundedCheck.position, 0.02f);
        //Faz o paler movimentar-se
        playerRb.velocity = new Vector2(h * speed, playerRb.velocity.y);
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

        

        if (h > 0 && lookingLeft && !attacking)
        {
            flip();
        }
        else if(h < 0 && !lookingLeft && !attacking)
        {
            flip();
        }

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

        //Botão preconfigurado
        if(Input.GetButtonDown("Fire1") &&  v >= 0)
        {
            playerAnimator.SetTrigger("atack");
        }

        //faz o player pular so quando ele esta colidindo com o chão
        if (Input.GetButtonDown("Jump") && Grounded && !attacking)
        {
            playerRb.AddForce(new Vector2(0, forceJump));
        }

        if (attacking && Grounded)
        {
            h = 0;
        }

        
        if (v < 0 && Grounded)
        { 
            crounching.enabled = true;
            standing.enabled = false;

        }
        else
        {
            crounching.enabled = false;
            standing.enabled = true;
        }



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
        transform.localScale = new  Vector3(x, transform.localScale.y, transform.localScale.x);

    }

    void atack(int atk)
    {
        switch(atk) 
        {
            case 0:
                attacking = false;
                break;
            case 1:
                attacking = true;
                break;

        }
    }
}

