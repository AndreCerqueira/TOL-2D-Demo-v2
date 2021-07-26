using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class teleporte : MonoBehaviour
{
    private GameObject player;
    private bool contactoPlayer;

    [SerializeField] private string tipoPortal;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.E) && contactoPlayer)
        {
            if(tipoPortal == "dir")
            {
                GameManager.ultimoPortal = "esq";
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
            }
            else if(tipoPortal == "esq")
            {
                GameManager.ultimoPortal = "dir";
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
            }
            FindObjectOfType<GameManager>().guardarDados();
        }
    }

    void OnTriggerStay2D(Collider2D other) 
    {
        if (other.gameObject.tag == "Player") 
            contactoPlayer = true;
    }

    void OnTriggerExit2D(Collider2D other) 
    {
        if (other.gameObject.tag == "Player") 
            contactoPlayer = false;
    }
}
