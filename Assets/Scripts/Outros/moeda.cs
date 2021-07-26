using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class moeda : MonoBehaviour
{
    private int valor = 1;

    void Start()
    {
        GetComponent<Rigidbody2D>().AddForce(Vector2.up * 6, ForceMode2D.Impulse);
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag == "Drop")
        {
            Physics2D.IgnoreCollision(col.gameObject.GetComponent<BoxCollider2D>(), GetComponent<BoxCollider2D>());
        }

        if(col.transform.tag == "Player")
        {
            GameObject player = FindObjectOfType<PlayerController>().gameObject;
            player.GetComponent<PlayerStats>().dinheiro += valor;
            Destroy(gameObject);
        }
    }
}
