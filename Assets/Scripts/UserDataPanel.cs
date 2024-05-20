using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UIElements;

public class UserDataPanel : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI userIdTMP;
    [SerializeField] TextMeshProUGUI userNameTMP;
    // Start is called before the first frame update
    void Start()
    {
        userIdTMP.text = UserData.UserId;
        userNameTMP.text = UserData.UserName;
    }
}
