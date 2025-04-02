using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class PageController : MonoBehaviour
{
    private CanvasRenderer canvasRenderer;
    private void Awake()
    {
        if (canvasRenderer == null)
        {
            canvasRenderer = transform.parent.transform.Find("Image").GetComponent<CanvasRenderer>();
        }
    }

    public void BeChosen()
    {
        Debug.Log("bechosen");

        foreach (Transform child in transform.parent.parent.transform)
        {
            Renderer renderer = child.GetComponent<Renderer>();
            if (renderer != null)
            {
                renderer.enabled = false; // Ω˚”√‰÷»æ∆˜
            }
        }

        transform.parent.GetComponent<Renderer>().enabled = true;
    }
}
