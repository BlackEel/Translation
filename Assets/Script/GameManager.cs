using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject chatThreadPrefab;
    private WindowController windowController;
    private void Start()
    {
        windowController = FindFirstObjectByType<WindowController>();
        WindowController.windowControllerInstance.OpenWindow(1);
        WindowController.windowControllerInstance.HidePanel(1);

        ReadJson chatThread = Instantiate(chatThreadPrefab).GetComponent<ReadJson>();
        chatThread.sceneId = "100";
        chatThread.StartScene();
    }
}
