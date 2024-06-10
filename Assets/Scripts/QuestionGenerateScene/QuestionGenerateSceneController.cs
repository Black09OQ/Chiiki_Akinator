using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace QuestionGenerateScene{
    public class QuestionGenerateSceneController : MonoBehaviour
    {
        [SerializeField] GameObject workListPanel;
        [SerializeField] GameObject protocolListPanel;

        // Start is called before the first frame update
        void Start()
        {
            SetWorkListPanelActive();
        }

        public void EditWork(Work work)
        {
            protocolListPanel.GetComponent<ProtocolListPanel>().SetWork(work);
            SetProtocolListPanelActive();
        }

        public void AssignWork()
        {
            SetWorkListPanelActive();
        }

        public void MoveHomeScene()
        {
            SceneManager.LoadScene("HomeScene");
        }

        void SetWorkListPanelActive()
        {
            workListPanel.SetActive(true);
            protocolListPanel.SetActive(false);
        }

        void SetProtocolListPanelActive()
        {
            protocolListPanel.SetActive(true);
            workListPanel.SetActive(false);
        }
    }
}

