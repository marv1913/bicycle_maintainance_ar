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

    public string pedalMaintenanceFileName = "text/pedal_maintenance";

    private void Start()
    {
        pedalMaintenanceButton.onClick.AddListener(() => OnClick(pedalMaintenanceFileName));
    }

    private static void OnClick(string fileName)
    {
        componentToMaintain = fileName;
        SceneManager.LoadScene("Maintenance");
    }
    
    
    
}
