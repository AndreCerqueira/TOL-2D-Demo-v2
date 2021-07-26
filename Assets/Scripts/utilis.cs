using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class utilis : MonoBehaviour
{
    // Este metodo transforma o valor de xp que o player vai receber, sabendo o xp base o nivel do mob e o nivel do player
    public static float receberXp(float xpBase, int nivelMob, int nivelPlayer)
    {
        // Diferença de niveis entre o player e o mob
        int difLevel = nivelMob - nivelPlayer;
        float mult = 0;

        if (difLevel > -4 && difLevel < 4)
        {
            switch (difLevel)
            {
                case 3: mult = 1.75f; break;
                case 2: mult = 1.50f; break;
                case 1: mult = 1.25f; break;
                case 0: mult = 1f; break;
                case -1: mult = 0.75f; break;
                case -2: mult = 0.50f; break;
                case -3: mult = 0.25f; break;
            }
        }
        else
        {
            if (difLevel >= 4)
                mult = 2f;
            else if (difLevel <= -4)
                mult = 0f;
        }

        return xpBase * mult;

    }

    public static float MapValue(float a0, float a1, float b0, float b1, float a)
    {
        return b0 + (b1 - b0) * ((a-a0)/(a1-a0));
    }

    public static int maxXp(int nivel)
    {
        int result = 0;

        if (nivel == 1)
            result = 25;
        else
            result = 25 * nivel * nivel - 25 * nivel;

        return result;
    }

    #region Missoes

        public static int totalMissoes;
        public static List<MissaoPlayer> ListaMissoesPlayer = new List<MissaoPlayer>();
        public static GameObject[] MissaoAtiva = new GameObject[4];

        // Metodo responsavel por criar a missao.
        public static void addQuest(MissaoPlayer p_MissaoPlayer)
        {        
            if(totalMissoes < 3)
            {
                Vector2 _posObj = Vector2.zero;
                
                totalMissoes++;

                MissaoPlayer _missao = p_MissaoPlayer;

                if(totalMissoes == 1)
                    _missao.posObj = new Vector2(0, 66f);
                else if(totalMissoes == 2)
                    _missao.posObj = new Vector2(0, 22f);
                else if(totalMissoes == 3)
                    _missao.posObj = new Vector2(0, -22f);

                ListaMissoesPlayer.Add(_missao);
                
                _missao.id = totalMissoes;

                getQuest(_missao);
            }
        }

        // Metodo responsavel para receber as quests ativas, quando troca de cena ou entra no jogo
        public static void showQuest(int id)
        {
            Canvas _canvas = GameObject.FindGameObjectsWithTag("Canvas")[0].GetComponent<Canvas>();
            GameObject _missoesInCanvas = _canvas.transform.Find("Missoes").gameObject;

            MissaoPlayer _missao = ListaMissoesPlayer.Find(x => x.id == id);

            getQuest(_missao);

        }

        // Metodo responsavel por criar o prefab da missao no jogo
        public static void getQuest(MissaoPlayer _missao)
        {
            Canvas _canvas = GameObject.FindGameObjectsWithTag("Canvas")[0].GetComponent<Canvas>();
            GameObject _missoesInCanvas = _canvas.transform.Find("Missoes").gameObject;

            GameObject _missaoObj = Resources.Load ("Missao") as GameObject;
            GameObject _instantiated = Instantiate(_missaoObj, _missoesInCanvas.transform.position, Quaternion.identity);
            _instantiated.transform.parent = _missoesInCanvas.transform;
            _instantiated.transform.localScale = new Vector2(1,1);
            _instantiated.transform.localPosition = _missao.posObj;

            Image _img = _instantiated.transform.Find("Imagem").GetComponent<Image>();
            _img.sprite = Resources.Load<Sprite>(_missao.imagem);
            _img.GetComponent<Transform>().localPosition = _missao.posImagem;

            Text _texto = _instantiated.transform.Find("objetivo/text").GetComponent<Text>();

            if(_missao.tipo == 3)
                _texto.text = " Encontre ";
            else
                _texto.text = " " + _missao.progresso + " / " + _missao.objetivo + " ";

            MissaoAtiva[_missao.id] = _instantiated;

        }

        // Metodo responsavel por aumentar o progresso nas missoes
        public static void continuarQuest(string _target)
        {
            if (ListaMissoesPlayer.Count > 0)
            {
                for (int i = 1; i <= ListaMissoesPlayer.Count; i++)
                {
                    MissaoPlayer _missao = ListaMissoesPlayer.Find(x => x.id == i);

                    if(_missao.target == _target)
                    {
                        _missao.progresso ++;
                        updateQuest(i);
                    }
                }
            }
        }

        public static void addProgresso(int quant, int i)
        {
            MissaoPlayer _missao = ListaMissoesPlayer.Find(x => x.id == i);
            _missao.progresso += quant;
            updateQuest(i);
        }

        // Metodo responsavel por atualizar as missoes na lista no jogo
        public static void updateQuest(int id)
        {
            MissaoPlayer _missao = ListaMissoesPlayer.Find(x => x.id == id);
            
            if(_missao.progresso < _missao.objetivo)
            {
                MissaoAtiva[id].transform.Find("objetivo/text").GetComponent<Text>().text = " " + _missao.progresso + " / " + _missao.objetivo + " ";
            }
            else
            {
                MissaoAtiva[id].transform.Find("objetivo/text").GetComponent<Text>().text = " Encontre ";
                MissaoAtiva[id].transform.Find("Imagem").GetComponent<Image>().sprite = Resources.Load<Sprite>("Icons/" + _missao.npc);
                MissaoAtiva[id].transform.Find("Imagem").GetComponent<Transform>().localPosition = _missao.posNpc;

                NpcController[] _npcController = FindObjectsOfType<NpcController>();
                for (int i = 0; i < _npcController.Length; i++)
                {
                    for (int j = 0; j < _npcController[i].ListaMissoesNpc.Count; j++)
                    {
                        if(_npcController[i].ListaMissoesNpc[j].id == _missao.idNpc && _npcController[i].ListaMissoesNpc[j].sender == _missao.npc)
                        {
                            if(_npcController[i].ListaMissoesNpc[j].status != "completada")
                                _npcController[i].ListaMissoesNpc[j].status = "concluida";
                            break;
                        }
                    }
                }
                
            }

        }

        public static void completarQuest(string npc)
        {
            List<MissaoPlayer> results = ListaMissoesPlayer.FindAll(x => x.npc == npc);
            int id_Removido = 0;


            foreach (var item in results)
            {
                if(item.progresso >= item.objetivo)
                    id_Removido = item.id;
            }

            MissaoPlayer _missao = ListaMissoesPlayer.Find(x => x.id == id_Removido);
            ListaMissoesPlayer.Remove(_missao);

            GameManager _gameManager = FindObjectOfType<GameManager>();
            _gameManager.apagarDadosMissoao(id_Removido);

            Destroy(MissaoAtiva[id_Removido]);

            totalMissoes--;

            corrigirOrdemQuests(id_Removido);
        }

        public static void corrigirOrdemQuests(int id_Removido)
        {
            for (int i = 1; i <= MissaoAtiva.Length -1; i++)
                Destroy(MissaoAtiva[i]);

            for (int i = 0; i <= ListaMissoesPlayer.Count -1; i++)
            { 
                if(ListaMissoesPlayer[i].id > id_Removido)
                    ListaMissoesPlayer[i].id -= 1;
            
            }
    
            for (int i = 0; i <= ListaMissoesPlayer.Count -1; i++)
            {
                switch (ListaMissoesPlayer[i].id)
                {
                    case 1: ListaMissoesPlayer[i].posObj = new Vector2(0, 66f);
                    break;
                    case 2: ListaMissoesPlayer[i].posObj = new Vector2(0, 22f);
                    break;
                    case 3: ListaMissoesPlayer[i].posObj = new Vector2(0, -22f);
                    break;
                }   
            }

            for (int i = 1; i <= utilis.ListaMissoesPlayer.Count; i++)
            {
                utilis.showQuest(i);
                utilis.updateQuest(i);
            }


        }

    #endregion

    #region Dialogo dos Npcs

        // Metodo usado para falas só de texto
        public static void addFala(GameObject _caixaDialogo, string _texto)
        {
            _caixaDialogo.transform.Find("BordaTexto/Recompensa").gameObject.SetActive(false);
            _caixaDialogo.transform.Find("bt_proximo").gameObject.SetActive(true);
            _caixaDialogo.transform.Find("bt_receber").gameObject.SetActive(false);
            _caixaDialogo.transform.Find("bt_aceitar").gameObject.SetActive(false);
            _caixaDialogo.transform.Find("bt_recusar").gameObject.SetActive(false);
            _caixaDialogo.transform.Find("BordaTexto/Text").GetComponent<Text>().text = _texto;
        }

        public static void addFala(GameObject _caixaDialogo, int _tipo, int _xp, int _dinheiro)
        {
            switch (_tipo)
            {
                case 1:
                    _caixaDialogo.transform.Find("bt_proximo").gameObject.SetActive(false);
                    _caixaDialogo.transform.Find("bt_receber").gameObject.SetActive(false);
                    _caixaDialogo.transform.Find("bt_aceitar").gameObject.SetActive(true);
                    _caixaDialogo.transform.Find("bt_recusar").gameObject.SetActive(true);
                    _caixaDialogo.transform.Find("BordaTexto/Text").GetComponent<Text>().text = "Recompensas depois de completar a missão! Vais aceitar?";
                break;

                case 2:
                    _caixaDialogo.transform.Find("bt_proximo").gameObject.SetActive(false);
                    _caixaDialogo.transform.Find("bt_receber").gameObject.SetActive(true);
                    _caixaDialogo.transform.Find("bt_aceitar").gameObject.SetActive(false);
                    _caixaDialogo.transform.Find("bt_recusar").gameObject.SetActive(false);
                    _caixaDialogo.transform.Find("BordaTexto/Text").GetComponent<Text>().text = "Obrigado pela ajuda! Aqui está a tua recompensa!";
                break;
            }

            _caixaDialogo.transform.Find("BordaTexto/Recompensa").gameObject.SetActive(true);
            _caixaDialogo.transform.Find("BordaTexto/Recompensa/xp/text").GetComponent<Text>().text = _xp.ToString();
            _caixaDialogo.transform.Find("BordaTexto/Recompensa/moedas/text").GetComponent<Text>().text = _dinheiro.ToString();
        }

    #endregion

}
