using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SlimeController : MonoBehaviour
{
    protected GameObject player;
    private Animator anim;
    private Canvas _canvas;
    private Transform wayPoint;
    private float destino;
    public int dirInicial = 1;
    private int dir;
    public float tempoVolta;
    
    private bool parado;
    private bool agro;
    private bool isAtacking;

    private float coldown;
    [SerializeField] private float coldownMax;

    [Header("Gizmos")]
    [SerializeField] private float campoVisao;
    [SerializeField] private float campoVisaoNormal;
    [SerializeField] private float campoVisaoAgro;
    [SerializeField] private float campoAtaque;

    [SerializeField] private Vector2 _posSpawn;

    // Start is called before the first frame update
    void Start()
    {
        _posSpawn = transform.position;

        dir = dirInicial;
        player = FindObjectOfType<PlayerController>().gameObject;
        anim = GetComponent<Animator>();
        _canvas = GetComponentInChildren<Canvas>();
        wayPoint = transform.Find("wayPoint");

        StartCoroutine(patrulha());
        rodar();

    }

    // Update is called once per frame
    void Update()
    {

        #region Movimento

            bool boolDir = (dir == 1 ? transform.position.x < destino : transform.position.x > destino);
            if(boolDir && !parado)
            {
                Vector2 playerMovement = new Vector2(-1.25f, 0) * Time.deltaTime;
                transform.Translate(playerMovement, Space.Self);
            }
            else
                anim.SetBool("Andar",false);

        #endregion

        #region Modo Agro

            Collider2D[] hitColliders = Physics2D.OverlapBoxAll(new Vector2(transform.position.x, transform.position.y + 0.5f), new Vector2(campoVisao, 1), 0);
            agro = false;
            foreach (var entidade in hitColliders)
            {
                if (entidade.CompareTag("Player"))
                    agro = true;
            }

            campoVisao = (agro ? campoVisaoAgro : campoVisaoNormal);

            if(agro)
            {
                dir = (player.transform.position.x - transform.position.x < 0.1f ? -1 : 1);
                rodar();
                
                destino = player.transform.position.x;

                Collider2D[] hitColliders2 = Physics2D.OverlapBoxAll(new Vector2(transform.position.x, transform.position.y + 0.5f), new Vector2(campoAtaque, 1), 0);
                parado = false;
                foreach (var entidade in hitColliders2)
                {
                    if (entidade.CompareTag("Player"))
                    {
                        parado = true;
                        if(!isAtacking)
                            selectAttack();
                    }
                }
            }

        #endregion

    }

    /*--------------- Sequencia de Ataques do Mob ---------------*/
    public void selectAttack()
    {
        // Tempo que ele tem que esperar antes de lançar o proximo ataque
        if (coldown > coldownMax)
        {
            anim.SetTrigger("Ataque");
            isAtacking = true;
            coldown = 0;
        }
        else
            coldown+=Time.deltaTime;
        
    }

    /*--------------- Causar Dano ---------------*/
    public void causarDano()
    {
        /*--------------- Desenha uma forma geometrica na zona do ataque, se o player entrar nessa zona durante o ataque ele leva dano ---------------*/            
        Collider2D[] hitColliders = Physics2D.OverlapBoxAll(new Vector2(transform.position.x, transform.position.y + 0.5f), new Vector2(campoAtaque, 1),0);
        bool tirouDano1vez = false;;
        foreach (var entidade in hitColliders)
        {
            /*--------------- Verifica se o player está dentro da zona e está vivo ---------------*/
            if (entidade.GetComponent<PlayerStats>() != null && entidade.GetComponent<PlayerStats>().currentVida > 0 && !tirouDano1vez)
            {
                entidade.GetComponent<PlayerStats>().TakeDamage(1);
                tirouDano1vez = true;
            }
        }
    }

    public void fimAtaque()
    {
        isAtacking = false;
    }


    IEnumerator patrulha()
    {
        while (true)
        {
            yield return new WaitForSeconds(tempoVolta);
            
            if(GetComponent<SlimeStats>().currentVida > 0 && !agro)
            {
                dir *= -1;
                rodar();
            }

        }
    }

    void rodar()
    {
        transform.eulerAngles = Vector2.up * (dir == 1 ? 180 : 0);
        destino = wayPoint.position.x;
        anim.SetBool("Andar",true);

        _canvas.transform.eulerAngles = Vector2.up * 0;
    }


    void OnCollisionEnter2D(Collision2D col)
    {
        if(col.transform.tag == "Ground" || col.transform.tag == "Plataforma")
        {
            GetComponent<CircleCollider2D>().enabled = false;
            GetComponent<Rigidbody2D>().gravityScale = 0;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if(other.tag == "Ground" || other.tag == "Plataforma")
        {
            GetComponent<CircleCollider2D>().enabled = true;
            GetComponent<Rigidbody2D>().gravityScale = 2;
        }

    }


    void OnDrawGizmos()
    {

        Gizmos.color = Color.blue;
        Gizmos.DrawWireCube(new Vector2(transform.position.x, transform.position.y + 0.5f), new Vector2(campoVisao, 1));

        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(new Vector2(transform.position.x, transform.position.y + 0.5f), new Vector2(campoAtaque, 1));
    }


    public Vector2 posSpawn
    {
        get { return _posSpawn; }
        set { _posSpawn = value; }
    }

}
