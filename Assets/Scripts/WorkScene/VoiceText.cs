using UnityEngine;
using UnityEngine.Windows.Speech;
using TMPro;
using Cysharp.Threading.Tasks;
using System.Threading;
using System;
using Unity.VisualScripting;

namespace WorkScene
{
    public class VoiceText : MonoBehaviour
    {
        [SerializeField] TextMeshProUGUI text_;
        public DictationRecognizer m_DictationRecognizer;

        [SerializeField] WorkSceneManager manager;

        // オブジェクトが破棄されるとき
        private void OnDestroy()
        {
            // 破棄
            // 下記を記載しないと処理中断時にエラーになる
            m_DictationRecognizer.Stop();
            m_DictationRecognizer.Dispose();
        }

        void Start()
        {
            m_DictationRecognizer = new DictationRecognizer();

            // 発音終了時
            //DictationResultのイベントを登録
            m_DictationRecognizer.DictationResult += (text, confidence) =>
            {
                //音声認識した文章はtextで受け取れます。
                text_.text = text;
            };
        }

        public async UniTask StartDictation(CancellationToken ct)
        {
            try
            {
                var dicTask = dictationTask(ct);
                await UniTask.Delay(5000);
                var delay = UniTask.Delay(15000);
                await UniTask.WhenAny(dicTask,delay);
                m_DictationRecognizer.Stop();
            }
            catch(OperationCanceledException e)
            {
                Debug.Log("Dictation canceled.");
            }
        }

        private async UniTask dictationTask(CancellationToken ct)
        {
            try
            {
                m_DictationRecognizer.Start();
                await UniTask.WaitUntil( ()=> m_DictationRecognizer.Status == SpeechSystemStatus.Stopped || m_DictationRecognizer.Status ==  SpeechSystemStatus.Failed);
            }
            catch(OperationCanceledException e)
            {
                Debug.Log("Dictation canceled.");
            }
        }
    }

    
}

