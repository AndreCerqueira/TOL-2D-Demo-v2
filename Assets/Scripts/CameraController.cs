using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    private GameObject player;
    [SerializeField] private float offset;
    private Vector3 vel = Vector3.zero;

    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<PlayerController>().gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        float posX = 0;
        if(player.transform.position.x <= -22)
            posX = -22;
        else if(player.transform.position.x >= 22)
            posX = 22;
        else
            posX = player.transform.position.x;
            
        transform.position = Vector3.SmoothDamp(transform.position, new Vector3(posX, player.transform.position.y + offset, player.transform.position.z -10), ref vel , 0.15f);
   }
}
