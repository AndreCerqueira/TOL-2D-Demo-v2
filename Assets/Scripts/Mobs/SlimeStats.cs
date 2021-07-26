using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SlimeStats : CharacterStats
{
    private Canvas _canvas;
    [SerializeField] private string nome;
    [SerializeField] private int xp;

    // Start is called before the first frame update
    void Awake()
    {
        _canvas = GetComponentInChildren<Canvas>();

        barraVida = _canvas.transform.Find("BarraVida/Slider").GetComponent<Slider>();
        barraVida.maxValue = maxVida;
    }

    // Update is called once per frame
    void Update()
    {
        barraVida.gameObject.SetActive((currentVida == maxVida ? false : true));
        barraVida.value = Mathf.Lerp(barraVida.value, currentVida, 0.25f);
    }

    public override void Morrer()
    {
        base.Morrer();
        GetComponent<SlimeController>().enabled = false;
        player.GetComponent<PlayerStats>().xp += xp;

        GameObject _spawner = Resources.Load ("Spawner") as GameObject;
        GameObject _instantiated = Instantiate(_spawner, transform.position, Quaternion.identity);
        _instantiated.GetComponent<Spawner>()._mob = Resources.Load<GameObject>("Mobs/" + nome);
        _instantiated.GetComponent<Spawner>().tempoVolta = GetComponent<SlimeController>().tempoVolta;
        _instantiated.GetComponent<Spawner>().dir = GetComponent<SlimeController>().dirInicial;
        _instantiated.GetComponent<Spawner>().posSpawn = GetComponent<SlimeController>().posSpawn;
        
        utilis.continuarQuest(nome);

        if(Random.Range(1,5) == 1){
            GameObject _moeda = Resources.Load ("Drops/Moeda") as GameObject;
            Instantiate(_moeda, transform.position, Quaternion.identity);
        }

        if(Random.Range(1,3) == 1){
            GameObject _gosma = Resources.Load ("Drops/Gosma Azul") as GameObject;
            GameObject _drop = Instantiate(_gosma, transform.position, Quaternion.identity);
            _drop.GetComponent<Drops>().nome = "Gosma Azul";
        }

 
    }

    #region coliders
    
        private bool podeLevarDano;
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

    #endregion





}
