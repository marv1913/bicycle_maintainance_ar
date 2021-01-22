using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Vuforia;
/**
* @author Marvin Rausch
*/
public class PauseMenu : MonoBehaviour
{
    public static bool GameIsPaused = false;

    public GameObject pauseMenuUI;
    
    /**
     * resume App
     */
    public void Resume()
    {
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        VuforiaRenderer.Instance.Pause(false);
        GameIsPaused = false;
    }
    
    /// <summary>
    /// pause UI and camera
    /// </summary>
    public void Pause()
    {
        VuforiaRenderer.Instance.Pause(true);
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        GameIsPaused = true;
    }
    
    /// <summary>
    /// go back to main menu
    /// </summary>
    public void LoadMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("MainMenu");
    }
    
    /// <summary>
    /// close app
    /// </summary>
    public void QuitGame()
    {
        Application.Quit();
    }
    
}
