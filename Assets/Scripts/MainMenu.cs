using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    /// <summary>
    /// start the discover mode
    /// </summary>
    public void StartDiscoverMode()
    {
        // Load next level
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        //SceneManager.LoadScene("Scenes/ComponentScene");
    }

    /// <summary>
    /// close the application
    /// </summary>
    public void QuitGame()
    {
        Application.Quit();
    }
}
