using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Drops : MonoBehaviour
{
    Inventario _inventario;

    public string nome;
    void Start()
    {
        transform.position += Vector3.left * Random.Range(-0.1f,0.1f);
        GetComponent<Rigidbody2D>().AddForce(Vector2.up * Random.Range(4,6), ForceMode2D.Impulse);

        _inventario = FindObjectOfType<Inventario>();
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag == "Drop")
        {
            Physics2D.IgnoreCollision(col.gameObject.GetComponent<BoxCollider2D>(), GetComponent<BoxCollider2D>());
        }

        if(col.transform.tag == "Player")
        {
            _inventario.adicionarNoInventario(nome, 1);
            Destroy(gameObject);
        }
    }
}
