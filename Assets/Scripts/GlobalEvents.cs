using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public static class GlobalEvents
{
    public static UnityEvent Pause = new UnityEvent();
    public static UnityEvent UnPause = new UnityEvent();
    public static UnityEvent Win = new UnityEvent();
    public static UnityEvent Lose = new UnityEvent();
    public static UnityEvent Restart = new UnityEvent();
    public static UnityEvent StationEnter = new UnityEvent();
}
