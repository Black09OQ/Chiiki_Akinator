using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Text.RegularExpressions;
using TMPro;
using System;

namespace QuestionGenerateScene
{
    public class ProtocolListPanel : MonoBehaviour
    {
        [SerializeField] QuestionGenerateSceneController controller;
        [SerializeField] DataManager dataManager;
        [SerializeField] GPTTest gpt;

        public GameObject protocolNodePrefab;
        [SerializeField] TextMeshProUGUI workNameInput;
        [SerializeField] GameObject protocolListPanel;

        private List<Protocol> protocols;
        [SerializeField] List<GameObject> protocolNodes;

        

        Work work;

        void Init()
        {
            workNameInput.text = "";
            foreach(GameObject obj in protocolNodes)
            {
                Destroy(obj);
            }
            protocolNodes.Clear();
        }

        public void SetWork(Work w)
        {
            work = w;
        }

        // Start is called before the first frame update
        void Start()
        {
            Init();
            
            if(work != null){
                // 作成済み作業の場合、作業名をInputFieldに書き込む
                if(work.Name != null)
                {
                    workNameInput.text = work.Name;
                }

                // 作業手順が存在する場合、ProtocolNodeを生成して書き込む
                protocols = dataManager.GetProtocolsByWorkId(work.ID);
                foreach (Protocol protocol in protocols)
                {
                    GameObject obj = Instantiate(protocolNodePrefab, protocolListPanel.transform);
                    ProtocolNode node = obj.GetComponent<ProtocolNode>();
                    node.SetProtocol(protocol);
                    protocolNodes.Add(obj);
                }
            }


            

        }

        void OnDisable()
        {
            work = null;    
        }

        public async void Register()
        {
            string questionJson;
            
            
            foreach(Protocol p in protocols)
            {
                Debug.Log($"Generating {(protocols.IndexOf(p)+1).ToString()} in {protocols.Count.ToString()}");
                string responseJson = await gpt.SendToGPT(work.Name, p.Name);
                Debug.Log(responseJson);
                string encodeJson = ExtractResponseContent(responseJson);
                questionJson = Regex.Unescape(encodeJson);
                QuestionsData questions = JsonUtility.FromJson<QuestionsData>(questionJson);
                foreach(QuestionStr qStr in questions.Questions)
                {
                    Question question = new Question();
                    question.Name = qStr.question;
                    question.WorkID = work.ID;
                    question.ProtocolID = p.ID;

                    dataManager.InsertQuestion(question);
                }
            }

            controller.AssignWork();

        }

        public void CancelRegist(){
            controller.AssignWork();
        }

        string ExtractResponseContent(string json)
        {
            // シンプルな方法として、JSONをデシリアライズしてresponseフィールドを取得
            try
            {
                ResponseWrapper wrapper = JsonUtility.FromJson<ResponseWrapper>(json);
                return wrapper.response;
            }
            catch (Exception ex)
            {
                Debug.LogError("Failed to extract response content: " + ex.Message);
                return null;
            }
        }

        [Serializable]
        public class ResponseWrapper
        {
            public string response;
        }

    }

    [Serializable]
    public class QuestionsData
    {
        public List<QuestionStr> Questions;
    }

    [Serializable]
    public class QuestionStr
    {
        public string question;
    }

    
}

