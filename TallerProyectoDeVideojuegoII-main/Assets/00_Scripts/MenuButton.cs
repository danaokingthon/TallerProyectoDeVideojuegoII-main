using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuButton : MonoBehaviour
{
    public string menuSceneName = "MENU";

    public void GoToMenu()
    {
        SceneManager.LoadScene(menuSceneName);
    }
}
