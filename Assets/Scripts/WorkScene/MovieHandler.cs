using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cysharp.Threading.Tasks;
using UnityEngine.Networking;
using System;

public class MovieHandler : MonoBehaviour
{
    public string cameraUri;
    public string startRecordPath;
    public string stopRecordPath;
    public string modeChangePath;
    public string getMoviePath;

    
    public async UniTask StartRecording()
    {
        UnityWebRequest request = new UnityWebRequest($"{cameraUri}/{modeChangePath}", "GET");
        try
        {
            await request.SendWebRequest();
            Debug.Log(request.downloadHandler.text);
        }
        catch(Exception e)
        {
            Debug.Log(e.ToString());
        }


        request = new UnityWebRequest($"{cameraUri}/{startRecordPath}", "GET");        
        try
        {
            await request.SendWebRequest();
            Debug.Log(request.downloadHandler.text);   
        }
        catch (Exception e)
        {
            Debug.Log(e.ToString());
        }
    }
    

    public async UniTask StopRecording()
    {
        UnityWebRequest request = new UnityWebRequest($"{cameraUri}/{stopRecordPath}", "GET");

        await request.SendWebRequest();
        Debug.Log(request.downloadHandler.text);   
    }

}
