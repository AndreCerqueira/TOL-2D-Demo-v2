using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class loja : MonoBehaviour
{
    private Canvas _canvas;
    private GameObject _loja;
    private GameObject _inv;

    private static List<Slot> slot = new List<Slot>();

    public static bool lojaCriada;

    // Update is called once per frame
    void Start()
    {
        _canvas = GameObject.FindGameObjectWithTag("Canvas").GetComponent<Canvas>();
        _loja = _canvas.transform.Find("Loja").gameObject;
        _inv = _canvas.transform.Find("Inventario").gameObject;
    }

    void Update()
    {
        if(_loja.activeSelf)
        {
            _inv.transform.Find("Slots").localPosition = new Vector2(81.3f, -18.4f);
            _inv.SetActive(true);
            _inv.transform.Find("fundo").gameObject.SetActive(false);
            _inv.transform.Find("bt_close").gameObject.SetActive(false);
            _inv.transform.Find("titulo").gameObject.SetActive(false);
            _inv.transform.Find("BordaImagem").gameObject.SetActive(false);
            _inv.transform.Find("Stats").gameObject.SetActive(false);
            //print("on");
        }
        /*else
        {
            print("off");
        }*/
    }

    public void reporInv()
    {
        _inv.transform.Find("Slots").localPosition = new Vector2(62.1f, -21.2f);
        _inv.SetActive(false);
        _inv.transform.Find("fundo").gameObject.SetActive(true);
        _inv.transform.Find("bt_close").gameObject.SetActive(true);
        _inv.transform.Find("titulo").gameObject.SetActive(true);
        _inv.transform.Find("BordaImagem").gameObject.SetActive(true);
        _inv.transform.Find("Stats").gameObject.SetActive(true);
    }
}
