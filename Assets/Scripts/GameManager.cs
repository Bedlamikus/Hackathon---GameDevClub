using Assets.Scripts.BattleScreen;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using YG;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameCycle gameCycle;
    [SerializeField] private GameObject cycles;
    [SerializeField] private Train train;
    [SerializeField] private Rails rails;
    [SerializeField] private Station station;
    [SerializeField] private Door exitDoor;
    [SerializeField] private Door secondDoor;

    public int currentCycle = 0;
    private ExcelSettings _data;
    private bool resetCyclesIfStartAgain = true;

    private void Start()
    {
        rails.Init();
        train.Init();
        GlobalEvents.BattleTrainDie.AddListener(LoseBattle);
        GlobalEvents.EvRewardedLevelRestart.AddListener(RestartCurrentLevel);
        GlobalEvents.RestartGame.AddListener(Restart);
        var YG = YandexGame.Instance;
        DataInit(YG.savesData()._defaultData);
        var c = FindObjectOfType<PlayerStats>().CurrentCycle;
        LoadCycleByNum(c);
    }

    public void RestartCurrentLevel()
    {
        var c = FindObjectOfType<PlayerStats>().CurrentCycle;
        print("GameMAnager: Restart currentCycle = " + c.ToString());
        LoadCycleByNum(currentCycle);
        station.Disable();
        rails.Init();
        train.ResetPosition();
    }

    public void Restart()
    {
        GlobalEvents.LoadDefaultSettings.Invoke();
        SceneManager.LoadScene(0);
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
    public void BuyAttckSpeed()
    {
        GlobalEvents.BuyAttackSpeed.Invoke();
    }
    public void BuyDamage()
    {
        GlobalEvents.BuyDamage.Invoke();
    }
    public void BuyArmor()
    {
        GlobalEvents.BuyArmor.Invoke();
    }

    private void DataInit(ExcelSettings data)
    {
        _data = data;
    }
    
    public void LoadCycleByNum(int num)
    {
        ClearCyclesInChild();
        if (num > 9 && resetCyclesIfStartAgain != true)
        {
            currentCycle = num;
            rails.SetGoAwayPoints();
            exitDoor.Open();
            secondDoor.Close();
            var data = YandexGame.Instance.savesData();
            data.levels[data.currentLevel].ended = true;
            GlobalEvents.ChangeCycleIndex.Invoke(currentCycle);
            GlobalEvents.SaveCurrentSettings.Invoke();
            return;
        }
        if (num > 9 && resetCyclesIfStartAgain)
        {
            resetCyclesIfStartAgain = false;
            num = 0;
        }
        var cycle = Instantiate(gameCycle, cycles.transform);
        cycle.Init(_data.cycleSettings[num], _data.enemiesSettings);
        currentCycle = num;
        GlobalEvents.ChangeCycleIndex.Invoke(currentCycle);
    }

    public void LoadNextCycle()
    {
        resetCyclesIfStartAgain = false;
        MetricEvents.Instance.EndCycle(currentCycle);
        LoadCycleByNum(currentCycle + 1);
        GlobalEvents.SaveCurrentSettings.Invoke();
    }

    private void ClearCyclesInChild()
    {
        var cycles = this.cycles.GetComponentsInChildren<GameCycle>();
        foreach (var cycle in cycles)
        {
            Destroy(cycle.gameObject);
        }
    }

    public void StartLoop()
    {
        if (train.IsRide())
        {
            ContinueLoop();
            return;
        }
        train.StartRide();
    }

    private void ContinueLoop()
    {
        train.UnPause();
        station.Enable();
    }

    public void LoopEnd()
    {
        GlobalEvents.StationEnter.Invoke();
    }
}
