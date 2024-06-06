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
    
    public IEnumerator StartRecording()
    {
        Debug.Log("Sending Record Start Request");
        UniTask modeChange = UniTask.Run(()=>SendStartRecordRequest());
        yield return new WaitUntil(() => modeChange.GetAwaiter().IsCompleted);
    }

    public IEnumerator FinishRecording()
    {
        UniTask finishRecord = UniTask.Run(()=> SendStopRecordRequest());
        new WaitUntil(() => finishRecord.GetAwaiter().IsCompleted);
        
        UniTask getMovieList = UniTask.Run(()=> SendGetMovieListRequest());
        yield return new WaitUntil(() => getMovieList.GetAwaiter().IsCompleted);
        
    }

    async void OnDestroy() {
        UniTask stopRecord = UniTask.Run(() => SendStopRecordRequest());
        await UniTask.WaitUntil(()=> stopRecord.GetAwaiter().IsCompleted);
    }

    public async void SendStartRecordRequest()
    {
        
        Debug.Log("Sending...");
        request = new UnityWebRequest($"{cameraUri}/{modeChangePath}", "GET");
        Debug.Log("Sending request to GoPro...");
        await request.SendWebRequest();
        Debug.Log(request.downloadHandler.text);
        

/*
        request = new UnityWebRequest($"{cameraUri}{startRecordPath}", "GET");
        Debug.Log("Sending request to GoPro...");
        await request.SendWebRequest();
        Debug.Log(request.downloadHandler.text);*/



        request.Dispose();
    }


    // 録画停止リクエスト
    private async void SendStopRecordRequest()
    {
        request = new UnityWebRequest($"{cameraUri}/{stopRecordPath}", "GET");

        try
        {
            await request.SendWebRequest();
            Debug.Log("Stopped recording!");
        }
        catch(Exception e)
        {
            Debug.Log($"Error: {e.Message}");
        }

        request.Dispose();
    }

    private async void SendGetMovieListRequest()
    {
        UnityWebRequest request = new UnityWebRequest($"{cameraUri}/{getMovieListPath}", "GET");
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
