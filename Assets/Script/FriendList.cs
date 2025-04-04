using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FriendList : MonoBehaviour
{
    public CanvasGroup[] groups { get; private set; }
    public char chosenFriend;

    private void Start()
    {
        groups = GetComponentsInChildren<CanvasGroup>();
        chosenFriend = '0';
    }
}
