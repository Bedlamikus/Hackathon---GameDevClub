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
    [SerializeField] private GameObject panelSettings;
    [SerializeField] private AdReward adReward;

    private void Start()
    {
        GlobalEvents.StressLose.AddListener(ShowPanelLose);
        GlobalEvents.Pause.AddListener(Pause);
        GlobalEvents.UnPause.AddListener(UnPause);
        GlobalEvents.StationEnter.AddListener(ShowPanelWin);
        GlobalEvents.StartBattle.AddListener(ShowPanelFight);
        GlobalEvents.EndBattle.AddListener(ShowPanelMagazine);
        GlobalEvents.EvRewarded.AddListener(SchowStationScreen);
    }

    public void ShowReward()
    {
        Instantiate(adReward, transform.parent);
    }

    private void ShowSettings()
    {
        panelSettings.SetActive(true);
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
        panelTapForContinuos.SetActive(true);
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
