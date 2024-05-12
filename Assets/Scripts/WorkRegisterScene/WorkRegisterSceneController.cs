using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;

namespace WorkRegisterScene
{
    public class WorkRegisterSceneController : MonoBehaviour
    {
        [SerializeField] GameObject workListPanel;
        [SerializeField] GameObject workRegisterPanel;

        // Start is called before the first frame update
        void Start()
        {
            SetWorkListPanelActive();
        }

        public void ResistWork()
        {
            workRegisterPanel.GetComponent<WorkRegisterPanel>().SetWork(new Work() { Name = null });
            SetWorkRegisterPanelActive();
        }

        public void EditWork(Work work)
        {
            workRegisterPanel.GetComponent<WorkRegisterPanel>().SetWork(work);
            SetWorkRegisterPanelActive();
        }

        public void AssignWork()
        {
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

