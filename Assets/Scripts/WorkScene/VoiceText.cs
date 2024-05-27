using UnityEngine;
using UnityEngine.Windows.Speech;
using TMPro;

namespace WorkScene
{
    public class VoiceText : MonoBehaviour
    {
        [SerializeField] TextMeshProUGUI text_;
        public DictationRecognizer m_DictationRecognizer;

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


            /*
            // 発音中
            m_DictationRecognizer.DictationHypothesis += DictationRecognizer_DictationHypothesis;
            */

            /*
            // 音声入力停止時に再起動
            m_DictationRecognizer.DictationComplete += (completionCause) =>
            {
                if (completionCause == DictationCompletionCause.TimeoutExceeded)
                {
                    //音声認識を起動。
                    m_DictationRecognizer.Start();
                }
                else
                {
                    //その他止まった原因に応じてハンドリング
                }
            };
            */

        }

        public void StartDictation()
        {
            m_DictationRecognizer.Start();
        }
    }

    
}

