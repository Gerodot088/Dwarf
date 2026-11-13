using UnityEngine;
using Firebase.Auth;
using Firebase;


public class ConnectionFirebase : MonoBehaviour
{
    public static FirebaseAuth AuthorizationPlayer;
    public static FirebaseUser User;
    [SerializeField] private ErrorManager errorManager;

    private void Awake()
    {
        FirebaseApp.CheckAndFixDependenciesAsync().ContinueWith(task =>
        {
            DependencyStatus dependencyStatus = task.Result;
            if (dependencyStatus == DependencyStatus.Available)
            {
                AuthorizationPlayer = FirebaseAuth.DefaultInstance;
            }
            else
            {
                errorManager.UpdateTextError("Не удалось разрешить все зависимости Firebase: " + dependencyStatus);
            }
        });
    }

}
