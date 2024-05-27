using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace WorkSelectScene
{

    public class WorkSelectSceneController : MonoBehaviour
    {
        [SerializeField] GameObject workListPanel;
        void Start()
        {
            workListPanel.SetActive(true);
        }
        public void StartWork(Work work)
        {
            UserData.work = work;
            SceneManager.LoadScene("WorkScene");
        }
    }
}

