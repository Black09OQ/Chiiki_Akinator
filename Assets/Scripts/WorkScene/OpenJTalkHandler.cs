using System.Collections;
using System.Collections.Generic;
using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;

public class OpenJTalkHandler : MonoBehaviour
{
    public async UniTask StartSpeak(string text, CancellationToken cts)
    {
        await OpenJTalk.SpeakStoppable(text);
    }

    public void Stop()
    {
        OpenJTalk.StopSpeaking();
    }
}
