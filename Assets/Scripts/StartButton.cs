using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartButton : MonoBehaviour
{
    private bool newGame = true;

    private void Start()
    {
        GlobalEvents.Restart.AddListener(NewGame);
    }

    private void NewGame()
    {
        newGame = true;
    }

    private void OnMouseDown()
    {
        if (!newGame) return;
        GlobalEvents.UnPause.Invoke();
        newGame = false;
    }
}
