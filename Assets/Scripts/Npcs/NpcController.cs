using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NpcController : MonoBehaviour
{
    public List<MissaoNpc> ListaMissoesNpc = new List<MissaoNpc>();
    public List<GameObject> ListaMissoesAtivas = new List<GameObject>();
    public List<Button> ListaButoesMissoes = new List<Button>();

    protected GameObject player;
    protected Canvas _canvas;
    protected GameObject CaixaDialogo;
    protected GameObject menuMissoes;
    public int fase;

    protected Animator anim;
    protected Rigidbody2D rb;
    protected bool falar;
    protected bool playerPerto;

    protected GameObject novaMissao;
    protected GameObject receberRecompensa;

    // Start is called before the first frame update
    public virtual void Awake()
    {
        player = FindObjectOfType<PlayerController>().gameObject;
        _canvas = GameObject.FindGameObjectWithTag("Canvas").GetComponent<Canvas>();
        CaixaDialogo = _canvas.transform.Find("CaixaDialogo").gameObject;
        menuMissoes = _canvas.transform.Find("MenuMissoesNpc").gameObject;

        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();

        novaMissao = transform.Find("novaMissao").gameObject;
        receberRecompensa = transform.Find("receberRecompensa").gameObject;

        StartCoroutine(desbloquearMissoesTimer());
    }

    public void Update()
    {
        atualizarIcon();

        if(Input.GetKeyDown(KeyCode.E) && playerPerto && !menuMissoes.activeSelf && !CaixaDialogo.activeSelf)
        {
            falar = !falar;
            anim.SetBool("falar", falar);
            
            ativarMenuMissoes();
        }
    }

    public void desbloquearMissoes()
    {
        foreach (var quest in ListaMissoesNpc)
        {
            if(quest.nivel <= player.GetComponent<PlayerStats>().nivel && (quest.status == "bloqueada" || quest.status == ""))
                quest.status = "pendente";
            else if(quest.nivel > player.GetComponent<PlayerStats>().nivel)
                quest.status = "bloqueada";
        }
    }

    public void atualizarIcon()
    {
        bool iconNovaMissao = false;
        bool iconRecompensa = false;

        NpcController[] _npcController = FindObjectsOfType<NpcController>();
        for (int i = 0; i < _npcController.Length; i++)
        {
            for (int j = 0; j < _npcController[i].ListaMissoesNpc.Count; j++)
            {
                if(_npcController[i].ListaMissoesNpc[j].sender == transform.name)
                {
                    if(_npcController[i].ListaMissoesNpc[j].status == "pendente")
                        iconNovaMissao = true;
                    if(_npcController[i].ListaMissoesNpc[j].status == "concluida")
                        iconRecompensa = true;
                }
            }
        }

        if(iconNovaMissao && iconRecompensa)
        {
            novaMissao.SetActive(false);
            receberRecompensa.SetActive(iconRecompensa);
        }
        else
        {
            novaMissao.SetActive(iconNovaMissao);
            receberRecompensa.SetActive(iconRecompensa);
        }

    }

    #region triggers

        void OnTriggerStay2D(Collider2D other) 
        {
            if (other.gameObject.tag == "Player") 
                playerPerto = true;
        }

        void OnTriggerExit2D(Collider2D other) 
        {
            if (other.gameObject.tag == "Player") 
                playerPerto = false;
        }

    #endregion

    #region Dialogos

        public void dialogo(int i)
        {
            if(fase == ListaMissoesNpc[i].fala.Length - 1)
                utilis.addFala(CaixaDialogo, 1, ListaMissoesNpc[i].xp, ListaMissoesNpc[i].dinheiro);
            else
            {
                switch (fase)
                {
                    case 0:
                        CaixaDialogo.SetActive(true);
                        CaixaDialogo.transform.Find("titulo/text").GetComponent<Text>().text = name;
                        CaixaDialogo.transform.Find("BordaImagem/Image").GetComponent<Image>().sprite = Resources.Load<Sprite>("DialogoIcons/" + name);
                        utilis.addFala(CaixaDialogo, ListaMissoesNpc[i].fala[fase]);
                    break;

                    case 101:
                        CaixaDialogo.SetActive(false);
                        ListaMissoesNpc[i].status = "aceitada";
                        utilis.addQuest(ListaMissoesNpc[i].quest);
                        fase = 0;
                        StopAllCoroutines();
                    break;

                    case 102:
                        fase = 0;
                        StopAllCoroutines();
                        CaixaDialogo.SetActive(false);
                    break;

                    case 1000:
                        CaixaDialogo.SetActive(true);
                        CaixaDialogo.transform.Find("titulo/text").GetComponent<Text>().text = name;
                        CaixaDialogo.transform.Find("BordaImagem/Image").GetComponent<Image>().sprite = Resources.Load<Sprite>("DialogoIcons/" + name);
                        utilis.addFala(CaixaDialogo, 2, ListaMissoesNpc[i].xp, ListaMissoesNpc[i].dinheiro);
                        ListaMissoesNpc[i].status = "completada";
                    break;

                    default:
                        utilis.addFala(CaixaDialogo, ListaMissoesNpc[i].fala[fase]);
                    break;
                }
            }
        }

        public void ultimoDialogo()
        {
            utilis.completarQuest(transform.name);
            fase = 0;
            StopAllCoroutines();
            CaixaDialogo.SetActive(false);
        }

    #endregion

    void ativarMenuMissoes()
    {
        menuMissoes.SetActive(true);
        menuMissoes.transform.Find("tituloBase/titulo/text").GetComponent<Text>().text = transform.name;
        menuMissoes.transform.Find("tituloBase/BordaImg/img").GetComponent<Image>().sprite = Resources.Load<Sprite>("DialogoIcons/" + name);
        menuMissoes.transform.Find("bt_close").GetComponent<Button>().onClick.AddListener(delegate { desativarMenuMissoes(); });

        int espacamento = 145;
        for (int i = 0; i < ListaMissoesNpc.Count; i++)
        {
            GameObject _linha = Resources.Load ("linha") as GameObject;
            GameObject _criado = Instantiate(_linha);
            _criado.transform.parent = menuMissoes.transform; //145
            _criado.transform.localPosition = new Vector3(0, espacamento);
            espacamento -= 35;
            _criado.transform.localScale = new Vector3(1,1,1);

            desbloquearMissoes();

            switch (ListaMissoesNpc[i].status)
            {
                case "pendente":
                    _criado.transform.Find("bt_aceitar").gameObject.SetActive(true);
                    ListaButoesMissoes.Add(_criado.transform.Find("bt_aceitar").GetComponent<Button>());
                break;

                case "aceitada":
                    _criado.gameObject.SetActive(false);
                    espacamento += 35;
                    ListaButoesMissoes.Add(_criado.transform.Find("bt_aceitar").GetComponent<Button>());
                break;

                case "completada":
                    _criado.gameObject.SetActive(false);
                    espacamento += 35;
                    ListaButoesMissoes.Add(_criado.transform.Find("bt_aceitar").GetComponent<Button>());
                break;

                case "concluida":
                    _criado.transform.Find("bt_receber").gameObject.SetActive(true);
                    _criado.transform.Find("nivel").GetComponent<Image>().color = new Color32(151,195,207,255);
                    ListaButoesMissoes.Add(_criado.transform.Find("bt_receber").GetComponent<Button>());
                break;

                case "bloqueada":
                    _criado.transform.Find("bt_bloqueado").gameObject.SetActive(true);
                    _criado.transform.Find("nivel").GetComponent<Image>().color = new Color32(200,200,200,255);
                    ListaButoesMissoes.Add(_criado.transform.Find("bt_aceitar").GetComponent<Button>());
                break;
            }

            AssignButtonOnClick (i); 
            
            _criado.transform.Find("BordaTexto/Text").GetComponent<Text>().text = "[Missão " + (i+1) + "]";
            _criado.transform.Find("nivel/text").GetComponent<Text>().text = ListaMissoesNpc[i].nivel.ToString();

            ListaMissoesAtivas.Add(_criado);
        }
    }

    void desativarMenuMissoes()
    {
        menuMissoes.SetActive(false);  
        StopAllCoroutines();

        if(ListaMissoesAtivas.Count > 0)
        {
            for (int j = 0; j < ListaMissoesAtivas.Count; j++)
                Destroy(ListaMissoesAtivas[j]);
        }  

        ListaMissoesAtivas.Clear();
        ListaButoesMissoes.Clear();  

    }

    void comecarDialogo(int i)
    {

        if(utilis.totalMissoes < 3 || ListaMissoesNpc[i].status == "concluida")
        {
            if(ListaMissoesNpc[i].status == "concluida")
                fase = 1000;

            desativarMenuMissoes();
            StartCoroutine(dialogoUpdate(i));
            ListaButoesMissoes.Clear(); 
        }

    }

    // Metodo para associar o metodo de conversa com o botao
    private void AssignButtonOnClick (int indexOfButtonAndValue)
    {
        ListaButoesMissoes[indexOfButtonAndValue].onClick.AddListener
        (
            delegate{ comecarDialogo (indexOfButtonAndValue); }
        );
    }

    IEnumerator dialogoUpdate(int i)
    {
        while (true)
        {
            yield return new WaitForFixedUpdate();
            dialogo(i);
        }
    }

    IEnumerator desbloquearMissoesTimer()
    {
        yield return new WaitForSeconds(1);
        desbloquearMissoes();
    }

}
