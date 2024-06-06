using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

namespace ManualSelectScene
{
    public class ManualSelectSceneController : MonoBehaviour
    {

        [SerializeField] GameObject workListPanel;
        void Start()
        {
            workListPanel.SetActive(true);
        }
        public void MoveManual(Work work)
        {
            UserData.work = work;
            SceneManager.LoadScene("ManualScene");
        }
    }
}

