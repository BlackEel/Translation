using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Window1 : MonoBehaviour, IPointerDownHandler
{
    public void OnPointerDown(PointerEventData eventData)
    {
        GetComponent<RectTransform>().SetAsLastSibling();
    }

    public void ClickTaskBar()
    {
        if (Data.IsOpened[1] == true)
        {
            WindowController.windowControllerInstance.UnhidePanel(1);
        }
        else
        {
            WindowController.windowControllerInstance.OpenWindow(1);
        }
    }

    public void ClickCloseButton()
    {
        WindowController.windowControllerInstance.HidePanel(1);
    }

    public void ClickMinimizeButton()
    {
        WindowController.windowControllerInstance.HidePanel(1);
    }
}
