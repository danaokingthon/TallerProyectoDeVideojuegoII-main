using UnityEngine;

public class PORTAL : MonoBehaviour
{
    public string sceneToLoad;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player))
        {
            Time.timeScale = 1f;
            SceneManager.LoadScene(sceneToLoad);
        }
    }
}
