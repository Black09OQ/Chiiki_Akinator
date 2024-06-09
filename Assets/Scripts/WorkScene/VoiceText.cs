using UnityEngine;
using UnityEngine.Windows.Speech;
using TMPro;
using Cysharp.Threading.Tasks;
using System.Threading;
using System;

namespace WorkScene
{
    public class VoiceText : MonoBehaviour
    {
        [SerializeField] TextMeshProUGUI text_;
        public DictationRecognizer m_DictationRecognizer;

        [SerializeField] WorkSceneManager manager;

        SynchronizationContext context;

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
            context = SynchronizationContext.Current;

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
                await dictationTask(ct);
            }
            catch(OperationCanceledException)
            {
                Debug.Log("Dictation canceled.");
            }
        }

        private async UniTask dictationTask(CancellationToken ct)
        {
            string preText = text_.text;
            try
            {
                context.Post(__ =>
                {
                    Debug.Log("Dictation Start");
                    m_DictationRecognizer.Start();
                }, null);
                await UniTask.Delay(3000);

                await UniTask.WaitUntil( ()=> text_.text != preText);
            }
            catch(OperationCanceledException)
            {
                Debug.Log("Dictation canceled.");
            }
        }
    }

    
}

