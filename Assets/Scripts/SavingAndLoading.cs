using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
        var currentSettings = JsonUtility.ToJson(playerSettings.GetCurrentSettings());
        PlayerPrefs.SetString(CURRENT_SETTINGS, currentSettings);
    }

    private IEnumerator LoadCurrentSettingsCoroutine()
    {
        yield return null;// new WaitForSeconds(1);
        var currentSettings = PlayerPrefs.GetString(CURRENT_SETTINGS);
        if (currentSettings != "")
        {
            //print(currentSettings);
            var settings = JsonUtility.FromJson<PlayerStatsData>(currentSettings);
            playerSettings.SetCurrentSettings(settings);
            var gm = FindObjectOfType<GameManager>();
            gm.LoadCycleByNum(settings.currentLevel);
        }

    }
}
