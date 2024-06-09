using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class GPTTest : MonoBehaviour
{
    public string url;
    public string prompt;

    public void SendToGPT()
    {
        StartCoroutine(SendPrompt());
    }


    public IEnumerator SendPrompt()
    {
        string jsonData = prompt;
        byte[] postData = System.Text.Encoding.UTF8.GetBytes (jsonData);
        UnityWebRequest request = new UnityWebRequest(url, "POST");
        request.uploadHandler = new UploadHandlerRaw(postData);
        request.downloadHandler = new DownloadHandlerBuffer();
        request.SetRequestHeader("Content-Type", "application/json");
        yield return request.SendWebRequest();

        Debug.Log(request.downloadHandler.text);
    }
}
