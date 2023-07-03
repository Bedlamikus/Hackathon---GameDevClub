using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using YG;

public class SavingAndLoading : MonoBehaviour
{
    [SerializeField] private bool settingsFromTable = false;

    private PlayerStats playerStats;
    private const string DEFAULT_SETTINGS = "DefaultSettings";
    private const string CURRENT_SETTINGS = "CurrentSettings";

    private void Start()
    {
        GlobalEvents.EndBattle.AddListener(SaveCurrentSettings);
        GlobalEvents.SaveCurrentSettings.AddListener(SaveCurrentSettings);

        if (settingsFromTable)
        {
            print("Loading from internet");
            GlobalEvents.LoadSettingsFromInternet.Invoke();
            return;
        }
        GlobalEvents.LoadDefaultSettings.Invoke();
    }

    private void SaveCurrentSettings()
    {
        YandexGame.Instance.UpdateSavesData(PlayerStats.GetCurrentSettings());
        YandexGame.Instance._SaveProgress();
    }

    private PlayerStats PlayerStats 
    { 
        get 
        { 
            if (playerStats == null) 
            {
                playerStats = FindObjectOfType<PlayerStats>();
            }
            return playerStats;
        }
    }
}
