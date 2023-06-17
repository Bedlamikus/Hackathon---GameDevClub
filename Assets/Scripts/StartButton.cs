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
    }

    public void Tap()
    {
        if (!newLoop) return;
        image.raycastTarget = false;
        newLoop = false;
    }
}
