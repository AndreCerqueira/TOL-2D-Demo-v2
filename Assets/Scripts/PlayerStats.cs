using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStats : CharacterStats
{
    protected Slider barraEnergia;
    protected Slider barraXp;

    protected Text barraVidaText;
    protected Text barraEnergiaText;
    protected Text barraXpText;

    private float currentEnergia;
    private static float currentXp;
    public int maxEnergia;
    public float maxXp;

    private Text dinheiroObj;
    private static int currentDinheiro;

    private Canvas _canvas;

    // Start is called before the first frame update
    void Awake()
    {
        _canvas = GameObject.FindGameObjectsWithTag("Canvas")[0].GetComponent<Canvas>();

        atribuirStats();

        nivelText = _canvas.transform.Find("PlayerStats/Nivel/NivelText").GetComponent<Text>();

        barraVidaText = _canvas.transform.Find("PlayerStats/BarraVida/Slider/Fill Area/BarraText").GetComponent<Text>();
        barraXpText = _canvas.transform.Find("PlayerStats/BarraXp/Slider/Fill Area/BarraText").GetComponent<Text>();
        barraEnergiaText = _canvas.transform.Find("PlayerStats/BarraEnergia/Slider/Fill Area/BarraText").GetComponent<Text>();

        StartCoroutine(regen());
    }

    // Update is called once per frame
    void Update()
    {
        barraVida.value = Mathf.Lerp(barraVida.value, currentVida, 0.25f);
        barraVidaText.text = "HP: " + currentVida.ToString() + "/" + barraVida.maxValue;

        barraXp.value = Mathf.Lerp(barraXp.value, currentXp, 0.05f);
        barraXpText.text = "XP: " + currentXp.ToString() + "/" + barraXp.maxValue;

        barraEnergia.value = Mathf.Lerp(barraEnergia.value, currentEnergia, 0.05f);
        barraEnergiaText.text = "AP: " + currentEnergia.ToString() + "/" + barraEnergia.maxValue;

        nivelText.text = nivel.ToString();
        dinheiroObj.text = dinheiro.ToString();

        if (Mathf.Round(barraXp.value) >= barraXp.maxValue)
        {
            //aviso.SetActive(true);
            nivel ++;
            //avisoText.text = "Passou para o nivel " + nivel + "!";

            maxVida += 10;
            barraVida.maxValue = maxVida;
            currentVida = (int)Mathf.Round(barraVida.maxValue);
            maxEnergia += 10;
            barraEnergia.maxValue = maxEnergia;
            currentEnergia = barraVida.maxValue;

            currentXp -= barraXp.maxValue;
            barraXp.value = 0;
            barraXp.maxValue = utilis.maxXp(nivel);
            maxXp = barraXp.maxValue;
            nivelText.text = nivel.ToString();

            NpcController[] _npcController = FindObjectsOfType<NpcController>();
            for (int i = 0; i < _npcController.Length; i++)
                _npcController[i].desbloquearMissoes();
        }
    }
    
    public void atribuirStats()
    {
        dinheiroObj = _canvas.transform.Find("Inventario/Stats/Dinheiro/text").GetComponent<Text>();
        dinheiroObj.text = currentDinheiro.ToString();

        barraVida = _canvas.transform.Find("PlayerStats/BarraVida/Slider").GetComponent<Slider>();
        barraEnergia = _canvas.transform.Find("PlayerStats/BarraEnergia/Slider").GetComponent<Slider>();
        barraXp = _canvas.transform.Find("PlayerStats/BarraXp/Slider").GetComponent<Slider>();

        barraVida.maxValue = maxVida;
        barraVida.value = barraVida.maxValue;
        currentVida = (int)barraVida.value;

        barraEnergia.maxValue = maxEnergia;
        barraEnergia.value = barraEnergia.maxValue;
        currentEnergia = barraEnergia.value;

        maxXp = utilis.maxXp(nivel);
        barraXp.maxValue = maxXp;
    }


    public override void Morrer()
    {
        base.Morrer();
        GetComponent<PlayerController>().enabled = false;
            
    }

    public float xp
    {
        get { return currentXp; }
        set { currentXp = value; }
    }

    public int dinheiro
    {
        get { return currentDinheiro; }
        set { currentDinheiro = value; }
    }

    public void atualizarInv()
    {
        dinheiroObj.text = currentDinheiro.ToString();
    }

    IEnumerator regen()
    {
        while (true)
        {
            yield return new WaitForSeconds(10f);
            if (currentVida + (int)Mathf.Round(maxVida * 0.05f) <= maxVida)
            {
                if((int)Mathf.Round(maxVida * 0.05f) <= 0.5f)
                    Regenerar(1);
                else
                    Regenerar((int)Mathf.Round(maxVida * 0.05f));
            }
            else if (currentVida < maxVida)
            {
                int dif = maxVida - currentVida;
                Regenerar(dif);
            }
        }
    }

}
