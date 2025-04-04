using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InputScript : MonoBehaviour
{
    public GameObject choicePrefab;
    public GameObject choiceParent;

    private List<GameObject> choiceList = new List<GameObject>();

    public void CreateChoice(List<string> texts)
    {
        foreach (string text in texts)
        {
            GameObject choice = Instantiate(choicePrefab, choiceParent.transform);
            choiceList.Add(choice);

            choice.GetComponentInChildren<Image>().color = Vector4.zero;

            RectTransform rt = choice.GetComponent<RectTransform>();
            rt.anchoredPosition = new Vector2(0, 60 - (choiceList.Count - 1) * 30); // Adjust the position based on the number of choices
            choice.GetComponentInChildren<Text>().text = "<color=#FFFFFF>" + text + "</color>";
        }
        SelectChoice(0);
    }

    public void SelectChoice(int idx)
    {
        int preIdx = (choiceList.Count + idx - 1) % choiceList.Count;
        // 取消高亮
        choiceList[preIdx].GetComponentInChildren<Image>().color = Vector4.zero;
        // 高亮
        choiceList[idx].GetComponentInChildren<Image>().color = Color.white;
    }

    public void ChangeChoiceColor(string originText, int idx, int count)
    {
        string text = "<color=#000000>";
        for (int i = 0; i < count; i++)
        {
            text += originText[i];
        }
        text += "</color><color=#FFFFFF>";
        for (int i = count; i < originText.Length; i++)
        {
            text += originText[i];
        }
        text += "</color>";
        choiceList[idx].GetComponentInChildren<Text>().text = "<color=#FFFFFF>" + text + "</color>";
    }

    public void ClearChoice()
    {
        choiceList.Clear();
        foreach (Transform child in choiceParent.transform)
        {
            Destroy(child.gameObject);
        }
    }
}
