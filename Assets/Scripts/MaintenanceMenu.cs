using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
/**
* @author Marvin Rausch
*/
public class MaintenanceMenu : MonoBehaviour
{
    // set default value for developing purposes. So you are be able to run maintenance scene without loading main menu before
    public static string componentToMaintain = "text/pedal_maintenance"; 

    public Button pedalMaintenanceButton;
    public Button startButton;
    public TextMeshProUGUI introductionText;
    public GameObject navigationButton;
    public string pedalMaintenanceFileName = "text/pedal_maintenance";

    private void Start()
    {
        // define filename of instruction text for each button in setup maintenance scene
        pedalMaintenanceButton.onClick.AddListener(() => OnClick(pedalMaintenanceFileName));
    }

    /// <summary>
    /// is called when user choose the component to maintain
    /// </summary>
    /// <param name="fileName"></param> name and directory of JSON file where the maintenance instruction are saved
    private void OnClick(string fileName)
    {
        componentToMaintain = fileName;
        
        // load introduction text for maintenance
        introductionText.text = UIScript.LoadGuidelines(componentToMaintain).guidelines[0].descriptionText;
        
        // setup UI to display introduction text and start button
        navigationButton.SetActive(false);
        startButton.gameObject.SetActive(true);
        introductionText.gameObject.SetActive(true);
    }
    
    /// <summary>
    /// load maintenance scene
    /// </summary>
    public void StartMaintenance()
    {
        SceneManager.LoadScene("Maintenance");
    }
    
    
}