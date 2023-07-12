using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonEndBattle : MonoBehaviour
{
    private Button button;

    void Start()
    {
        button = GetComponent<Button>();
        button.onClick.AddListener(EndBattle);
    }

    private void EndBattle()
    {
        GlobalEvents.EndBattle.Invoke();
    }
}
