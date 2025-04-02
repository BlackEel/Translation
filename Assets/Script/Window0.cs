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
            Debug.LogError("Data.IsOpened δ��ȷ��ʼ����");
            return;
        }
        if (WindowController.windowControllerInstance == null)
        {
            Debug.LogError("WindowController ʵ��δ���䣡");
            return; // ��ǰ�˳��������������
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
