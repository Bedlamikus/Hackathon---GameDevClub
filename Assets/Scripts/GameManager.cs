using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private MiniGame gamePrefab;
    [SerializeField] private GameObject screenFight;

    private MiniGame game;


    private void Start()
    {
        GlobalEvents.BattleTrainDie.AddListener(LoseBattle);
        GlobalEvents.StartBattle.AddListener(StartBattle);
        GlobalEvents.EndBattle.AddListener(EndBattle);
    }

    private void LoseBattle()
    {
        GlobalEvents.StressLose.Invoke();
    }

    private void StartBattle()
    {
        game = Instantiate(gamePrefab, screenFight.transform);
        game.Init();
    }

    private void EndBattle()
    {
        Destroy(game.gameObject);
    }
}
