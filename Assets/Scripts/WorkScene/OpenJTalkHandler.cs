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
            await UniTask.RunOnThreadPool(() => OpenJTalk.SpeakStoppable(text));
        }
        catch(OperationCanceledException)
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
