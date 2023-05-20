using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Station : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        GlobalEvents.StationEnter.Invoke();
    }
}
