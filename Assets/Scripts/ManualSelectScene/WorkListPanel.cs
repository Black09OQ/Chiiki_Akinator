using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ManualSelectScene
{
    public class WorkListPanel : MonoBehaviour
    {
        [SerializeField] ManualSelectSceneController controller;
        private List<Work> works;
        [SerializeField] GameObject WorkNodePrefab;
        
        [SerializeField] GameObject nodeConteiner;
        [SerializeField] List<GameObject> workNodes;
        [SerializeField] DataManager dataManager;

        // Start is called before the first frame update

        void Init(){
            works = new List<Work>();
            foreach(GameObject obj in workNodes)
            {
                Destroy(obj);
            }
            workNodes.Clear();
        }

        void OnEnable()
        {
            Init();

            works = dataManager.GetAllWorks();
            
            foreach (Work work in works)
            {
                GameObject obj = Instantiate(WorkNodePrefab, nodeConteiner.transform);
                WorkNode node = obj.GetComponent<WorkNode>();
                node.SetWork(work);
                workNodes.Add(obj);
            }
        }

        public void StartWork(Work work)
        {
            controller.MoveManual(work);
        }
    }
}

