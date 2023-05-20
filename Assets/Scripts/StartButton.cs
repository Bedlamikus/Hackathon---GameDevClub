using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartButton : MonoBehaviour
{
    private bool newLoop = true;

    private void Start()
    {
        GlobalEvents.Restart.AddListener(Restart);
        GlobalEvents.StationEnter.AddListener(Restart);
    }

    private void Restart()
    {
        newLoop = true;
    }

    private void OnMouseDown()
    {
        if (!newLoop) return;
        GlobalEvents.UnPause.Invoke();
        GlobalEvents.NewLoop.Invoke();
        newLoop = false;
    }
}
