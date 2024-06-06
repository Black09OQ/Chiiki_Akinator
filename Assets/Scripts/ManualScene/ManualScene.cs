using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;
using UnityEngine.EventSystems;

namespace ManualSeane
{
    public class VideoController : MonoBehaviour
    {
        public Button startButton; // Button start をInspectorからアサインしてください
        public Button stopButton;  // Button stop をInspectorからアサインしてください
        public Button pauseButton; // Button pause をInspectorからアサインしてください
        public Button toggleButton1; // 新しいボタン1をInspectorからアサインしてください
        public Button toggleButton2; // 新しいボタン2をInspectorからアサインしてください
        public VideoPlayer videoPlayer1; // Video Player 1をInspectorからアサインしてください
        public VideoPlayer videoPlayer2; // Video Player 2をInspectorからアサインしてください
        public Slider slider1; // Slider 1をInspectorからアサインしてください
        public Slider slider2; // Slider 2をInspectorからアサインしてください

        void Start()
        {
            // ボタンにクリックイベントを追加
            startButton.onClick.AddListener(PlayVideos);
            stopButton.onClick.AddListener(StopVideos);
            pauseButton.onClick.AddListener(PauseVideos);
            toggleButton1.onClick.AddListener(ToggleVideo1); // 新しいボタン1にクリックイベントを追加
            toggleButton2.onClick.AddListener(ToggleVideo2); // 新しいボタン2にクリックイベントを追加
        }

        void Update()
        {
            if (videoPlayer1.isPlaying)
            {
                slider1.value = (float)(videoPlayer1.time / videoPlayer1.clip.length);
            }

            if (videoPlayer2.isPlaying)
            {
                slider2.value = (float)(videoPlayer2.time / videoPlayer2.clip.length);
            }
        }

        void PlayVideos()
        {
            // 動画を再生
            videoPlayer1.Play();
            videoPlayer2.Play();
        }

        void StopVideos()
        {
            // 動画を停止
            videoPlayer1.Stop();
            videoPlayer2.Stop();
        }

        void PauseVideos()
        {
            // 動画を一時停止
            videoPlayer1.Pause();
            videoPlayer2.Pause();
        }

        void ToggleVideo1() // 新しいボタン1が押されたときに呼び出されるメソッド
        {
            if (videoPlayer1.isPlaying)
            {
                videoPlayer1.Pause(); // videoplayer 1 が再生中なら一時停止
            }
            else
            {
                videoPlayer1.Play(); // そうでなければ再生
            }
        }

        void ToggleVideo2() // 新しいボタン2が押されたときに呼び出されるメソッド
        {
            if (videoPlayer2.isPlaying)
            {
                videoPlayer2.Pause(); // videoplayer 2 が再生中なら一時停止
            }
            else
            {
                videoPlayer2.Play(); // そうでなければ再生
            }
        }
    }
}
