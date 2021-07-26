using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NuvemMove : MonoBehaviour
{
    private float vel;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(destruirNuvem());

        vel = Random.Range(1.1f, 2.1f);
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector2(transform.position.x + vel * Time.deltaTime, transform.position.y);
    }

    IEnumerator destruirNuvem()
    {
        while (true)
        {
            yield return new WaitForSeconds(60f);
            Destroy(gameObject);
        }
    }

}
