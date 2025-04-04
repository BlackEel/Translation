using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static UnityEngine.Rendering.DebugUI;

public class TextScript : MonoBehaviour
{
    [SerializeField] private Vector2 padding = new Vector2(40, 40);
    public GameObject chatBubblePrefab;
    public GameObject chatBubbleParent;
    private ReadJson readJson;
    private RectTransform panelRT;

    private float originHeight;
    private float panelHeight;
    private float totalHeight = 0;
    private int bubbleCount = 0;
    void Start()
    {
        panelRT = chatBubbleParent.GetComponent<RectTransform>();
        readJson = FindFirstObjectByType<ReadJson>();
        originHeight = panelRT.rect.height;
    }

    public void CreateChatBubble(int sender)
    {
        bubbleCount++;
        if (bubbleCount > 1)
        {
            MoveBubble(panelHeight);
        }
        //GameObject chatBubble = Instantiate(chatBubblePrefab,chatBubbleParent.transform);

        GameObject panel = new GameObject("Panel");
        panel.tag = "Bubble";
        panel.transform.SetParent(chatBubbleParent.transform);

        // 设置Panel的RectTransform
        RectTransform panelRect = panel.AddComponent<RectTransform>();

        if (sender == 0)
        {
            panelRect.anchorMin = new Vector2(0, 0);
            panelRect.anchorMax = new Vector2(0, 0);
            panelRect.pivot = new Vector2(0, 0);
            panelRect.sizeDelta = new Vector2(300, 200);
            panelRect.anchoredPosition = new Vector2(15, 15);
        }
        else
        {
            panelRect.anchorMin = new Vector2(1, 0);
            panelRect.anchorMax = new Vector2(1, 0);
            panelRect.pivot = new Vector2(1, 0);
            panelRect.sizeDelta = new Vector2(300, 200);
            panelRect.anchoredPosition = new Vector2(-15, 15);
        }

        Image panelImage = panel.AddComponent<Image>();
        panelImage.color = new Color(0.2f, 0.2f, 0.2f, 0.8f);

        Text textComponent = CreateText(panel, readJson.mesText);

        UpdatePanelSize(panel, textComponent);

        totalHeight = totalHeight + panelHeight;

        if(totalHeight > originHeight)
        {
            MoveChatExample(panelHeight);
            Debug.Log("OH"+originHeight);
            Debug.Log("TH"+totalHeight);
        }

    }
    Text CreateText(GameObject _panel, string text)
    {   
        GameObject textObj = new GameObject("Text");
        textObj.transform.SetParent(_panel.transform);

        Text textComponent = textObj.AddComponent<Text>();
        textComponent.text = text;
        textComponent.font = Resources.GetBuiltinResource<Font>("LegacyRuntime.ttf");
        textComponent.fontSize = 24;
        textComponent.color = Color.white;

        // 关键设置：启用自动换行
        textComponent.horizontalOverflow = HorizontalWrapMode.Wrap;
        textComponent.verticalOverflow = VerticalWrapMode.Truncate;
        textComponent.alignment = TextAnchor.UpperLeft; // 左上对齐

        // 设置Text的RectTransform
        RectTransform textRect = textObj.GetComponent<RectTransform>();

        textRect.anchorMin = Vector2.zero;
        textRect.anchorMax = Vector2.one;
        textRect.offsetMin = new Vector2(padding.x / 2, padding.y / 2); // 边距
        textRect.offsetMax = new Vector2(-padding.x / 2, -padding.y / 2);

        return textComponent;
    }

    void UpdatePanelSize(GameObject panel, Text textComponent)
    {
        // 强制立即刷新布局
        Canvas.ForceUpdateCanvases();

        // 获取文本实际需要的尺寸
        float preferredWidth = textComponent.preferredWidth;
        float preferredHeight = textComponent.preferredHeight;

        // 计算包含边距的面板尺寸
        float panelWidth = preferredWidth + padding.x;
        panelHeight = preferredHeight + padding.y;

        panelWidth = Mathf.Min(panelWidth, 300);

        // 设置Panel尺寸
        RectTransform panelRect = panel.GetComponent<RectTransform>();

        panelRect.sizeDelta = new Vector2(panelWidth, panelHeight);
    }

    public void MoveChatExample(float _panelHeight)
    {
        panelRT.offsetMax = panelRT.offsetMax + Vector2.up * _panelHeight;
    }
    public void MoveBubble(float _panelHeight)
    {
        foreach (Transform child in chatBubbleParent.transform)
        {
            if (child.CompareTag("Bubble"))
            {
                RectTransform rt = child.GetComponent<RectTransform>();
                rt.anchoredPosition += Vector2.up * _panelHeight;
            }
        }
    }
}
