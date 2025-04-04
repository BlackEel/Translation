using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject chatThreadPrefab;
    private void Start()
    {
        ReadJson chatThread = Instantiate(chatThreadPrefab).GetComponent<ReadJson>();
        chatThread.sceneId = "100";
        chatThread.StartScene();
    }
}
