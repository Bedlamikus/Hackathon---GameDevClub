using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private void Start()
    {
        GlobalEvents.BattleTrainDie.AddListener(LoseBattle);
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
}
