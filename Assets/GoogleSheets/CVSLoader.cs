using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Networking;

public class CVSLoader : MonoBehaviour
{
    private readonly bool _debug = false;

    private const string urlCycleSettings = "https://docs.google.com/spreadsheets/d/1xXmIVZmuqfSgPuG9owL2IANJ4sdlnJCjnBRluQLCF1E/export?format=csv&gid=*";

    public IEnumerator DownloadRawCvsTable(string gID, string[] result)
    {
        string actualUrl = urlCycleSettings.Replace("*", gID);
        using (UnityWebRequest request = UnityWebRequest.Get(actualUrl))
        {
            yield return request.SendWebRequest();
            if (request.result == UnityWebRequest.Result.ConnectionError || request.result == UnityWebRequest.Result.ProtocolError ||
                request.result == UnityWebRequest.Result.DataProcessingError)
            {
                Debug.LogError(request.error);
            }
            else
            {
                if (_debug)
                {
                    Debug.Log("Successful download");
                    Debug.Log(request.downloadHandler.text);
                }

                result[0] = request.downloadHandler.text;
            }
            
        }
        yield return null;
    }
}
