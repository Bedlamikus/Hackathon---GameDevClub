using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using YG;

public class MetricEvents : MonoBehaviour
{
    public static MetricEvents Instance;

    public const string M_CoinsMultiplyReward = "302475577";
    public const string M_ApplyAttakSpeed = "302475371";
    public const string M_ApplyAttakDamage = "301636022";

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

    public void ApplyDamage()
    {
        YandexMetrica.Send(M_ApplyAttakDamage);
    }

    public void ApplySpeedAttack()
    {
        YandexMetrica.Send(M_ApplyAttakSpeed);
    }

    public void RewardCoins()
    {
        YandexMetrica.Send(M_CoinsMultiplyReward);
    }
}
