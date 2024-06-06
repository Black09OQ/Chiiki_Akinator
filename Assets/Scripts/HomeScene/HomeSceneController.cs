using UnityEngine;
using UnityEngine.SceneManagement;

public class HomeSceneController : MonoBehaviour
{
    public void MoveWorkScene()
    {
        SceneManager.LoadScene("WorkSelectScene");
    }

    public void MoveManualScene()
    {
        SceneManager.LoadScene("ManualSelectScene");
    }

    public void MoveWorkRegisterScene()
    {
        SceneManager.LoadScene("WorkRegisterScene");
    }

    public void MoveConfigScene()
    {
        SceneManager.LoadScene("ConfigScene");
    }
}
