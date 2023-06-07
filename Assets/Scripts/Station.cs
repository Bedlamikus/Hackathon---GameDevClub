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
        GlobalEvents.UnPause.AddListener(Enabled);
        //GlobalEvents.EvRewarded.AddListener(OnRewarded);

        trigger = GetComponent<Collider>();
        trigger.enabled = false;
    }

    public void Enabled()
    {
        StartCoroutine(EnabledTrigger());
    }

    private void OnTriggerEnter(Collider other)
    {
        var train = other.GetComponent<Train>();
        if (!train) return;
        train.Pause();
        GlobalEvents.StationEnter.Invoke();
        trigger.enabled = false;
    }

    private IEnumerator EnabledTrigger()
    {
        yield return new WaitForSeconds(timeForEnabledStation);
        trigger.enabled = true;
    }

    public void OnRewarded()
    {
        trigger.enabled = false;
    }
}
