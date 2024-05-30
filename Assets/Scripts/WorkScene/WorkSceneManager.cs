using System;
using System.Collections.Generic;
using System.Threading;
using Cysharp.Threading.Tasks;
using TMPro;
using UnityEngine;

namespace WorkScene
{
    public class WorkSceneManager : MonoBehaviour
    {
        [SerializeField] DataManager dataManager;
        [SerializeField] TextMeshProUGUI workTMP;
        [SerializeField] TextMeshProUGUI protocolTMP;
        [SerializeField] VoiceText voiceText;
        [SerializeField] MovieHandler movieHandler;
        [SerializeField] OpenJTalkHandler talkHandler;
        [SerializeField] AudioManager audioManager;
        [SerializeField] GameObject workStartButton;
        [SerializeField] GameObject nextProtocolButton;

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

        public async void StartWork()
        {
            await movieHandler.StartRecording();
            workStartButton.SetActive(false);
            nextProtocolButton.SetActive(true);
            RunQA(QACTS.Token);
        }

        async UniTask RunQA(CancellationToken cts)
        {
            if(order < protocols.Count)
            {
                SetProtocols();
                List<Question> q = questions[order];
                foreach(Question question in q)
                {
                    try
                    {
                        await talkHandler.StartSpeak(question.Name, cts);
                    }
                    catch (OperationCanceledException e)
                    {
                        talkHandler.Stop();
                    }

                    try
                    {

                    }
                    catch (OperationCanceledException e)
                    {

                    }
                }
            }
        }

        public void StopQA()
        {
            QACTS.Cancel();
        }

        void SetProtocols()
        {
            protocolTMP.SetText(protocols[order].Name);
        }
    }
}


