using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameCycle gameCycle;

    private int currentCycle = 0;
    private ExcelSettings _data;

    private void Start()
    {
        GlobalEvents.BattleTrainDie.AddListener(LoseBattle);
        GlobalEvents.DefaultSettingsLoaded.AddListener(DataInit);
    }

    public void Restart()
    {
        SceneManager.LoadScene(0);
        GlobalEvents.Restart.Invoke(0);
    }

    private void LoseBattle()
    {
        GlobalEvents.StressLose.Invoke();
        GlobalEvents.EndBattle.Invoke();
    }

    public void Pause()
    {
        GlobalEvents.Pause.Invoke();
    }

    public void UnPause()
    {
        GlobalEvents.UnPause.Invoke();
    }
    public void BuyHealth() 
    {
        GlobalEvents.BuyHealth.Invoke();
    }

    public void BuyAttck()
    {
        GlobalEvents.BuyAttack.Invoke();
    }

    private void DataInit(ExcelSettings data)
    {
        _data = data;
        LoadCycleByNum(0);
    }
    
    public void LoadCycleByNum(int num)
    {
        ClearCyclesInChild();
        if (num > 9) return;
        var cycle = Instantiate(gameCycle, transform);
        cycle.Init(_data.cycleSettings[num]);
        currentCycle = num;
        GlobalEvents.ChangeCycleIndex.Invoke(currentCycle);
    }

    public void LoadNextCycle()
    {
        LoadCycleByNum(currentCycle + 1);
        GlobalEvents.SaveCurrentSettings.Invoke();
    }

    private void ClearCyclesInChild()
    {
        var cycles = GetComponentsInChildren<GameCycle>();
        foreach (var cycle in cycles)
        {
            Destroy(cycle.gameObject);
        }
    }
}
