using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Window0 : MonoBehaviour, IPointerDownHandler
{
    public void OnPointerDown(PointerEventData eventData)
    {
        GetComponent<RectTransform>().SetAsLastSibling();
    }

    public void ClickTaskBar()
    {
        if (Data.IsOpened == null || Data.IsOpened.Length == 0)
        {
            Debug.LogError("Data.IsOpened 未正确初始化！");
            return;
        }
        if (WindowController.windowControllerInstance == null)
        {
            Debug.LogError("WindowController 实例未分配！");
            return; // 提前退出，避免后续错误
        }
        if (Data.IsOpened[0] == true)
        {
            WindowController.windowControllerInstance.UnhidePanel(0);
        }
        else
        {
            Debug.Log("clicktaskbar");
            WindowController.windowControllerInstance.OpenWindow(0);
        }
    }

    public void ClickCloseButton()
    {
        WindowController.windowControllerInstance.ShutdownPanel(0);
    }

    public void ClickMinimizeButton()
    {
        WindowController.windowControllerInstance.HidePanel(0);
    }
}
