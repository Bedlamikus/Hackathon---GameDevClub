using System;
using System.Collections;
using UnityEngine;

[RequireComponent(typeof(CVSLoader), typeof(SheetProcessor))]
public class GoogleSheetLoader : MonoBehaviour
{
    [SerializeField] private string _sheetIdCycleSettings;
    [SerializeField] private string _sheetIdPlayerSettings;
    [SerializeField] private string _sheetIdEnemySettings;

    [SerializeField] private ExcelSettings _data;
    
    private CVSLoader _cvsLoader;
    private SheetProcessor _sheetProcessor;

    private void Awake()
    {
        _cvsLoader = GetComponent<CVSLoader>();
        _sheetProcessor = GetComponent<SheetProcessor>();
        GlobalEvents.LoadSettings.AddListener(DownloadSettings);
        DontDestroyOnLoad(gameObject);
    }

    private void DownloadSettings()
    {
        StartCoroutine(DownloadTable());
    }

    private IEnumerator DownloadTable()
    {
        string[] rawCSVtext = new string[1];
        yield return _cvsLoader.DownloadRawCvsTable(_sheetIdCycleSettings, rawCSVtext);
        _data = _sheetProcessor.LoadCycleSettings(rawCSVtext[0]);
        yield return _cvsLoader.DownloadRawCvsTable(_sheetIdPlayerSettings, rawCSVtext);
        _data.playerSettings = _sheetProcessor.LoadPlayerSettings(rawCSVtext[0]);
        yield return _cvsLoader.DownloadRawCvsTable(_sheetIdEnemySettings, rawCSVtext);
        _data.enemiesSettings = _sheetProcessor.LoadEnemiesSettings(rawCSVtext[0]);
        GlobalEvents.DefaultSettingsLoaded.Invoke(_data);
    }

    public ExcelSettings GetSettings()
    {
        return _data;
    }
}
