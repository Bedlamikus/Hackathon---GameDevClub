using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CVSLoader), typeof(SheetProcessor), typeof(GoogleSheetLoader))]
public class SavingAndLoading : MonoBehaviour
{
    private void Start()
    {
        GlobalEvents.SettingsLoaded.AddListener(SavingData);
    }

    private void SavingData(ExcelSettings settings)
    {
        string s = JsonUtility.ToJson(settings);
    }

    private void LoadSettingsFromGoogleTable()
    {
        GlobalEvents.LoadSettings.Invoke();
        GlobalEvents.SettingsLoaded.AddListener(UpdateLoadedSettings);
    }

    private void UpdateLoadedSettings(ExcelSettings settings)
    {
        
    }
}
