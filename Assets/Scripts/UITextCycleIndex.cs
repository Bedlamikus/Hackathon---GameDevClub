using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UITextCycleIndex : MonoBehaviour
{
    [SerializeField] private TMP_Text text;
    [SerializeField] private string beforeText;
    [SerializeField] private string afterText;

    private void Start()
    {
        GlobalEvents.ChangeCycleIndex.AddListener(ChangeText);
    }

    private void ChangeText(int index)
    {
        text.text = beforeText + (index + 1).ToString() + afterText;
    }
}
