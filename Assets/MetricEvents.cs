using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class MetricEvents : MonoBehaviour
{
    public MetricEvents Instance;

    /// <summary>
    /// Events
    /// </summary>

    private bool firstStartGame = true;
    private bool levelOneStart = false;
    private bool CycleEnded1 = false;
    private bool CycleEnded2 = false;
    private bool CycleEnded3 = false;
    private bool CycleEnded4 = false;
    private bool CycleEnded5 = false;
    private bool CycleEnded6 = false;
    private bool CycleEnded7 = false;
    private bool CycleEnded8 = false;
    private bool CycleEnded9 = false;
    private bool CycleEnded10 = false;

    private void Awake()
    {
        transform.SetParent(null);
        gameObject.name = "MetricEvents";

            if (Instance != null) Destroy(gameObject);
            else
            {
                Instance = this;
                DontDestroyOnLoad(gameObject);
            }
    }

    private void SavedEvents()
    {

    }
}
