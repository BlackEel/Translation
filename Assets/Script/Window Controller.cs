using System.Collections;
using System.Collections.Generic;
using UnityEditor.PackageManager.UI;
using UnityEngine;

public class WindowController : MonoBehaviour
{
    // 用来存储所有的窗口对象
    private GameObject[] windows = new GameObject[3];
    public GameObject[] prefabs;
    
    private static WindowController windowController;
    public static WindowController windowControllerInstance => windowController;
    
    public GameObject windowParent;
    public static CanvasGroup panelCanvasGroup;

    private void Awake()
    {
        if (windowController != null && windowController != this)
        {
            Destroy(gameObject);
            return;
        }
        windowController = this;
        DontDestroyOnLoad(gameObject);

        panelCanvasGroup = GetComponent<CanvasGroup>();


    }

    public void OpenWindow(int _prefabIndex)
    {
        windows[_prefabIndex] = Instantiate(prefabs[_prefabIndex], new Vector2(Screen.width/2, Screen.height/2), Quaternion.identity, windowParent.transform);
        Data.IsOpened[_prefabIndex] = true;
    }
    public void UnhidePanel(int _prefabIndex)
    {
        panelCanvasGroup = windows[_prefabIndex].GetComponent<CanvasGroup>();
        panelCanvasGroup.alpha = 1;
        panelCanvasGroup.blocksRaycasts = true;
        panelCanvasGroup.interactable = true;
        Debug.Log("window" + _prefabIndex + "unhide");
    }
    public void HidePanel(int _prefabIndex)
    {
        panelCanvasGroup = windows[_prefabIndex].GetComponent<CanvasGroup>();
        panelCanvasGroup.alpha = 0;
        panelCanvasGroup.blocksRaycasts = false;
        panelCanvasGroup.interactable = false;
        Debug.Log("window" + _prefabIndex + "hide");
    }
    public void ShutdownPanel(int _windowIndex)
    {
        Destroy(windows[_windowIndex]);
        Data.IsOpened[_windowIndex] = false;
    }
}