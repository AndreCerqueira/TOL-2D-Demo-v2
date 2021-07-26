using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    //[SerializeField] private GameObject _mob;
    public GameObject _mob;
    public float tempoVolta;
    public int dir;
    public Vector2 posSpawn;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Evocar());
    }

    IEnumerator Evocar()
    {
        yield return new WaitForSeconds(15); // 300 - 5min
        GameObject clone = Instantiate(_mob);

        clone.GetComponent<SlimeController>().tempoVolta = tempoVolta;
        clone.GetComponent<SlimeController>().dirInicial = dir;
        clone.GetComponent<SlimeController>().transform.position = posSpawn;

        Destroy(gameObject);
    }

}
