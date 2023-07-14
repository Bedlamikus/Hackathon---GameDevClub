using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonEndBattle : MonoBehaviour
{
    private Button button;

    void Start()
    {
#if !UNITY_EDITOR
        gameObject.SetActive(false);
#endif
        button = GetComponent<Button>();
        button.onClick.AddListener(EndBattle);
    }

    private void EndBattle()
    {
        GlobalEvents.EndBattle.Invoke();
    }
}
