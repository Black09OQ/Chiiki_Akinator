using System;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.Networking;

public class GPTTest : MonoBehaviour
{
    public string url;
    private string prompt;

    public async UniTask<string> SendToGPT(string workName, string protocolName)
    {
        SetPrompt(workName,protocolName);
        return await SendPrompt();
    }

    private void SetPrompt(string workName, string protocolName)
    {
        string userMessage = "金属加工の町工場で働く若手職人の技術習得をサポートするため、作業時の感覚をメタ認知により認知させようとしています。そこで、作業者のメタ認知を誘発させる質問を投げかけようと思います。「{workName}」作業の「{protocolName}」工程の中で、金属を研磨している若手職人に対して投げかける質問を10個以上考えて、json形式で出力してください。出力するjsonの形式は次の通りでお願いします。{\\\"Questions\\\": [{\\\"question\\\": \\\"質問文\\\"},{\\\"question\\\": \\\"質問文\\\"}]}";
        string text = "{\"message\":\"" + userMessage + "\"}";

        prompt = text.Replace("{workName}", workName).Replace("{protocolName}", protocolName);

    }


    private async UniTask<string> SendPrompt()
    {
        string questionsData;
        byte[] postData = System.Text.Encoding.UTF8.GetBytes (prompt);
        UnityWebRequest request = new UnityWebRequest(url, "POST");
        request.uploadHandler = new UploadHandlerRaw(postData);
        request.downloadHandler = new DownloadHandlerBuffer();
        request.SetRequestHeader("Content-Type", "application/json");
        try
        {
            await request.SendWebRequest();
            questionsData = request.downloadHandler.text;
        }
        catch (Exception ex)
        {
            Debug.Log($"Generate Error: {ex.Message}");
            questionsData = "";
        }

        return questionsData;
    }
}
