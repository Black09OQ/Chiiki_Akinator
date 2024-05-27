using System.Collections.Generic;
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
        [SerializeField] AudioManager audioManager;

        Work work;

        List<Protocol> protocols;

        List<Question> questions;

        int order;

        void Init()
        {
            order = 0;
            work = new Work();
            protocols = new List<Protocol>();
            questions = new List<Question>();
            protocolTMP.SetText("回答文がここに表示されます。");
        }

        // Start is called before the first frame update
        void Start()
        {
            Init();
            work = UserData.work;
            workTMP.text = work.Name;
            GenerateQuestions();
        }

        void GenerateQuestions()
        {
            questions = dataManager.GetQuestionsByWorkId(work.ID);
        }

        public async void StartWork()
        {
            await movieHandler.StartRecording();
            voiceText.StartDictation();
        }

        void SetProtocols(int order)
        {
            protocolTMP.SetText(protocols[order].Name);
        }
    }
}


