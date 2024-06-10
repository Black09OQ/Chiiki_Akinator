using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Cysharp.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace WorkScene
{
    public class WorkSceneManager : MonoBehaviour
    {
        [SerializeField] DataManager dataManager;
        [SerializeField] TextMeshProUGUI workTMP;
        [SerializeField] TextMeshProUGUI protocolTMP;
        [SerializeField] TextMeshProUGUI questionTMP;
        [SerializeField] TextMeshProUGUI answerTMP;
        [SerializeField] VoiceText voiceText;
        [SerializeField] MovieHandler movieHandler;
        [SerializeField] OpenJTalkHandler talkHandler;
        [SerializeField] AudioManager audioManager;
        [SerializeField] GameObject workStartButton;
        [SerializeField] GameObject nextProtocolButton;
        [SerializeField] GameObject connectingPanel;

        Work work;

        List<Protocol> protocols;

        List<List<Question>> questions;

        CancellationTokenSource QACTS;

        int order;

        void Init()
        {
            order = 0;
            QACTS = new CancellationTokenSource();
            work = new Work();
            protocols = new List<Protocol>();
            questions = new List<List<Question>>();
            protocolTMP.SetText("回答文がここに表示されます。");
        }

        // Start is called before the first frame update
        void Start()
        {
            connectingPanel.SetActive(false);
            Init();
            work = UserData.work;
            workTMP.text = work.Name;
            protocols = dataManager.GetProtocolsByWorkId(work.ID);
            GenerateQuestions();
        }

        void GenerateQuestions()
        {
            foreach(Protocol p in protocols){
                List<Question> pQuestions = dataManager.GetQuestionsByProtocolId(p.ID);
                questions.Add(pQuestions);
            }
        }

        private async UniTaskVoid StartWork()
        {
            connectingPanel.SetActive(true);
            await movieHandler.StartRecording();
            connectingPanel.SetActive(false);
            workStartButton.SetActive(false);
            nextProtocolButton.SetActive(true);
            try
            {
                await RunQA(QACTS.Token);
            }
            catch(OperationCanceledException)
            {
                Debug.Log("RunQA canceled.");
            }
        }

        async UniTask RunQA(CancellationToken cts)
        {
            if(order < protocols.Count)
            {
                // protocolTMPに作業手順名を表示
                protocolTMP.SetText(protocols[order].Name);
                await UniTask.WaitUntil(() => protocolTMP.text == protocols[order].Name); // 表示完了まで待つ

                List<Question> q = questions[order];
                foreach(Question question in q)
                {
                    // 質問TMPに質問文を表示
                    questionTMP.SetText(question.Name);
                    await UniTask.WaitUntil(() => questionTMP.text == question.Name); // 表示完了まで待ち
                    // 回答TMPの中身を消す
                    answerTMP.text = "";
                    await UniTask.WaitUntil(() => answerTMP.text == ""); 
                    try
                    {
                        await talkHandler.StartSpeak(question.Name, QACTS.Token);
                    }
                    catch (OperationCanceledException)
                    {
                        Debug.Log("StartSpeak canceled");
                        talkHandler.Stop();
                    }

                    try
                    {
                        audioManager.PlayStartSound();
                        await voiceText.StartDictation(QACTS.Token);
                        audioManager.PlayCompleteSound();
                        await UniTask.Delay(1000);
                    }
                    catch (OperationCanceledException)
                    {
                        Debug.Log("Dictation task canceled");
                    }

                    // 記録処理
                    Result result = new Result();
                    result.QuestionId = question.ID;
                    result.Answer = answerTMP.text;
                    result.UserId = UserData.Id;

                    dataManager.InsertResult(result);
                }
            }
        }

        public async void MoveNextProtocol()
        {
            QACTS.Cancel();

            if(order <= protocols.Count)
            {
                order ++;
            }

            try
            {
                await RunQA(QACTS.Token);
            }
            catch(OperationCanceledException)
            {
                Debug.Log("QATask Canceled.");
            }
        }

        public void StopQA()
        {
            QACTS.Cancel();
            Debug.Log("QATask Canceled.");
        }

        public async void FinishWork()
        {
            StopQA();
            Debug.Log("Finish work!");
            connectingPanel.SetActive(true);
            await movieHandler.FinishRecording();
            connectingPanel.SetActive(false);
            string movieList = movieHandler.movieList;
            string moviePath = "";

            if(movieList != "")
            {
                GoProMedia mediaList = JsonUtility.FromJson<GoProMedia>(movieList);
                // デシリアライズしたデータの確認
                Debug.Log("Media ID: " + mediaList.id);
                if(mediaList != null)
                {
                    foreach (var folder in mediaList.media)
                    {
                        Debug.Log("Folder: " + folder.d);
                        List<GoProFile> files = folder.fs;
                        moviePath = files.Last().n;
                    }

                    MoviePath path = new MoviePath();
                    path.Path = moviePath;
                    path.UserID = UserData.Id;
                    path.WorkID = UserData.work.ID;

                    dataManager.InsertMoviePath(path);
                }
            }

            SceneManager.LoadScene("EvaluationScene");
        }

        public void OnWorkStartButtonDown()
        {
            StartWork().Forget();
        }
    }

    [Serializable]
    public class GoProFile
    {
        public string n;    // ファイル名
        public string cre;  // 作成日時（エポックタイム）
        public string mod;  // 修正日時（エポックタイム）
        public string glrv; // 追加データ
        public string ls;   // その他のデータ
        public string s;    // サイズ
    }

    [Serializable]
    public class MediaFolder
    {
        public string d;       // ディレクトリ名
        public List<GoProFile> fs; // ファイルのリスト
    }

    [Serializable]
    public class GoProMedia
    {
        public string id;              // メディアID
        public List<MediaFolder> media; // メディアフォルダのリスト
    }
}




