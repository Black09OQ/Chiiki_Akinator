using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;

public class OpenJTalkHandler : MonoBehaviour
{
    public async UniTask StartSpeak(string text, CancellationToken ct)
    {
        try
        {
            await UniTask.Run(() => OpenJTalk.SpeakStoppable(text));
        }
        catch(OperationCanceledException e)
        {
            Debug.Log("Speak stopped.");
            Stop();
        }
    }

    public void Stop()
    {
        OpenJTalk.StopSpeaking();
    }
}
