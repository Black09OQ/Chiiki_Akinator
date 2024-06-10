using UnityEngine;
using UnityEngine.SceneManagement;

namespace EvaluationScene
{
    public class EvaluationSceneController : MonoBehaviour
    {
        [SerializeField] GameObject workListPanel;
        [SerializeField] GameObject evaluationPanel;

        // Start is called before the first frame update
        void Start()
        {
            SetWorkListPanelActive();
        }

        public void RegistWork()
        {
            evaluationPanel.GetComponent<EvaluationPanel>().SetWork(new Work() { Name = null });
            SetEvaluationPanelActive();
        }

        public void EditWork(Work work)
        {
            evaluationPanel.GetComponent<EvaluationPanel>().SetWork(work);
            SetEvaluationPanelActive();
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
            evaluationPanel.SetActive(false);
        }

        void SetEvaluationPanelActive()
        {
            evaluationPanel.SetActive(true);
            workListPanel.SetActive(false);
        }
    }
}

