using System;
using System.Collections;
using UnityEngine;

[RequireComponent(typeof(CVSLoader), typeof(SheetProcessor))]
public class GoogleSheetLoader : MonoBehaviour
{
    [SerializeField] private string _sheetIdCycleSettings;
    [SerializeField] private string _sheetIdPlayerSettings;

    [SerializeField] private ExcelSettings _data;
    
    private CVSLoader _cvsLoader;
    private SheetProcessor _sheetProcessor;

    private void Start()
    {
        _cvsLoader = GetComponent<CVSLoader>();
        _sheetProcessor = GetComponent<SheetProcessor>();
        StartCoroutine(DownloadTable());
    }

    private IEnumerator DownloadTable()
    {
        string[] rawCSVtext = new string[1];
        yield return _cvsLoader.DownloadRawCvsTable(_sheetIdCycleSettings, rawCSVtext);
        _data = _sheetProcessor.LoadCycleSettings(rawCSVtext[0]);
        yield return _cvsLoader.DownloadRawCvsTable(_sheetIdPlayerSettings, rawCSVtext);
        _data.playerSettings = _sheetProcessor.LoadPlayerSettings(rawCSVtext[0]);
        GlobalEvents.SettingsLoaded.Invoke(_data);
    }
}
