using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private MiniGame gamePrefab;
    [SerializeField] private GameObject screenFight;
    [SerializeField] private GameCycle gameCycle;

    private MiniGame game;


    private void Start()
    {
        GlobalEvents.BattleTrainDie.AddListener(LoseBattle);
        GlobalEvents.StartBattle.AddListener(StartBattle);
        GlobalEvents.EndBattle.AddListener(EndBattle);
        gameCycle.Init();
    }

    private void LoseBattle()
    {
        GlobalEvents.StressLose.Invoke();
    }

    private void StartBattle(int enemyCount)
    {
        game = Instantiate(gamePrefab, screenFight.transform);
        game.Init(enemyCount);
    }

    private void EndBattle()
    {
        Destroy(game.gameObject);
    }
}
