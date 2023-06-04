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
        GlobalEvents.EndBattle.AddListener(SaveCurrentSettings);
        GlobalEvents.SaveCurrentSettings.AddListener(SaveCurrentSettings);
        GlobalEvents.DefaultSettingsLoaded.AddListener(SavingDefaultSettingsToPlayerPrefs);

        if (settingsFromTable)
        {
            print("Loading from internet");
            LoadSettingsFromGoogleTable();
            return;
        }
        LoadCurrentSettings();
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
        GlobalEvents.LoadSettings.Invoke();
    }

    private void SaveCurrentSettings()
    {
        var currentSettings = JsonUtility.ToJson(playerSettings.GetCurrentSettings());
        print("Saving");
        print(currentSettings);
        PlayerPrefs.SetString(CURRENT_SETTINGS, currentSettings);
    }

    private void LoadCurrentSettings()
    {
        LoadingDefaultSettingsFromPlayerPrefs();
        StartCoroutine(LoadCurrentSettingsCoroutine());
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
