using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using YG;

public class SavingAndLoading : MonoBehaviour
{
    [SerializeField] private bool settingsFromTable = false;

    private PlayerStats playerSettings;
    private const string DEFAULT_SETTINGS = "DefaultSettings";
    private const string CURRENT_SETTINGS = "CurrentSettings";

    private void Start()
    {
        playerSettings = FindObjectOfType<PlayerStats>();
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
        YandexGame.Instance._SaveProgress();
    }
}
