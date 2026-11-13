using UnityEngine;
using TMPro;

public class Menu : MonoBehaviour
{
    [SerializeField] private TMP_Text nick;

    private void OnEnable()
    {
        LoadProfile();
    }

    private void LoadProfile()
    {
        if (ConnectionFirebase.User == null) return;

        
        nick.text = string.IsNullOrEmpty(ConnectionFirebase.User.DisplayName) ? "Игрок" : ConnectionFirebase.User.DisplayName;
    }
}
