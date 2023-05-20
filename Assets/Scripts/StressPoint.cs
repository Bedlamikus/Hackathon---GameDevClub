using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StressPoint : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (!other.GetComponent<Train>()) return;
        GlobalEvents.Pause.Invoke();

        //TO DO: заменить StressWin на вызов нового боя
        GlobalEvents.StressWin.Invoke();
    }
}
