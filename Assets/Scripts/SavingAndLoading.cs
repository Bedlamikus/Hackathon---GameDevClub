using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SavingAndLoading : MonoBehaviour
{
    [SerializeField] private bool settingsFromTable = true;

    private PlayerStats playerSettings;
    private const string DEFAULT_SETTINGS = "DefaultSettings";
    private const string CURRENT_SETTINGS = "CurrentSettings";
    private const string FIRST_START = "FirstStart";

    private void Start()
    {
        playerSettings = FindObjectOfType<PlayerStats>();
        GlobalEvents.DefaultSettingsLoaded.AddListener(SavingDataToPlayerPrefs);
        if (settingsFromTable) LoadSettingsFromGoogleTable();
        else LoadingDataFromPlayerPrefs();
    }

    private void SavingDataToPlayerPrefs(ExcelSettings settings)
    {
        string s = JsonUtility.ToJson(settings);
        PlayerPrefs.SetString(DEFAULT_SETTINGS, s);
        SaveCurrentSettings();
    }

    private void LoadingDataFromPlayerPrefs()
    {
        var settings = JsonUtility.FromJson<ExcelSettings>(PlayerPrefs.GetString(DEFAULT_SETTINGS));
        GlobalEvents.DefaultSettingsLoaded.Invoke(settings);
    }

    public void LoadSettingsFromGoogleTable()
    {
        PlayerPrefs.DeleteAll();
        PlayerPrefs.SetString(FIRST_START, FIRST_START);
        GlobalEvents.LoadSettings.Invoke();
    }

    private void SaveCurrentSettings()
    {
        var currentSettings = JsonUtility.ToJson(playerSettings);
        PlayerPrefs.SetString(CURRENT_SETTINGS, currentSettings);
    }

    private void LoadCurrentSettings()
    {
        JsonUtility.FromJsonOverwrite(PlayerPrefs.GetString(CURRENT_SETTINGS), playerSettings);
    }

}
