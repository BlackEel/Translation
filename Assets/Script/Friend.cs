using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Friend : MonoBehaviour
{
    private FriendList friendList;
    private Text inputTargetText => GameObject.FindGameObjectWithTag("InputTarget").GetComponent<Text>();
    void Start()
    {
        friendList = FindAnyObjectByType<FriendList>();
    }

    public void ChooseFriend()
    {
        // Deactivate all groups
        foreach (var group in friendList.groups)
        {
            group.alpha = 0;
            group.interactable = false;
            group.blocksRaycasts = false;
        }
        // Activate the selected group
        var selectedGroup = GetComponentInChildren<CanvasGroup>();
        selectedGroup.alpha = 1;
        selectedGroup.interactable = true;
        selectedGroup.blocksRaycasts = true;
        friendList.chosenFriend = gameObject.name[0];
    }
}
