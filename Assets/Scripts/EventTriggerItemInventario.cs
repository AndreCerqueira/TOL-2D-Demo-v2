using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class EventTriggerItemInventario : MonoBehaviour
{
    public EventTrigger trigger;
    private Canvas _canvas;
    private GameObject _infoItem;

    void Start()
    {
        _canvas = GameObject.FindGameObjectWithTag("Canvas").GetComponent<Canvas>();
        _infoItem = _canvas.transform.Find("InfoDrop").gameObject;

        trigger = GetComponent<EventTrigger>( );
        EventTrigger.Entry entry = new EventTrigger.Entry( );
        entry.eventID = EventTriggerType.PointerEnter;
        entry.callback.AddListener( ( data ) => { porCimaEntrar( (PointerEventData) data); } );
        trigger.triggers.Add( entry );

        entry = new EventTrigger.Entry( );
        entry.eventID = EventTriggerType.PointerExit;
        entry.callback.AddListener( ( data ) => { porCimaSair( ( PointerEventData ) data ); } );
        trigger.triggers.Add( entry );
    }

    public void porCimaEntrar(PointerEventData data)
    {
        Image other = transform.Find("espaco").GetComponent<Image>();
        if(other.enabled)
        {
            _infoItem.SetActive(true);
            Text nome = _infoItem.transform.Find("nome").GetComponent<Text>();
            nome.text = other.sprite.name;
            _infoItem.transform.position = Input.mousePosition;
        }
    }

    public void porCimaSair(PointerEventData data)
    {
        _infoItem.SetActive(false);
    }
}
