using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LogOutButton : MonoBehaviour
{

    public void LogOut()
    {
        SceneManager.LoadScene("LoginScene");
    }
}
