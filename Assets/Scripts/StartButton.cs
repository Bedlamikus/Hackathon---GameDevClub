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
        GlobalEvents.StationEnter.AddListener(StationEnter);
    }

    private void Restart(int _)
    {
        StationEnter();
    }

    private void StationEnter()
    {
        newLoop = true;
        if (image) image.raycastTarget = true;
    }

    public void Tap()
    {
        if (!newLoop) return;
        image.raycastTarget = false;
        newLoop = false;
    }
}
