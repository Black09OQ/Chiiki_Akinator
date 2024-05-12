using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace WorkRegisterScene
{
    public class WorkRegisterSceneController : MonoBehaviour
    {
        [SerializeField] GameObject workListPanel;
        [SerializeField] GameObject workRegisterPanel;
        [SerializeField] DataLoder dataLoder;

        public List<Work> workList;
        // Start is called before the first frame update
        void Start()
        {
            workList = dataLoder.LoadData();
        }

        public void RegistWork()
        {
            Work w = new Work() {workID = workList.Count};
            workRegisterPanel.GetComponent<WorkRegisterPanel>().SetWork(w);
            SetWorkRegisterPanelActive();
        }

        public void EditWork(Work work)
        {
            workRegisterPanel.GetComponent<WorkRegisterPanel>().SetWork(work);
            SetWorkRegisterPanelActive();
        }

        public void AddWork(Work work)
        {
            workList.Add(work);
            SetWorkListPanelActive();
        }



        void SetWorkListPanelActive()
        {
            workListPanel.SetActive(true);
            workRegisterPanel.SetActive(false);
        }

        void SetWorkRegisterPanelActive()
        {
            workRegisterPanel.SetActive(true);
            workListPanel.SetActive(false);
        }
    }
}

