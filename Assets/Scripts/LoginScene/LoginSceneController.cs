using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace LoginScene{
    public class LoginSceneController : MonoBehaviour
    {
        [SerializeField] GameObject LoginPanel;
        [SerializeField] GameObject RegistrationPanel;
        [SerializeField] GameObject warnPanel;

        void Start()
        {
            SetLoginPanelActive();    
        }

        public void MoveToHomeScene()
        {
            SceneManager.LoadScene("HomeScene");
        }

        public void MoveToLoginMode()
        {
            SetLoginPanelActive();
        }

        public void MoveToRegistrationMode()
        {
            SetRegistrationPanelActive();
        }

        void SetLoginPanelActive()
        {
            LoginPanel.SetActive(true);
            RegistrationPanel.SetActive(false);
            warnPanel.SetActive(false);
        }

        void SetRegistrationPanelActive()
        {
            RegistrationPanel.SetActive(true);
            LoginPanel.SetActive(false);
            warnPanel.SetActive(false);
        }
    }
}

