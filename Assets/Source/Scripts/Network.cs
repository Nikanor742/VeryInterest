using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Networking;

public class Network: MonoBehaviour
{
    public event Action<string> OnJSONDownloaded;

    [SerializeField] private string downloadLink;
    

    private void Start()
    {
        LoadData();
    }

    private IEnumerator GetRequest(string url, Action<UnityWebRequest> callback)
    {
        using (UnityWebRequest request = UnityWebRequest.Get(url))
        {
            yield return request.SendWebRequest();
            callback(request);
        }
    }
    public void LoadData()
    {
        StartCoroutine(GetRequest(downloadLink, (UnityWebRequest req) =>
        {
            if (req.isNetworkError || req.isHttpError)
            {
                Debug.Log($"{req.error}: {req.downloadHandler.text}");
            }
            else
            {
                OnJSONDownloaded?.Invoke(req.downloadHandler.text);
            }
        }));
    }
}
