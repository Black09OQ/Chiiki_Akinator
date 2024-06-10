using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace EvaluationScene
{
    public class EvaluationPanel : MonoBehaviour
    {
        [SerializeField] EvaluationSceneController controller;
        [SerializeField] DataManager dataManager;

        public GameObject ProtocolNodePrefab;
        [SerializeField] TextMeshProUGUI workNameTMP;
        [SerializeField] GameObject ProtocolListPanel;

        private List<Protocol> protocols;
        [SerializeField] List<GameObject> protocolNodes;



        Work work;

        void Init()
        {
            workNameTMP.text = "";
            foreach (GameObject obj in protocolNodes)
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
            work = UserData.work;

            if (work != null)
            {
                // 作成済み作業の場合、作業名をInputFieldに書き込む
                if (work.Name != null)
                {
                    workNameTMP.text = work.Name;
                }

                // 作業手順が存在する場合、ProtocolNodeを生成して書き込む
                protocols = dataManager.GetProtocolsByWorkId(work.ID);
                foreach (Protocol protocol in protocols)
                {
                    GameObject obj = Instantiate(ProtocolNodePrefab, ProtocolListPanel.transform);
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


        public void Register()
        {
            foreach(GameObject obj in protocolNodes)
            {
                ProtocolNode node = obj.GetComponent<ProtocolNode>();
                int value = (int)node.nowValue;

                Evaluation evaluation = new Evaluation();
                evaluation.ProtocolID = node.protocol.ID;
                evaluation.Value = value;
                dataManager.InsertEvaluation(evaluation);
            }

            controller.AssignWork();

        }

        public void CancelRegist()
        {
            controller.AssignWork();
        }
    }
}

