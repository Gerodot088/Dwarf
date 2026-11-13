using UnityEngine;


public enum WindowsApp
{
    Login = 0,
    Registration = 1,
    Menu = 2
}

public class UIManager : MonoBehaviour
{
    [SerializeField] private GameObject[] windows;

    
    public void ChangeWindows(int window)
    {
        for (int i = 0; i < windows.Length; i++)
        {
            if (windows[i] != null)
            {
                windows[i].SetActive(false);
            }
        }

        if (window >= 0 && window < windows.Length && windows[window] != null)
        {
            windows[window].SetActive(true);
        }
    }

    
    public void ChangeWindows(WindowsApp window)
    {
        ChangeWindows((int)window);
    }
}
