using System;
using UnityEngine;

[RequireComponent(typeof(CVSLoader), typeof(SheetProcessor))]
public class GoogleSheetLoader : MonoBehaviour
{
    public event Action<ExcelSettings> OnProcessData;
    
    [SerializeField] private string _sheetId;
    [SerializeField] private ExcelSettings _data;
    
    private CVSLoader _cvsLoader;
    private SheetProcessor _sheetProcessor;

    private void Start()
    {
        _cvsLoader = GetComponent<CVSLoader>();
        _sheetProcessor = GetComponent<SheetProcessor>();
        DownloadTable();
    }

    private void DownloadTable()
    {
        _cvsLoader.DownloadTable(_sheetId, OnRawCVSLoaded);
    }

    private void OnRawCVSLoaded(string rawCVSText)
    {
        _data = _sheetProcessor.ProcessData(rawCVSText);
        OnProcessData?.Invoke(_data);
    }
}
