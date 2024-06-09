using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace LoginScene{
    public class LoginPanel : MonoBehaviour
    {
        [SerializeField] LoginSceneController controller;
        [SerializeField] TMP_InputField userIdInput;
        [SerializeField] TMP_InputField passwordInput;
        [SerializeField] DataManager dataManager;
        [SerializeField] WarnPanel warnPanel;

        void OnEnable()
        {
            Init();
        }

        void Init()
        {
            userIdInput.text = "";
            passwordInput.text = "";
        }

        public void Login()
        {
            if(userIdInput.text == "" || passwordInput.text == "")
            {
                Debug.Log("Error: No value in inputfield");
                warnPanel.SetText("入力を確認してください。");
                return;
            }

            List<User> primalyUsers = dataManager.GetUsersByUserId(userIdInput.text);
            if(primalyUsers.Count != 1)
            {
                if(primalyUsers.Count == 0)
                {
                    Debug.Log("Error: User not found");
                    warnPanel.SetText("ユーザーが見つかりません");
                }
                else if(primalyUsers.Count > 1)
                {
                    Debug.Log("Error: Too many users exist");
                }
                else
                {
                    Debug.Log("Error: Unknown error");
                }
                
                return;
            }

            foreach(User user in primalyUsers)
            {
                if(user.Password != passwordInput.text)
                {
                    Debug.Log("Password incorrect");
                    warnPanel.SetText("パスワードが違います。");
                    return;
                }
            }

            foreach(User user in primalyUsers)
            {
                UserData.Id = user.ID;
                UserData.UserId = user.UserID;
                UserData.UserName = user.UserName;
            }

            controller.MoveToHomeScene();
        }
    }
}

