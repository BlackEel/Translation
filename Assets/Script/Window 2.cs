using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Window2 : MonoBehaviour, IPointerDownHandler
{
    public void OnPointerDown(PointerEventData eventData)
    {
        GetComponent<RectTransform>().SetAsLastSibling();
    }

    public void ClickTaskBar()
    {
        if (Data.IsOpened[2] == true)
        {
            WindowController.windowControllerInstance.UnhidePanel(2);
        }
        else
        {
            WindowController.windowControllerInstance.OpenWindow(2);
        }
    }

    public void ClickCloseButton()
    {
        WindowController.windowControllerInstance.ShutdownPanel(2);
    }

    public void ClickMinimizeButton()
    {
        WindowController.windowControllerInstance.HidePanel(2);
    }
}
