using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LitJson;
using System.Linq;


public class ReadJson : MonoBehaviour
{
    private List<Block> blocks;
    public string mesText;

    void Start()
    {
        string jsonFilePath = "100";    // 不带扩展名
        TextAsset jsonTextAsset = Resources.Load<TextAsset>(jsonFilePath);
        string jsonFileContent = jsonTextAsset.text;

        blocks = JsonMapper.ToObject<List<Block>>(jsonFileContent);
        StartCoroutine(GetBlock(0));
    }

    private void Update()
    {
    }

    private IEnumerator GetBlock(int targetId)
    {
        Block block = blocks.FirstOrDefault(b => b.id == targetId);
        foreach (var message in block.messages)
        {
            mesText = string.Join(", ", message.text);
            Debug.Log("Sender: " + message.sender);
            //Debug.Log("Type: " + mes.type);
            Debug.Log("Text: " + string.Join(", ", message.text));
            //Debug.Log("Choice: " + string.Join(", ", mes.choice));
            if (message.text.Count == 1)
            {
                yield return new WaitForSeconds(1f);
            }
            else
            {
                yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Space));
                if (message.type == "text" && message.choice.Count > 0)
                {
                    StartCoroutine(GetBlock(int.Parse(message.choice[0])));
                }
            }
        }

    }
}
