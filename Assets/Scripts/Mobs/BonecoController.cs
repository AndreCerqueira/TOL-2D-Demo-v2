using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BonecoController : MonoBehaviour
{
    private GameObject player;
    private Animator anim;
    private Rigidbody2D rb;
    private bool podeLevarDano;

    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<PlayerController>().gameObject;
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.X) && podeLevarDano)
        {
            anim.SetTrigger("dano");
                        
            if(player.transform.position.x > transform.position.x)
                transform.eulerAngles = Vector2.up * 180;
            else
                transform.eulerAngles = Vector2.up * 0;

            utilis.continuarQuest("Boneco");
        }
    }

    void OnTriggerStay2D(Collider2D other) 
    {
        if (other.gameObject.tag == "Player") 
            podeLevarDano = true;
    }

    void OnTriggerExit2D(Collider2D other) 
    {
        if (other.gameObject.tag == "Player") 
            podeLevarDano = false;
    }

}
