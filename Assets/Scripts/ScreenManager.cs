using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenManager : MonoBehaviour
{
    [SerializeField] private GameObject panelWin;
    [SerializeField] private GameObject panelLose;
    [SerializeField] private GameObject panelPause;
    [SerializeField] private GameObject panelTapForContinuos;
    [SerializeField] private GameObject panelFight;
    [SerializeField] private GameObject panelMagazine;

    private void Start()
    {
        GlobalEvents.StressWin.AddListener(ShowPanelWin);
        GlobalEvents.StressLose.AddListener(ShowPanelLose);
        GlobalEvents.Pause.AddListener(Pause);
        GlobalEvents.UnPause.AddListener(UnPause);
        GlobalEvents.StationEnter.AddListener(SchowStationScreen);
        GlobalEvents.StartBattle.AddListener(ShowPanelFight);
        GlobalEvents.EndBattle.AddListener(ShowPanelMagazine);
    }

    private void ShowPanelFight()
    {
        panelFight.SetActive(true);
        panelMagazine.SetActive(false);
    }

    private void ShowPanelMagazine()
    {
        panelFight.SetActive(false);
        panelMagazine.SetActive(true);
    }

    private void SchowStationScreen()
    {
        panelTapForContinuos.SetActive(true);
    }

    private void ShowPanelWin()
    {
        panelWin.SetActive(true);
    }

    private void ShowPanelLose()
    {
        panelLose.SetActive(true);
    }

    private void Pause()
    {
        panelPause.SetActive(true);
    }
    private void UnPause()
    {
        panelPause.SetActive(false);
    }

}
