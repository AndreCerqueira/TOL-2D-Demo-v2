using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterStats : MonoBehaviour
{
    protected GameObject player;
    protected Animator anim;
    public int nivel;
    protected Text nivelText;
    public int maxVida;
    public int currentVida;

    protected Slider barraVida;

    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<PlayerController>().gameObject;
        anim = GetComponent<Animator>();
    
        currentVida = maxVida;
    }

    public void TakeDamage(int dano)
    {
        //GetComponent<PopUpText>().ProduceText(dano);
        currentVida -= dano;
        if(currentVida <= 0)
        {
            Morrer();
        }
    }

    public virtual void Morrer()
    {
        anim.SetTrigger("Morrer");

        if (!gameObject.CompareTag("Player"))
            StartCoroutine(destruirMob());

    }

    /*---------------Este metodo é usado para destruir o objeto segundos depois de ter morrido ---------------*/
    IEnumerator destruirMob()
    {
        yield return new WaitForSeconds(2f);
        Destroy(gameObject);
    }

    public int _currentVida
    {
        get { return currentVida; }
        set { currentVida = value; }
    }

    public void Regenerar(int val)
    {
        if (currentVida > 0)
            currentVida += val;

        if(currentVida > maxVida)
            currentVida = maxVida;
    }

}
