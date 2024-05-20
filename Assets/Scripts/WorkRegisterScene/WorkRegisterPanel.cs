using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace WorkRegisterScene
{
    public class WorkRegisterPanel : MonoBehaviour
    {
        [SerializeField] WorkRegisterSceneController controller;
        [SerializeField] DataManager dataManager;

        public GameObject ProtocolNodePrefab;
        [SerializeField] TMP_InputField workNameInput;
        [SerializeField] Button plusButton;
        [SerializeField] Button minusButton;
        [SerializeField] GameObject ProtocolListPanel;

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
        void OnEnable()
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

        public void AddProtocol()
        {
            Protocol protocol = new Protocol(){WorkID = work.ID, Order = protocolNodes.Count + 1};
            GameObject obj = Instantiate(ProtocolNodePrefab, ProtocolListPanel.transform);
            ProtocolNode node = obj.GetComponent<ProtocolNode>();
            node.SetProtocol(protocol);
            protocolNodes.Add(obj);
        }

        public void Register()
        {
            work.Name = workNameInput.text;
            
            List<Work> primalyWorks = dataManager.GetWorksById(work.ID);
            if(primalyWorks.Count == 0){
                dataManager.InsertWork(work);
                Debug.Log("Work inserted");
            }
            else if(primalyWorks.Count == 1)
            {
                dataManager.UpdateWork(work);
                Debug.Log("Work updated");
            }
            else
            {
                Debug.Log("Error: Too many works exist!");
            }

            foreach(GameObject obj in protocolNodes)
            {
                ProtocolNode node = obj.GetComponent<ProtocolNode>();
                node.SetProtocolData(work.ID);
                List<Protocol> primalyProtocols = dataManager.GetProtocolsById(node.protocol.ID);
                if(primalyProtocols.Count == 0)
                {
                    dataManager.InsertProtocol(node.protocol);
                }
                else if(primalyProtocols.Count == 1)
                {
                    dataManager.UpdateProtocol(node.protocol);
                }
                else
                {
                    Debug.Log("Too many protocols exist!");
                }
            }

            controller.AssignWork();

        }

        public void CancelRegist(){
            controller.AssignWork();
        }
    }
}

