using UnityEngine;
using Firebase.Auth;
using Firebase;


public class ConnectionFirebase : MonoBehaviour
{
    public static FirebaseAuth AuthhorizationPlayer;
    public static FirebaseUser User;
    [SerializeField] private ErrorManager errorManager;
    
}
