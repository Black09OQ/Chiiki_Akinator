using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cysharp.Threading.Tasks;
using UnityEngine.Networking;
using System;
using Unity.Collections;

public class MovieHandler : MonoBehaviour
{
    public string cameraUri;
    public string startRecordPath;
    public string stopRecordPath;
    public string modeChangePath;
    public string getMovieListPath;
    public string getMoviePath = "";
    public string movieList;

    UnityWebRequest request;

    void Start()
    {
        movieList = "";
    }
    
    public async UniTask StartRecording()
    {
        Debug.Log("Sending Record Start Request");
        await SendStartRecordRequest();
    }

    public async UniTask FinishRecording()
    {
        await SendStopRecordRequest();
    }

    async void OnDestroy() {
        UniTask stopRecord = UniTask.RunOnThreadPool(() => SendStopRecordRequest());
        await UniTask.WaitUntil(()=> stopRecord.GetAwaiter().IsCompleted);
    }

    public async UniTask SendStartRecordRequest()
    {
        
        Debug.Log("Sending...");
        request = UnityWebRequest.Get($"{cameraUri}{modeChangePath}");
        
        try
        {
            Debug.Log("Sending request to GoPro...");
            await request.SendWebRequest();
            Debug.Log(request.downloadHandler.text);
        }
        catch (Exception ex)
        {
            Debug.Log($"Request error: {ex.Message}");
            request.Dispose();
            return;
        }        

        request = UnityWebRequest.Get($"{cameraUri}{startRecordPath}");
        
        try
        {
            Debug.Log("Sending request to GoPro...");
            await request.SendWebRequest();
        }
        catch (Exception ex)
        {
            Debug.LogError($"Request Error: {ex.Message}");
        }

        request.Dispose();
    }


    // 録画停止リクエスト
    private async UniTask SendStopRecordRequest()
    {
        request = UnityWebRequest.Get($"{cameraUri}{stopRecordPath}");

        try
        {
            await request.SendWebRequest();
            Debug.Log("Stopped recording!");
        }
        catch(Exception e)
        {
            Debug.Log($"Error: {e.Message}");
            request.Dispose();
            return;
        }

        request = UnityWebRequest.Get($"{cameraUri}{getMovieListPath}");
        try
        {
            await request.SendWebRequest();
            movieList = request.downloadHandler.text;
            
        }
        catch(Exception e)
        {
            Debug.LogError($"Error: {e.Message}");
            movieList = "";
        }

        request.Dispose();
    }
}
