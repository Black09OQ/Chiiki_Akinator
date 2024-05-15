using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace LoginScene{
    public class RegistrationPanel : MonoBehaviour
    {
        [SerializeField] LoginSceneController controller;
        [SerializeField] TMP_InputField userIdInput;
        [SerializeField] TMP_InputField userNameInput;
        [SerializeField] TMP_InputField passwordInput;
        [SerializeField] TMP_InputField passwordConfirmInput;
        [SerializeField] DataManager dataManager;
        [SerializeField] WarnPanel warnPanel;

        public void Regist()
        {
            if(userIdInput.text == "" || userNameInput.text == "" || passwordInput.text == "" || passwordConfirmInput.text == "")
            {
                Debug.Log("Error: Input field not filled");
                warnPanel.SetText("入力されていない項目があります。");
                return;
            }

            List<User> primalyUsers = dataManager.GetUsersByUserId(userIdInput.text);
            if(primalyUsers.Count != 0)
            {
                Debug.Log("User already exist");
                warnPanel.SetText("このユーザーIDは既に登録されています。");
                return;
            }

            if(passwordConfirmInput.text != passwordInput.text)
            {
                Debug.Log("Error: Pasword not correct");
                warnPanel.SetText("パスワードが一致していません。");
                return;
            }

            User user = new User(){UserID = userIdInput.text, UserName = userNameInput.text, Password = passwordInput.text};
            dataManager.InsertUser(user);

            controller.MoveToLoginMode();

        }
    }
}

