using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace WorkRegisterScene
{
    public class WorkRegisterPanel : MonoBehaviour
    {
        public GameObject ProtocolNodePrefab;

        [SerializeField] TMP_InputField workNameInput;
        [SerializeField] Button plusButton;
        [SerializeField] Button minusButton;
        [SerializeField] GameObject ProtocolListPanel;
        [SerializeField] List<GameObject> protocolNodes;

        

        Work work;

        public void SetWork(Work w)
        {
            work = w;
        }

        // Start is called before the first frame update
        void OnEnable()
        {
            if(work.workName != null)
            {
                workNameInput.text = work.workName;
            }

            if(work.workProtocol != null)
            {
                int protocolID = 0;
                foreach (string protocol in work.workProtocol)
                {
                    GameObject obj = Instantiate(ProtocolNodePrefab, ProtocolListPanel.transform);
                    ProtocolNode node = obj.GetComponent<ProtocolNode>();
                    node.SetProtocolID(protocolID + 1);
                    node.SetInputText(protocol);
                    protocolNodes.Add(obj);

                    protocolID++;
                }
            }
        }

        public void AddProtocol()
        {
            GameObject obj = Instantiate(ProtocolNodePrefab, ProtocolListPanel.transform);
            ProtocolNode node = obj.GetComponent<ProtocolNode>();
            protocolNodes.Add(obj);
            node.SetProtocolID(protocolNodes.Count);
        }

        public void Register()
        {
            work.workName = workNameInput.text;
            
            work.workProtocol = new List<string>();
            foreach(GameObject obj in protocolNodes)
            {
                ProtocolNode node = obj.GetComponent<ProtocolNode>();
                work.workProtocol.Add(node.protocol);
            }

            GameObject.Find("WorkRegisterSceneController").GetComponent<WorkRegisterSceneController>().AddWork(work);
        }
    }
}

