using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LitJson;
using System.Linq;
using UnityEditor.VersionControl;


public class ReadJson : MonoBehaviour
{
    private List<Block> blocks;
    private Messages mes;
    public bool isContinue;

    private bool isChoice = false;
    private int chioceIndex = 0;

    void Start()
    {
        isContinue = false;

        StartScene("100");
    }

    private void Update()
    {
        if (isChoice && Input.GetKeyDown(KeyCode.Backspace))
        {
            chioceIndex = (chioceIndex + 1) % mes.choice.Count;
            Debug.Log("Sender: " + mes.sender);
            Debug.Log("Text: " + mes.text[chioceIndex]);
        }
        if (!isContinue && Input.GetKeyDown(KeyCode.Return))
        {
            isContinue = true;
        }
    }

    private void StartScene(string sceneId)
    {
        string jsonFilePath = sceneId;    // 不带扩展名
        TextAsset jsonTextAsset = Resources.Load<TextAsset>(jsonFilePath);
        string jsonFileContent = jsonTextAsset.text;

        blocks = JsonMapper.ToObject<List<Block>>(jsonFileContent);
        StartCoroutine(GetBlock(0));
    }

    private IEnumerator GetBlock(int targetId)
    {
        Block block = blocks.FirstOrDefault(b => b.id == targetId);
        foreach (var message in block.messages)
        {
            mes = message;

            // 没有遇到选择
            if (message.text.Count == 1)
            {
                Debug.Log("Sender: " + message.sender);
                Debug.Log("Text: " + string.Join(", ", message.text));
                yield return new WaitForSeconds(1f);
            }
            // 遇到选择
            else
            {
                isChoice = true;
                Debug.Log("Sender: " + message.sender);
                Debug.Log("Text: " + message.text[0]);
                yield return new WaitUntil(() => isContinue);
                isContinue = false;
                if (message.type == "text" && message.choice.Count > 0)
                {
                    StartCoroutine(GetBlock(int.Parse(message.choice[chioceIndex])));
                }
            }
        }

    }
}
