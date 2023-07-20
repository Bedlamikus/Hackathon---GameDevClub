using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StartLevelButton : MonoBehaviour
{
    private Button startButton;

    private void Start()
    {
        startButton = GetComponent<Button>();
        startButton.onClick.AddListener(ButtonClick);
    }

    private void ButtonClick()
    {
        GlobalEvents.StartLevelButton.Invoke();
    }
}
