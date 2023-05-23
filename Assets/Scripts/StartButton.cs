using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StartButton : MonoBehaviour
{
    private bool newLoop = true;
    private Image image = null;
    private void Start()
    {
        image = GetComponent<Image>();
        GlobalEvents.Restart.AddListener(Restart);
        GlobalEvents.StationEnter.AddListener(Restart);
    }

    private void Restart()
    {
        newLoop = true;
        if (image) image.raycastTarget = true;
    }

    public void Tap()
    {
        if (!newLoop) return;
        GlobalEvents.UnPause.Invoke();
        GlobalEvents.NewLoop.Invoke();
        image.raycastTarget = false;
        newLoop = false;
    }
}
