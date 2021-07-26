using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private GameObject player;
    private Canvas _canvas;
    private GameObject CaixaDialogo;
    private int dialogoIndex;
    public static string ultimoPortal;
    public static bool primeiraEntrada;

    // Start is called before the first frame update
    void Start()
    {
        if(PlayerPrefs.HasKey("fase") && !primeiraEntrada)
        {
            primeiraEntrada = true;
            SceneManager.LoadScene(PlayerPrefs.GetInt("fase"));
        }
        
        player = FindObjectOfType<PlayerController>().gameObject;
        _canvas = GameObject.FindGameObjectWithTag("Canvas").GetComponent<Canvas>();
        CaixaDialogo = _canvas.transform.Find("CaixaDialogo").gameObject;

        for (int i = 1; i <= utilis.ListaMissoesPlayer.Count; i++)
        {
            utilis.showQuest(i);
            utilis.updateQuest(i);
        }

        //apagarDados();
        chamarDados();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {   
            guardarDados();
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);   
        }

        if (Input.GetKeyDown(KeyCode.I))
            menuInventario();  

        if (Input.GetKeyDown(KeyCode.P))
        {
            apagarDados();
        }     
    }

    void OnApplicationQuit()
    {
        guardarDados();
    }

    #region botoes

        public void menuInventario()
        {
            GameObject inv = _canvas.transform.Find("Inventario").gameObject;
            inv.SetActive(!inv.activeSelf);
            player.GetComponent<PlayerStats>().atualizarInv();
        }

        public void menuShop()
        {
            GameObject shop = _canvas.transform.Find("Loja").gameObject;
            shop.SetActive(!shop.activeSelf);
            player.GetComponent<PlayerStats>().atualizarInv();

            
            loja loja = FindObjectOfType<loja>();
                loja.reporInv();
        }

        #region botoes Dialogo

            public void proximo()
            {
                GameObject _npcTarget = npcTarget();

                if(dialogoIndex > 100)
                    dialogoIndex = 1;
                else
                    dialogoIndex++;
        
                _npcTarget.GetComponent<NpcController>().fase = dialogoIndex;
                //_npcTarget.GetComponent<NpcController>().ListaDialogos[_npcTarget.GetComponent<NpcController>().missaoNumero]();
            }

            public void aceitar()
            {
                GameObject _npcTarget = npcTarget();

                dialogoIndex = 101;
                _npcTarget.GetComponent<NpcController>().fase = dialogoIndex;
                //_npcTarget.GetComponent<NpcController>().ListaDialogos[_npcTarget.GetComponent<NpcController>().missaoNumero]();
            }

            public void recusar()
            {
                GameObject _npcTarget = npcTarget();

                dialogoIndex = 102;
                _npcTarget.GetComponent<NpcController>().fase = dialogoIndex;
                //_npcTarget.GetComponent<NpcController>().ListaDialogos[_npcTarget.GetComponent<NpcController>().missaoNumero]();
            }

            public void receber()
            {
                GameObject _npcTarget = npcTarget();

                _npcTarget.GetComponent<NpcController>().ultimoDialogo();

                int _dinheiro = int.Parse(_canvas.transform.Find("CaixaDialogo/BordaTexto/Recompensa/moedas/text").GetComponent<Text>().text);
                int _xp = int.Parse(_canvas.transform.Find("CaixaDialogo/BordaTexto/Recompensa/xp/text").GetComponent<Text>().text);

                player.GetComponent<PlayerStats>().dinheiro += _dinheiro;
                player.GetComponent<PlayerStats>().xp += _xp;
                player.GetComponent<PlayerStats>().atualizarInv();
            }

            private GameObject npcTarget()
            {
                string _nome = CaixaDialogo.transform.Find("titulo/text").GetComponent<Text>().text;
                GameObject _npcs = GameObject.FindGameObjectsWithTag("Npc")[0];
                GameObject _npcTarget = _npcs.transform.Find(_nome).gameObject;
                
                return _npcTarget;
            }

        #endregion

    #endregion

    #region Guardar Dados

        public void apagarDados()
        {
            PlayerPrefs.DeleteAll();
        }

        // Guardar progresso do jogador pode ser ativado pelo botao de guardar ou pela corrotina de 1 em 1min
        public void guardarDados()
        {
            PlayerPrefs.SetInt("fase", SceneManager.GetActiveScene().buildIndex);
            PlayerPrefs.SetString("ultimoPortal", ultimoPortal);

            PlayerPrefs.SetInt("maxVida", player.GetComponent<PlayerStats>().maxVida);
            PlayerPrefs.SetFloat("maxXp", player.GetComponent<PlayerStats>().maxXp);
            PlayerPrefs.SetInt("maxEnergia", player.GetComponent<PlayerStats>().maxEnergia);

            PlayerPrefs.SetInt("nivel", player.GetComponent<PlayerStats>().nivel);
            PlayerPrefs.SetFloat("currentXp", player.GetComponent<PlayerStats>().xp);

            PlayerPrefs.SetInt("dinheiro", player.GetComponent<PlayerStats>().dinheiro);

            for (int i = 0; i < 30; i++)
                PlayerPrefs.SetString("slot_" + i + "_item", Inventario.slot[i].item);

            for (int i = 0; i < 30; i++)
                PlayerPrefs.SetInt("slot_" + i + "_quant", Inventario.slot[i].quant);

             #region Guardar Missoes
                if(utilis.ListaMissoesPlayer.Count > 0){
                for (int i = 1; i <= utilis.ListaMissoesPlayer.Count; i++)
                {
                    PlayerPrefs.SetInt("missao_" + i + "_idNpc", utilis.ListaMissoesPlayer[i-1].idNpc);
                    PlayerPrefs.SetString("missao_" + i + "_imagem", utilis.ListaMissoesPlayer[i-1].imagem);

                    PlayerPrefs.SetFloat("missao_" + i + "_posImagemX", utilis.ListaMissoesPlayer[i-1].posImagem.x);
                    PlayerPrefs.SetFloat("missao_" + i + "_posImagemY", utilis.ListaMissoesPlayer[i-1].posImagem.y);
                    PlayerPrefs.SetFloat("missao_" + i + "_posObjX", utilis.ListaMissoesPlayer[i-1].posObj.x);
                    PlayerPrefs.SetFloat("missao_" + i + "_posObjY", utilis.ListaMissoesPlayer[i-1].posObj.y);

                    PlayerPrefs.SetInt("missao_" + i + "_objetivo", utilis.ListaMissoesPlayer[i-1].objetivo);
                    PlayerPrefs.SetInt("missao_" + i + "_progresso", utilis.ListaMissoesPlayer[i-1].progresso);
                    PlayerPrefs.SetInt("missao_" + i + "_tipo", utilis.ListaMissoesPlayer[i-1].tipo);
                    PlayerPrefs.SetString("missao_" + i + "_target", utilis.ListaMissoesPlayer[i-1].target);
                    PlayerPrefs.SetString("missao_" + i + "_npc", utilis.ListaMissoesPlayer[i-1].npc);

                    PlayerPrefs.SetFloat("missao_" + i + "_posNpcX", utilis.ListaMissoesPlayer[i-1].posNpc.x);
                    PlayerPrefs.SetFloat("missao_" + i + "_posNpcY", utilis.ListaMissoesPlayer[i-1].posNpc.y);
                }
                PlayerPrefs.SetInt("Total_missoes", utilis.ListaMissoesPlayer.Count);
                }
            #endregion

            #region Guardar Estado das Missoes
                NpcController[] _npcController = FindObjectsOfType<NpcController>();
                for (int i = 0; i < _npcController.Length; i++)
                {
                    for (int j = 0; j < _npcController[i].ListaMissoesNpc.Count; j++)
                    {
                        PlayerPrefs.SetString("status_"+_npcController[i].ListaMissoesNpc[j].sender + "_"+ _npcController[i].ListaMissoesNpc[j].id, _npcController[i].ListaMissoesNpc[j].status);
                    }
                }
            #endregion

        }

        // Chamar o progresso do jogador que foi guardado
        public void chamarDados()
        {
            Vector3 _ultimoPortal = Vector3.zero; 
            if(PlayerPrefs.GetString("ultimoPortal") == "dir")
                _ultimoPortal = new Vector3(33.25f,0.17f,0);
            else //if(PlayerPrefs.GetString("ultimoPortal") == "esq")
                _ultimoPortal = new Vector3(-30.42f,0.17f,0);

            player.transform.position = _ultimoPortal;

            var _nivel = (PlayerPrefs.HasKey("nivel") ? PlayerPrefs.GetInt("nivel") : 1);
            player.GetComponent<PlayerStats>().nivel = _nivel;

            var _currentXp = (PlayerPrefs.HasKey("currentXp") ? PlayerPrefs.GetFloat("currentXp") : 0);
            player.GetComponent<PlayerStats>().xp = _currentXp;

            var _maxXp = (PlayerPrefs.HasKey("maxXp") ? PlayerPrefs.GetFloat("maxXp") : 25);
            player.GetComponent<PlayerStats>().maxXp = _maxXp;

            var _maxEnergia = (PlayerPrefs.HasKey("maxEnergia") ? PlayerPrefs.GetInt("maxEnergia") : 10);
            player.GetComponent<PlayerStats>().maxEnergia = _maxEnergia;

            var _maxVida = (PlayerPrefs.HasKey("maxVida") ? PlayerPrefs.GetInt("maxVida") : 10);
            player.GetComponent<PlayerStats>().maxVida = _maxVida;

            var _dinheiro = (PlayerPrefs.HasKey("dinheiro") ? PlayerPrefs.GetInt("dinheiro") : 0);
            player.GetComponent<PlayerStats>().dinheiro = _dinheiro;

            player.GetComponent<PlayerStats>()._currentVida = player.GetComponent<PlayerStats>().maxVida;

            player.GetComponent<PlayerStats>().atribuirStats();

            for (int i = 0; i < 30; i++)
            {
                Inventario _inventario = FindObjectOfType<Inventario>();
                if(PlayerPrefs.GetString("slot_" + i + "_item") != "")
                    _inventario.adicionarNoInventario(PlayerPrefs.GetString("slot_" + i + "_item"), PlayerPrefs.GetInt("slot_" + i + "_quant"), i);
            }

             #region Chamar Missoes
                if(utilis.ListaMissoesPlayer.Count <= 0){
                    for (int i = 1; i <= PlayerPrefs.GetInt("Total_missoes"); i++)
                    {
                        MissaoPlayer temp = new MissaoPlayer
                        {
                            idNpc = PlayerPrefs.GetInt("missao_" + i + "_idNpc"),
                            imagem = PlayerPrefs.GetString("missao_" + i + "_imagem"),
                            posImagem = new Vector2(PlayerPrefs.GetFloat("missao_" + i + "_posImagemX"), PlayerPrefs.GetFloat("missao_" + i + "_posImagemY")),
                            tipo = PlayerPrefs.GetInt("missao_" + i + "_tipo"),
                            objetivo = PlayerPrefs.GetInt("missao_" + i + "_objetivo"),
                            target = PlayerPrefs.GetString("missao_" + i + "_target"),
                            npc = PlayerPrefs.GetString("missao_" + i + "_npc"),
                            posNpc = new Vector2(PlayerPrefs.GetFloat("missao_" + i + "_posNpcX"), PlayerPrefs.GetFloat("missao_" + i + "_posNpcY")),
                        };

                        utilis.addQuest(temp);
                        utilis.addProgresso(PlayerPrefs.GetInt("missao_" + i + "_progresso"), i);
                    }
                }
            #endregion

            #region Chamar Estado das Missoes
                NpcController[] _npcController = FindObjectsOfType<NpcController>();
                for (int i = 0; i < _npcController.Length; i++)
                {
                    for (int j = 0; j < _npcController[i].ListaMissoesNpc.Count; j++)
                    {
                        _npcController[i].ListaMissoesNpc[j].status = PlayerPrefs.GetString("status_"+_npcController[i].ListaMissoesNpc[j].sender + "_"+ _npcController[i].ListaMissoesNpc[j].id);
                    }
                }
            #endregion
         
        }

        public void apagarDadosMissoao(int i)
        {
            PlayerPrefs.DeleteKey("missao_" + i + "_idNpc");
            PlayerPrefs.DeleteKey("missao_" + i + "_imagem");

            PlayerPrefs.DeleteKey("missao_" + i + "_posImagemX");
            PlayerPrefs.DeleteKey("missao_" + i + "_posImagemY");
            PlayerPrefs.DeleteKey("missao_" + i + "_posObjX");
            PlayerPrefs.DeleteKey("missao_" + i + "_posObjY");

            PlayerPrefs.DeleteKey("missao_" + i + "_objetivo");
            PlayerPrefs.DeleteKey("missao_" + i + "_progresso");
            PlayerPrefs.DeleteKey("missao_" + i + "_tipo");
            PlayerPrefs.DeleteKey("missao_" + i + "_target");
            PlayerPrefs.DeleteKey("missao_" + i + "_npc");

            PlayerPrefs.DeleteKey("missao_" + i + "_posNpcX");
            PlayerPrefs.DeleteKey("missao_" + i + "_posNpcY");
            
            PlayerPrefs.SetInt("Total_missoes", utilis.ListaMissoesPlayer.Count);
        }

    #endregion

}
