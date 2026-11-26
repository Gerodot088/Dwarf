using TMPro;
using UnityEngine;

public class Settings_menu : MonoBehaviour
{

    [SerializeField] private ErrorManager errorManager;
    [SerializeField] private UIManager uiManager;

    public void SettingsButton()
    {
        uiManager.ChangeWindows(WindowsApp.Settings);
        errorManager.UpdateTextError("");
    }
}
