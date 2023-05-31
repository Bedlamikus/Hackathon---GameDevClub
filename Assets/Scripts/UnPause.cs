using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnPause : MonoBehaviour
{
    public void UnPaused()
    {
        GlobalEvents.TrainGo.Invoke();
        GlobalEvents.UnPause.Invoke();
    }
}
