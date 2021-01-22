using UnityEngine;
using UnityEngine.SceneManagement;
/**
* @author Marvin Rausch
*/
public class MainMenu : MonoBehaviour
{
    /// <summary>
    /// start the discover mode
    /// </summary>
    public void StartDiscoverMode()
    {
        // Load next level
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    /// <summary>
    /// close the application
    /// </summary>
    public void QuitGame()
    {
        Application.Quit();
    }
}
