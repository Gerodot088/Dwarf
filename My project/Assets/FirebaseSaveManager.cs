using System;
using UnityEngine;
using Firebase.Database;
using Firebase.Extensions;

[Serializable]
public class UserData
{
    public float brightness = 1f;
    public float fonMusicVolume = 1f;
    public float gameMusicVolume = 1f;
    public int unlockedLevel = 1;
}

public class FirebaseSaveManager : MonoBehaviour
{
    public static FirebaseSaveManager Instance;
    public UserData currentData = new UserData();

    private DatabaseReference dbReference;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        dbReference = FirebaseDatabase.GetInstance("https://firebaseio.com").RootReference;
    }

    public void LoadDataFromCloud(Action onLoaded = null)
    {
        if (ConnectionFirebase.User == null) return;
        string userId = ConnectionFirebase.User.UserId;

        dbReference.Child("users").Child(userId).GetValueAsync().ContinueWithOnMainThread(task =>
        {
            if (task.IsFaulted)
            {
                Debug.LogError("Ошибка загрузки: " + task.Exception);
                onLoaded?.Invoke();
                return;
            }

            DataSnapshot snapshot = task.Result;
            if (snapshot.Exists && !string.IsNullOrEmpty(snapshot.GetRawJsonValue()))
            {
                currentData = JsonUtility.FromJson<UserData>(snapshot.GetRawJsonValue());
            }
            else
            {
                currentData = new UserData();
                SaveDataToCloud();
            }

            onLoaded?.Invoke();
        });
    }

    public void SaveDataToCloud()
    {
        if (ConnectionFirebase.User == null) return;
        string userId = ConnectionFirebase.User.UserId;

        string json = JsonUtility.ToJson(currentData);
        dbReference.Child("users").Child(userId).SetRawJsonValueAsync(json);
    }
}
