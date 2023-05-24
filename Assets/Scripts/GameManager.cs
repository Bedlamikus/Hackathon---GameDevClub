using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] private MiniGame gamePrefab;
    [SerializeField] private GameObject screenFight;
    [SerializeField] private GameCycle gameCyclePrefab;

    private GameCycle gameCycle = null;
    private MiniGame game = null;


    private void Start()
    {
        gameCycle = Instantiate(gameCyclePrefab, transform.position, Quaternion.identity);
        GlobalEvents.BattleTrainDie.AddListener(LoseBattle);
        GlobalEvents.StartBattle.AddListener(StartBattle);
        GlobalEvents.EndBattle.AddListener(EndBattle);
        gameCycle.Init(0);
    }

    public void Restart()
    {
        SceneManager.LoadScene(0);

        if (gameCycle) Destroy(gameCycle.gameObject);
        EndBattle();
        gameCycle = Instantiate(gameCyclePrefab, transform.position, Quaternion.identity);
        gameCycle.Init(0);
        GlobalEvents.Restart.Invoke(0);
    }

    private void LoseBattle()
    {
        GlobalEvents.StressLose.Invoke();
        GlobalEvents.EndBattle.Invoke();
        EndBattle();
    }

    private void StartBattle(int enemyCount)
    {
        if (game != null) return;
        game = Instantiate(gamePrefab, screenFight.transform);
        game.Init(enemyCount);
    }

    private void EndBattle()
    {
        if (game) Destroy(game.gameObject);
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
