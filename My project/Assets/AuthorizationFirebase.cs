using System.Collections;
using Firebase;
using Firebase.Auth;
using TMPro;
using UnityEngine;

public class AuthorizationFirebase : MonoBehaviour
{
    [SerializeField] private TMP_InputField inputFieldEmail, inputFieldPassword;
    [SerializeField] private ErrorManager errorManager;
    [SerializeField] private UIManager uiManager;
    [SerializeField] private SettingsController settingsController; // Сюда перетащите Panel_Settings

    public void LoginButton()
    {
        errorManager.UpdateTextError("");
        StartCoroutine(SignIn(inputFieldEmail.text, inputFieldPassword.text));
    }

    private IEnumerator SignIn(string email, string password)
    {
        var loginTask = ConnectionFirebase.AuthorizationPlayer.SignInWithEmailAndPasswordAsync(email, password);
        yield return new WaitUntil(() => loginTask.IsCompleted);

        if (loginTask.Exception != null)
        {
            FirebaseException firebaseEx = loginTask.Exception.GetBaseException() as FirebaseException;
            if (firebaseEx != null)
            {
                errorManager.WhatErrorOut(firebaseEx);
            }
            else
            {
                errorManager.UpdateTextError("Ошибка входа!");
            }
        }
        else
        {
            ConnectionFirebase.User = loginTask.Result.User;
            errorManager.UpdateTextError("");

            // Загружаем настройки из Firebase и применяем их перед открытием меню
            if (FirebaseSaveManager.Instance != null)
            {
                FirebaseSaveManager.Instance.LoadDataFromCloud(() =>
                {
                    if (settingsController != null)
                    {
                        settingsController.ApplyAllSettings();
                    }
                    uiManager.ChangeWindows(WindowsApp.Menu);
                });
            }
            else
            {
                uiManager.ChangeWindows(WindowsApp.Menu);
            }
        }
    }
}
