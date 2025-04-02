using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DragWindow : MonoBehaviour, IDragHandler, IPointerDownHandler
{
    private RectTransform panelRectTrans;

    private void Awake()
    {
        if (panelRectTrans == null)
        {
            panelRectTrans = transform.parent.GetComponent<RectTransform>();
        }
    }

    public void OnDrag(PointerEventData eventData)
    {
        panelRectTrans.anchoredPosition += eventData.delta;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        panelRectTrans.SetAsLastSibling();
    }
}
