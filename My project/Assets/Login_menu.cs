using TMPro;
using UnityEngine;

public class Login_menu : MonoBehaviour
{
    
    [SerializeField] private ErrorManager errorManager;
    [SerializeField] private UIManager uiManager;

    public void LoginButton()
    {
        uiManager.ChangeWindows(WindowsApp.Loading);
        errorManager.UpdateTextError("");
    }
}
