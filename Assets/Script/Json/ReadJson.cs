using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LitJson;
using System.Linq;
using UnityEngine.UI;


public class ReadJson : MonoBehaviour
{
    private const string LETTERS = "abcdefghijklmnopqrstuvwxyz";

    private List<Block> blocks;
    private Messages mes;

    [HideInInspector] public bool isContinue = false;

    private bool isChoice = false;
    private int chioceIndex = 0;

    [HideInInspector] public string sceneId;

    public GameObject chatThreadPrefab;
    
    private TextScript textScript;
    private InputScript inputScript;

    // 记录选择的文本个数
    private List<int> changeCount = new List<int>();
    private List<string> inputTarget = new List<string>();
    private Text inputTargetText => GameObject.FindGameObjectWithTag("InputTarget").GetComponent<Text>();

    private FriendList friendList => FindAnyObjectByType<FriendList>();
    private char talkerId;

    void Start()
    {
    }

    private void Update()
    {
        if (talkerId == friendList.chosenFriend)
        {
            if (inputTarget.Count > 0)
                inputTargetText.text = inputTarget[chioceIndex];

            if (isChoice && Input.GetKeyDown(KeyCode.Backspace))
            {
                chioceIndex = (chioceIndex + 1) % mes.choice.Count;
                inputScript.SelectChoice(chioceIndex);
                //inputTargetText.text = inputTarget[chioceIndex];
            }
            //else if (!isContinue && Input.GetKeyDown(KeyCode.Return) && changeCount[chioceIndex] >= mes.text[chioceIndex].Length)
            else if (!isContinue && Input.GetKeyDown(KeyCode.Return) && changeCount[chioceIndex] >= 1)
            {
                isChoice = false;
                isContinue = true;
                changeCount.Clear();
                inputTarget.Clear();
            }
            else if (inputTarget.Count > 0 && inputTarget[chioceIndex].Length > 0 && Input.GetKeyDown(KeyCode.A + inputTarget[chioceIndex][0] - 'a'))
            {
                inputTarget[chioceIndex] = inputTarget[chioceIndex].Substring(1);
                //inputTargetText.text = inputTarget[chioceIndex];
                changeCount[chioceIndex]++;
                if (changeCount[chioceIndex] <= mes.text[chioceIndex].Length)
                    inputScript.ChangeChoiceColor(mes.text[chioceIndex], chioceIndex, changeCount[chioceIndex]);
            }
        }
    }

    public void StartScene()
    {
        talkerId = sceneId[1];
        GameObject talker = GameObject.Find(talkerId.ToString());
        textScript = talker.GetComponentInChildren<TextScript>();
        inputScript = talker.GetComponentInChildren<InputScript>();
        inputScript.ClearChoice();

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

            if (message.type == "end")
            {
                if (message.text[0] == "bad")
                {
                    Debug.Log("BAD!!!");
                }
                else if (message.text[0] == "done")
                {
                    Debug.Log("DONE!!!");
                }
                else
                {
                    foreach (var text in message.text)
                    {
                        ReadJson chatThread = Instantiate(chatThreadPrefab).GetComponent<ReadJson>();
                        chatThread.sceneId = text;
                        chatThread.StartScene();
                    }
                }
                Destroy(gameObject);
            }
            // 收到消息
            else if (message.sender == 0)
            {
                // 随机选择
                if (message.type == "random")
                {
                    yield return new WaitForSeconds(.2f);
                    int randomIndex = Random.Range(0, blocks.Count);
                    StartCoroutine(GetBlock(randomIndex));
                }
                else if (message.type == "text" || message.type == "pic")
                {
                    yield return new WaitForSeconds(.1f);
                    textScript.CreateChatBubble(message.sender, message.text[0]);
                }
            }
            // 发送消息
            else if (message.sender == 1)
            {
                if (message.type == "text" || message.type == "pic")
                {
                    // 没有遇到选择
                    if (message.text.Count == 1)
                    {
                        inputScript.CreateChoice(message.text);
                        GenerateInputTarget(message.text[0].Length);
                        changeCount.Add(0);
                        yield return new WaitUntil(() => isContinue);
                        inputScript.ClearChoice();
                        // 发送
                        textScript.CreateChatBubble(message.sender, message.text[0]);
                        isContinue = false;
                    }
                    // 遇到选择
                    else
                    {
                        isChoice = true;
                        inputScript.CreateChoice(message.text);
                        foreach (var text in message.text)
                        {
                            GenerateInputTarget(text.Length);
                            changeCount.Add(0);
                        }
                        yield return new WaitUntil(() => isContinue);
                        inputScript.ClearChoice();
                        // 发送
                        textScript.CreateChatBubble(message.sender, message.text[chioceIndex]);
                        isContinue = false;
                        if (message.choice.Count > 0)
                        {
                            StartCoroutine(GetBlock(int.Parse(message.choice[chioceIndex])));
                            chioceIndex = 0;
                        }
                    }
                }
            }
        }

    }

    private void GenerateInputTarget(int length)
    {
        string text = "";
        for (int i = 0; i < 1; i++)
        {
            text += LETTERS[Random.Range(0, LETTERS.Length)];
        }
        inputTarget.Add(text);
    }
}
