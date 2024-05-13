using UnityEngine;
using UnityEngine.SceneManagement;

public class HomeSceneController : MonoBehaviour
{
    public void MoveWorkScene()
    {
        SceneManager.LoadScene("WorkScene");
    }

    public void MoveManualScene()
    {
        SceneManager.LoadScene("ManulaScene");
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
