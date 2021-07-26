using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Animator anim;
    private Rigidbody2D rb;
    [SerializeField] private float speed;
    private int dir = 1;
    private bool isGrounded;
    private bool temPlataformaBaixo;
    private bool temPlataformaCima;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {

        #region Correr

            float InputX = Input.GetAxis("Horizontal");

            anim.SetBool("Correr", (InputX != 0 ? true : false));

            if(InputX > 0) dir = 1;
            else if(InputX < 0) dir = -1;

            transform.eulerAngles = Vector3.up * (dir == -1 ? 180 : 0);

            Vector2 playerMovement = new Vector2(InputX * speed, 0) * Time.deltaTime;

            transform.Translate(playerMovement * dir, Space.Self);

        #endregion

        #region Saltar

            if((Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.W)) && isGrounded && GetComponent<CapsuleCollider2D>().enabled)
            {
                rb.AddForce(Vector3.up * 690);
            }

            anim.SetBool("Saltar", !isGrounded);

        #endregion

        #region Atacar

            if(Input.GetKeyDown(KeyCode.X))
                anim.SetTrigger("Soco");

        #endregion

    }

    IEnumerator ativarCollider()
    {
        yield return new WaitForSeconds(0.4f);
        GetComponent<CapsuleCollider2D>().enabled = true;
    }

    // Este metodo é chamado quando o golpe acerta e causa dano e outros efeitos
    public void causarDano(string ataque)
    {
        //Ele pega todas as entidades dentro da esfera de ataque dele e se estiver lá o player ele causa dano
        Collider2D[] hitColliders = Physics2D.OverlapCircleAll(transform.position, 1.25f);
        foreach (var entidade in hitColliders)
        {
            if (entidade.GetComponent<SlimeStats>() != null)
            {      
                if (entidade.GetComponent<SlimeStats>().currentVida > 0)
                {
                    entidade.GetComponent<SlimeStats>().TakeDamage(1);
                }
            }
        }
        
    }


    void OnCollisionStay2D(Collision2D other) 
    {
        if (other.gameObject.tag == "Ground" || other.gameObject.tag == "Plataforma") 
            isGrounded = true;

        if(other.gameObject.tag == "Plataforma")
            temPlataformaBaixo = true;
    }

    void OnCollisionExit2D(Collision2D other) 
    {
        if (other.gameObject.tag == "Ground" || other.gameObject.tag == "Plataforma") 
            isGrounded = false;

        if(other.gameObject.tag == "Plataforma")
            temPlataformaBaixo = false;
    }

    void OnTriggerStay2D(Collider2D other) 
    {
        if(other.gameObject.tag == "Plataforma")
            temPlataformaCima = true;
        
        if(other.gameObject.tag == "Parede")
        {
            temPlataformaCima = false;
            GetComponent<CapsuleCollider2D>().enabled = true;
        }
    }

    void OnTriggerExit2D(Collider2D other) 
    {
        if(other.gameObject.tag == "Plataforma")
            temPlataformaCima = false;
    }

    void OnDrawGizmos()
    {
        // Draw a yellow sphere at the transform's position
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, 1.25f);
    }

}
