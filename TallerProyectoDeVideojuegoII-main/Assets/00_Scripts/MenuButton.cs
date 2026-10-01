using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuButton : MonoBehaviour
{
    public string menuSceneName = "Menu";

    public void GoToMenu()
    {
        SceneManager.LoadScene(menuSceneName);
    }
}
