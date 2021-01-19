using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
/**
* @author Marvin Rausch
*/
public class MaintenanceMenu : MonoBehaviour
{
    public static string componentToMaintain = "text/pedal_maintenance";

    public Button pedalMaintenanceButton;
    public Button startButton;
    public TextMeshProUGUI introductionText;
    public GameObject navigationButton;
    public string pedalMaintenanceFileName = "text/pedal_maintenance";

    private void Start()
    {
        pedalMaintenanceButton.onClick.AddListener(() => OnClick(pedalMaintenanceFileName));
    }

    private void OnClick(string fileName)
    {
        componentToMaintain = fileName;
        introductionText.text = UIScript.LoadGuidelines(componentToMaintain).guidelines[0].descriptionText;
        navigationButton.SetActive(false);
        startButton.gameObject.SetActive(true);
        introductionText.gameObject.SetActive(true);
    }
    
    public void StartMaintenance()
    {
        SceneManager.LoadScene("Maintenance");
    }
    
    
}