using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RaulController : MonoBehaviour
{
    private Animator anim;
    private Canvas _canvas;
    private GameObject _shopMenu;
    private bool falar;
    private bool playerPerto;

    // Start is called before the first frame update
    void Start()
    {
        _canvas = GameObject.FindGameObjectWithTag("Canvas").GetComponent<Canvas>();
        _shopMenu = _canvas.transform.Find("Loja").gameObject;
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.E) && playerPerto)
        {
            falar = !falar;
            anim.SetBool("falar", falar);
            ativarMenuLoja();
        }

    }

    void ativarMenuLoja()
    {
        _shopMenu.SetActive(true);
        _shopMenu.transform.Find("tituloBase/BordaImg/img").GetComponent<Image>().sprite = Resources.Load<Sprite>("DialogoIcons/" + name);
        //_shopMenu.transform.Find("bt_close").GetComponent<Button>().onClick.AddListener(delegate { desativarMenuMissoes(); });
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

}
