using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using YG;

[System.Serializable]
public class MetricData
{
    public const string M_CoinsMultiplyReward = "";
}

public class MetricEvents : MonoBehaviour
{
    public static MetricEvents Instance;

    public MetricData _metricData;

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

    public void FirstStartGame()
    {
        YandexMetrica.Send("301523903");
    }

}
