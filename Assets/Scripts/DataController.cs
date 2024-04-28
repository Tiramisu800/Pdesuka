using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Firebase.Database;
using Firebase.Extensions;
using UnityEngine.SocialPlatforms.Impl;
using System.Threading.Tasks;
using Unity.VisualScripting;
using UnityEngine.SocialPlatforms;

namespace Pdesuka.Data
{
    public class DataController : MonoBehaviour
    {
        public static event Action<DataToSave> OnDataLoaded;

        static DatabaseReference _databaseReference;
        //public static DataToSave _playerData;

        //public static List<DataToSave> playerDatas = new List<DataToSave>();


        private void Awake()
        {
            _databaseReference = FirebaseDatabase.DefaultInstance.RootReference;
        }
        
        /*
        public static void SaveData(string userName, int sceneIndex, float timeScore)
        {
            string json = JsonUtility.ToJson(new DataToSave(userName, sceneIndex, timeScore));
            _databaseReference.Child("users").Child(userName).SetRawJsonValueAsync(json);
        }
        */

        public static void SaveData(string userName, int sceneIndex, float timeScore)
        {
            FirebaseDatabase.DefaultInstance.RootReference.Child("users").GetValueAsync().ContinueWithOnMainThread(task =>
            {
                if (task.IsFaulted)
                {
                    Debug.LogError("Failed to load data from Firebase.");
                }
                else if (task.IsCompleted)
                {
                    DataSnapshot snapshot = task.Result;

                    Debug.Log(snapshot.GetRawJsonValue());

                    var playerData = JsonUtility.FromJson<DataToSave>(snapshot.GetRawJsonValue());

                    _databaseReference.Child("users").Child(userName).SetRawJsonValueAsync(JsonUtility.ToJson(new DataToSave(userName, sceneIndex, timeScore)));
                }
            });
        }

        /*
        public static void SaveData(string userName, int sceneIndex, float timeScore)
        {
            var serverData = _databaseReference.Child("users").Child(userName).GetValueAsync();
            DataSnapshot snapshot = serverData.Result;
            foreach (var child in snapshot.Children)
            {
                var playerData = JsonUtility.FromJson<DataToSave>(child.GetRawJsonValue());

                if (!playerDatas.Contains(playerData))
                {
                    playerDatas.Add(playerData);
                }

                if (playerData.UserName == userName)
                {
                    if (playerData.CurrentLevelIndex == sceneIndex)
                    {
                        if (playerData.TimeScore < timeScore)
                        {
                            FirebaseDatabase.DefaultInstance.RootReference.Child("users").Child(userName).SetRawJsonValueAsync(JsonUtility.ToJson(new DataToSave(userName, sceneIndex, timeScore)));
                        }
                    }     
                }
                else
                {
                    FirebaseDatabase.DefaultInstance.RootReference.Child("users").Child(userName).SetRawJsonValueAsync(JsonUtility.ToJson(new DataToSave(userName, sceneIndex, timeScore)));
                }
            }
        }
        */

        public void LoadData(string userName)
        {
            StartCoroutine(LoadDataE(userName));
        }

        IEnumerator LoadDataE(string userName)
        {
            var serverData = _databaseReference.Child("users").Child(userName).GetValueAsync();
            yield return new WaitUntil(predicate: () => serverData.IsCompleted);

            DataSnapshot snapshot = serverData.Result;
            string jsonData = snapshot.GetRawJsonValue();

            if (jsonData != null)
            {
                var playerData = JsonUtility.FromJson<DataToSave>(jsonData);

                OnDataLoaded?.Invoke(playerData);
            }
            else
            {
                Debug.Log("No data");
            }

        }
        

        /*
        public void LoadData(string userName)
        {
            Task.Run(async () => await FirebaseDatabase.DefaultInstance.RootReference.Child("users").Child(userName).GetValueAsync().ContinueWith(OnData));
        }
        private void OnData(Task<DataSnapshot> task)
        {
            DataSnapshot snapshot = task.Result;
            string jsonData = snapshot.GetRawJsonValue();

            _playerData = JsonUtility.FromJson<DataToSave>(jsonData);

            OnDataLoaded?.Invoke(_playerData);
        }
        */
    }



    [Serializable]
    public class DataToSave
    {
        public string UserName;
        public int CurrentLevelIndex;
        public float TimeScore;

        public DataToSave(string userName, int currentLevelIndex, float timeScore)
        {
            UserName = userName;
            CurrentLevelIndex = currentLevelIndex;
            TimeScore = timeScore;
        }
    }

}