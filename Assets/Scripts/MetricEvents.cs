using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using YG;

public class MetricEvents : MonoBehaviour
{
    public static MetricEvents Instance;

    public const string M_CoinsMultiplyReward = "302475577";
    public const string M_RestartBeforeDieReward = "302699479";
    public const string M_ApplyAttakSpeed = "302475371";
    public const string M_ApplyAttakDamage = "301636022";
    public const string M_FightAttackDamage = "attackdamage";
    public const string M_FightAttackSpeed = "speedattack";
    public const string M_FightHealth = "health";

    public const string M_EndCycle1 = "endCycle1";
    public const string M_EndCycle2 = "endCycle2";
    public const string M_EndCycle3 = "endCycle3";
    public const string M_EndCycle4 = "endCycle4";
    public const string M_EndCycle5 = "endCycle5";
    public const string M_EndCycle6 = "endCycle6";
    public const string M_EndCycle7 = "endCycle7";
    public const string M_EndCycle8 = "endCycle8";
    public const string M_EndCycle9 = "endCycle9";
    public const string M_EndCycle10 = "endCycle10";

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

    public void EndCycle(int num)
    {

        switch (num)
        {
            case 0:
                YandexMetrica.Send(M_EndCycle1);
                break;
            case 1:
                YandexMetrica.Send(M_EndCycle2);
                break;
            case 2:
                YandexMetrica.Send(M_EndCycle3);
                break;
            case 3:
                YandexMetrica.Send(M_EndCycle4);
                break;
            case 4:
                YandexMetrica.Send(M_EndCycle5);
                break;
            case 5:
                YandexMetrica.Send(M_EndCycle6);
                break;
            case 6:
                YandexMetrica.Send(M_EndCycle7);
                break;
            case 7:
                YandexMetrica.Send(M_EndCycle8);
                break;
            case 8:
                YandexMetrica.Send(M_EndCycle9);
                break;
            case 9:
                YandexMetrica.Send(M_EndCycle10);
                break;
        }

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
    public void RewardBeforePlayerDie()
    {
        YandexMetrica.Send(M_RestartBeforeDieReward);
    }
    public void FightAttackSpeed()
    {
        YandexMetrica.Send(M_FightAttackSpeed);
    }
    public void FightAttackDamage()
    {
        YandexMetrica.Send(M_ApplyAttakDamage);
    }
    public void FightHealth()
    {
        YandexMetrica.Send(M_FightHealth);
    }
}
