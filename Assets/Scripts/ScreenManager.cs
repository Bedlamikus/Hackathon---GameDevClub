using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenManager : MonoBehaviour
{
    [SerializeField] private GameObject panelWin;
    [SerializeField] private GameObject panelLose;

    private void Start()
    {
        GlobalEvents.StressWin.AddListener(ShowPanelWin);
        GlobalEvents.StressLose.AddListener(ShowPanelLose);
    }

    private void ShowPanelWin()
    {
        panelWin.SetActive(true);
    }

    private void ShowPanelLose()
    {
        panelLose.SetActive(true);
    }
}
