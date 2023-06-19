using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.InteropServices;
using UnityEngine.Networking;
using TMPro;
using UnityEngine.UI;

public class Yandex : MonoBehaviour
{
    [DllImport("__Internal")]
    private static extern void GiveMePlayerData();

    [SerializeField] private RawImage texture;

    private void Start()
    {
        GiveMePlayerData();
    }

    public void PrintName(string name)
    {
        print(name);
    }

    public void SetPhoto(string url)
    {
        StartCoroutine(DownLoadImage(url));
    }

    IEnumerator DownLoadImage(string mediaUrl)
    {
        UnityWebRequest request = UnityWebRequestTexture.GetTexture(mediaUrl);
        yield return request.SendWebRequest();
        if (request.result == UnityWebRequest.Result.ConnectionError || request.result == UnityWebRequest.Result.ProtocolError)
            Debug.Log(request.error);
        else
            texture.texture = ((DownloadHandlerTexture)request.downloadHandler).texture;
    }
}
