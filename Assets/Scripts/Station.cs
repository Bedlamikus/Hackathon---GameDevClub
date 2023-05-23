using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Station : MonoBehaviour
{
    [SerializeField] float timeForEnabledStation = 2.0f;
    private Collider trigger;

    private void Start()
    {
        GlobalEvents.NewLoop.AddListener(Enabled);
        trigger = GetComponent<Collider>();
        trigger.enabled = false;
    }

    private void Enabled()
    {
        StartCoroutine(EnabledTrigger());
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.GetComponent<Train>()) return;
        GlobalEvents.StationEnter.Invoke();
        GlobalEvents.TrainStop.Invoke();
        trigger.enabled = false;
    }

    private IEnumerator EnabledTrigger()
    {
        yield return new WaitForSeconds(timeForEnabledStation);
        trigger.enabled = true;
    }
}
