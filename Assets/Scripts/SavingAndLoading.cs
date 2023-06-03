using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SavingAndLoading : MonoBehaviour
{
    [SerializeField] private bool settingsFromTable = true;
    
    private void Start()
    {
        GlobalEvents.SettingsLoaded.AddListener(SavingDataToPlayerPrefs);
        GlobalEvents.SettingsLoaded.AddListener(UpdateLoadedSettings);
        if (settingsFromTable) LoadSettingsFromGoogleTable();
        else LoadingDataFromPlayerPrefs();
    }

    private void SavingDataToPlayerPrefs(ExcelSettings settings)
    {
        string s = JsonUtility.ToJson(settings);
        PlayerPrefs.SetString("DefaultSettings", s);
    }

    private void LoadingDataFromPlayerPrefs()
    {
        var settings = JsonUtility.FromJson<ExcelSettings>("DefaultSettings");
        GlobalEvents.SettingsLoaded.Invoke(settings);
    }

    private void LoadSettingsFromGoogleTable()
    {
        GlobalEvents.LoadSettings.Invoke();
    }

    private void UpdateLoadedSettings(ExcelSettings settings)
    {
        
    }
}
