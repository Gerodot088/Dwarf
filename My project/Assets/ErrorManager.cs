using System.Collections;
using UnityEngine;
using TMPro;
using Firebase.Auth;
using Firebase;

public class ErrorManager : MonoBehaviour
{
    [Header("Настройки плашки ошибки")]
    [SerializeField] private TMP_Text errorText;
    [SerializeField] private CanvasGroup messageBox; 
    [SerializeField] private float showDuration = 3f; 

    private Coroutine hideCoroutine;

    private void Start()
    {
        
        if (messageBox != null)
        {
            messageBox.alpha = 0f;
            messageBox.blocksRaycasts = false;
        }
    }

    public void UpdateTextError(string text)
    {
        if (errorText == null) return;

       
        if (string.IsNullOrWhiteSpace(text))
        {
            HideImmediate();
            return;
        }

        errorText.text = text;

        
        if (hideCoroutine != null) StopCoroutine(hideCoroutine);
        hideCoroutine = StartCoroutine(ShowAndHideRoutine());
    }

    private IEnumerator ShowAndHideRoutine()
    {
        if (messageBox != null)
        {
            messageBox.blocksRaycasts = false; 

            
            float t = 0f;
            while (t < 1f)
            {
                t += Time.deltaTime * 5f;
                messageBox.alpha = Mathf.Lerp(0f, 1f, t);
                yield return null;
            }
            messageBox.alpha = 1f;
        }

        
        yield return new WaitForSeconds(showDuration);

        if (messageBox != null)
        {
            
            float t = 0f;
            while (t < 1f)
            {
                t += Time.deltaTime * 3f;
                messageBox.alpha = Mathf.Lerp(1f, 0f, t);
                yield return null;
            }
            messageBox.alpha = 0f;
        }
    }

    private void HideImmediate()
    {
        if (hideCoroutine != null) StopCoroutine(hideCoroutine);
        if (messageBox != null)
        {
            messageBox.alpha = 0f;
            messageBox.blocksRaycasts = false;
        }
    }

    public void WhatErrorOut(FirebaseException firebaseException)
    {
        AuthError errorCode = (AuthError)firebaseException.ErrorCode;
        string message = "Ошибка авторизации!";

        switch (errorCode)
        {
            case AuthError.None:
                message = "Ошибок нет.";
                break;
            case AuthError.Failure:
                message = "Сбой операции. Попробуйте позже!";
                break;
            case AuthError.InvalidCredential:
                message = "Неверная почта или пароль!";
                break;
            case AuthError.UserDisabled:
                message = "Этот аккаунт заблокирован!";
                break;
            case AuthError.AccountExistsWithDifferentCredentials:
                message = "Аккаунт уже привязан к другому способу входа!";
                break;
            case AuthError.OperationNotAllowed:
                message = "Этот способ входа отключен в настройках сервера!";
                break;
            case AuthError.EmailAlreadyInUse:
                message = "Эта почта уже зарегистрирована!";
                break;
            case AuthError.RequiresRecentLogin:
                message = "Для этого действия нужно перезайти в аккаунт!";
                break;
            case AuthError.CredentialAlreadyInUse:
                message = "Эти данные уже привязаны к другому аккаунту!";
                break;
            case AuthError.InvalidEmail:
                message = "Некорректный формат электронной почты!";
                break;
            case AuthError.MissingEmail:
                message = "Введите адрес электронной почты!";
                break;
            case AuthError.MissingPassword:
                message = "Введите пароль!";
                break;
            case AuthError.WeakPassword:
                message = "Слишком слабый пароль (минимум 6 символов)!";
                break;
            case AuthError.WrongPassword:
                message = "Неправильный пароль!";
                break;
            case AuthError.TooManyRequests:
                message = "Слишком много попыток! Попробуйте позже!";
                break;
            case AuthError.UserNotFound:
                message = "Пользователь с такой почтой не найден!";
                break;
            case AuthError.ProviderAlreadyLinked:
                message = "Этот способ входа уже привязан!";
                break;
            case AuthError.NoSuchProvider:
                message = "Провайдер авторизации не найден!";
                break;
            case AuthError.InvalidUserToken:
            case AuthError.UserTokenExpired:
                message = "Сессия устарела. Войдите в аккаунт заново!";
                break;
            case AuthError.NetworkRequestFailed:
                message = "Ошибка сети! Проверьте подключение к интернету.";
                break;
            case AuthError.InvalidApiKey:
                message = "Ошибка конфигурации приложения!";
                break;
            case AuthError.RejectedCredential:
                message = "Учетные данные отклонены!";
                break;
            case AuthError.PhoneNumberNotFound:
                message = "Номер телефона не найден!";
                break;
            case AuthError.MissingClientIdentifier:
                message = "Отсутствует идентификатор клиента!";
                break;
            case AuthError.MissingMultiFactorSession:
                message = "Требуется двухфакторная аутентификация!";
                break;
            default:
                message = $"Неизвестная ошибка ({errorCode})";
                break;
        }

        UpdateTextError(message);
    }
}
