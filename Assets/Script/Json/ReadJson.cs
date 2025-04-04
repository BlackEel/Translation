using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LitJson;
using System.Linq;


public class ReadJson : MonoBehaviour
{
    private List<Block> blocks;
    private Messages mes;

    [HideInInspector] public bool isContinue;

    private bool isChoice;
    private int chioceIndex;

    [HideInInspector] public string sceneId;

    public GameObject chatThreadPrefab;
    
    private TextScript textScript;

    void Start()
    {
        isContinue = false;
        isChoice = false;
        chioceIndex = 0;
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

    public void StartScene()
    {
        textScript = FindFirstObjectByType<TextScript>();

        string jsonFilePath = sceneId;
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

            // 随机选择
            if (message.type == "random")
            {
                yield return new WaitForSeconds(2f);
                int randomIndex = Random.Range(0, blocks.Count);
                StartCoroutine(GetBlock(randomIndex));
            }
            else if (message.type == "text" || message.type == "pic")
            {
                // 没有遇到选择
                if (message.text.Count == 1)
                {
                    textScript.CreateChatBubble(message.sender, message.text[0]);
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
                        chioceIndex = 0;
                    }
                }
            }
            else if (message.type == "end")
            {
                foreach (var text in message.text)
                {
                    ReadJson chatThread = Instantiate(chatThreadPrefab).GetComponent<ReadJson>();
                    chatThread.sceneId = text;
                    chatThread.StartScene();
                }
                Destroy(gameObject);
            }
        }

    }
}
