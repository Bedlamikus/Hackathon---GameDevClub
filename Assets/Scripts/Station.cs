using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Station : MonoBehaviour
{
    [SerializeField] float timeForEnabledStation = 2.0f;
    public Collider trigger;

    private void Start()
    {
        trigger = GetComponent<Collider>();
        trigger.enabled = false;
    }

    public void Enable()
    {
        StartCoroutine(EnabledTrigger());
    }

    private void OnTriggerEnter(Collider other)
    {
        var train = other.GetComponent<Train>();
        if (!train) return;
        GlobalEvents.StationEnter.Invoke();
        print("Station Enter invoke");
        trigger.enabled = false;
    }

    private IEnumerator EnabledTrigger()
    {
        yield return new WaitForSeconds(timeForEnabledStation);
        trigger.enabled = true;
        print("station enabled");
    }

    private void OnRewarded()
    {
        trigger.enabled = false;
    }
}
