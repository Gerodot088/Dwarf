using TMPro;
using UnityEngine;

public class Perehod_v_menu : MonoBehaviour
{

    [SerializeField] private ErrorManager errorManager;
    [SerializeField] private UIManager uiManager;

    public void MenuButton()
    {
        uiManager.ChangeWindows(WindowsApp.Menu);
        errorManager.UpdateTextError("");
    }
}
