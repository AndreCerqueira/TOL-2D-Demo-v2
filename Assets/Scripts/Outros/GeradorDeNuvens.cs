using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeradorDeNuvens : MonoBehaviour
{
    private int tempo = 5;
    [SerializeField] private GameObject[] nuvem = new GameObject[3];

    void Start()
    {
        StartCoroutine(criarNuvem());
    }


    IEnumerator criarNuvem()
    {
        while (true)
        {
            yield return new WaitForSeconds(tempo);

            tempo = Random.Range(3, 7);
            int i = Random.Range(0, 3);

            Vector2 posNuvem = new Vector2(transform.position.x, transform.position.y + Random.Range(0, 3.2f));

            Instantiate(nuvem[i], posNuvem, Quaternion.identity);
        }
    }
}
