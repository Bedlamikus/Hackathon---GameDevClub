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
        GlobalEvents.DefaultSettingsLoaded.AddListener(SavingDefaultSettingsToPlayerPrefs);
        GlobalEvents.SaveCurrentSettings.AddListener(SaveCurrentSettings);


        if (settingsFromTable)
        {
            LoadSettingsFromGoogleTable();
            return;
        }
        if (PlayerPrefs.HasKey(CURRENT_SETTINGS))
        {
            LoadCurrentSettings();
            return;
        }
        LoadingDefaultSettingsFromPlayerPrefs();
    }

    private void SavingDefaultSettingsToPlayerPrefs(ExcelSettings settings)
    {
        string s = JsonUtility.ToJson(settings);
        PlayerPrefs.SetString(DEFAULT_SETTINGS, s);
    }

    private void LoadingDefaultSettingsFromPlayerPrefs()
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
