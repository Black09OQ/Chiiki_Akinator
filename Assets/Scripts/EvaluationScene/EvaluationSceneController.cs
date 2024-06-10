using UnityEngine;
using UnityEngine.SceneManagement;

namespace EvaluationScene
{
    public class EvaluationSceneController : MonoBehaviour
    {
        [SerializeField] GameObject evaluationPanel;

        // Start is called before the first frame update
        void Start()
        {
           evaluationPanel.SetActive(true);
        }

        public void AssignWork()
        {
            SceneManager.LoadScene("HomeScene");
        }
    }
}

