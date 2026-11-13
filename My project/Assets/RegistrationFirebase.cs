using UnityEngine;
using TMPro;
using System.Collections;
using Firebase;
using Firebase.Auth;

public class RegistrationFirebase : MonoBehaviour
{
    
    [SerializeField] private TMP_InputField inputFieldName, inputFieldEmail, inputFieldPassword;
    [SerializeField] private ErrorManager errorManager;
    [SerializeField] private UIManager uiManager;

    public void RegisterButton()
    {
        if (string.IsNullOrWhiteSpace(inputFieldName.text))
        {
            errorManager.UpdateTextError("Введите имя игрока!");
            return;
        }

        errorManager.UpdateTextError("");
        StartCoroutine(RegisterPlayer(inputFieldName.text, inputFieldEmail.text, inputFieldPassword.text));
    }

    private IEnumerator RegisterPlayer(string name, string email, string password)
    {
        var registerTask = ConnectionFirebase.AuthorizationPlayer.CreateUserWithEmailAndPasswordAsync(email, password);
        yield return new WaitUntil(() => registerTask.IsCompleted);

        if (registerTask.Exception != null)
        {
            FirebaseException firebaseEx = registerTask.Exception.GetBaseException() as FirebaseException;
            if (firebaseEx != null)
            {
                errorManager.WhatErrorOut(firebaseEx);
            }
            else
            {
                errorManager.UpdateTextError("Неизвестная ошибка регистрации!");
            }
        }
        else
        {
            ConnectionFirebase.User = registerTask.Result.User;

            if (ConnectionFirebase.User != null)
            {
                UserProfile profile = new UserProfile { DisplayName = name };

                var profileTask = ConnectionFirebase.User.UpdateUserProfileAsync(profile);
                yield return new WaitUntil(() => profileTask.IsCompleted);

                if (profileTask.Exception != null)
                {
                    errorManager.UpdateTextError("Ошибка создания профиля!");
                }
                else
                {
                    uiManager.ChangeWindows(WindowsApp.Menu);
                    errorManager.UpdateTextError("");
                }
            }
        }
    }
}
