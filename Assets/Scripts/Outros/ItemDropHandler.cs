using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ItemDropHandler : MonoBehaviour, IDropHandler
{

    public void OnDrop(PointerEventData eventData)
    {
        RectTransform invPanel = transform as RectTransform;

        if(!RectTransformUtility.RectangleContainsScreenPoint(invPanel, Input.mousePosition))
        {
            // "Slot (0)"
            var start = invPanel.name.IndexOf("(") + 1;
            var slotInicial = invPanel.name.Substring(start, invPanel.name.IndexOf(")") - start);

            Vector2 pos = Input.mousePosition;
            float dist = Vector3.Distance(pos, transform.position);
            float melhorDist = 1000;
            Slot target = null;

            int teste = 0;


            for (int i = 0; i < 30; i++)
            {
                if(i.ToString() != slotInicial)
                {
                    if(Vector3.Distance(pos, Inventario.slot[i].espaco.GetComponent<Transform>().position) < melhorDist)
                    {
                        melhorDist = Vector3.Distance(pos, Inventario.slot[i].espaco.GetComponent<Transform>().position);
                        target = Inventario.slot[i];
                        teste = i;
                    }
                }
            }

            Inventario _inventario = FindObjectOfType<Inventario>();
            _inventario.trocarDeSlot(Inventario.slot[int.Parse(slotInicial)], target);
        }

    }

}
