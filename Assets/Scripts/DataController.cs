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
using Pdesuka.Manager;

namespace Pdesuka.Data
{
    public class DataController : MonoBehaviour
    {
        public static DataController Instance;

        public List<DataToSave> userManyData = new List<DataToSave>();

        public event Action<DataToSave> LoadedData;
        public event Action<List<DataToSave>> LoadedAllData;

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

        public void SaveData(string userName, string sceneIndex, float timeScore)
        {
            string json = JsonUtility.ToJson(new DataToSave(userName, int.Parse(sceneIndex), timeScore));
            FirebaseDatabase.DefaultInstance.RootReference.Child("users").Child(userName).Child(sceneIndex).SetRawJsonValueAsync(json);
        }
        /*
        public void SaveData(string userName, string sceneIndex, float timeScore)
        {
            StartCoroutine(SaveDataE(userName, sceneIndex, timeScore));
        }
        IEnumerator SaveDataE(string userName, string sceneIndex, float timeScore)
        {
            var serverData = FirebaseDatabase.DefaultInstance.RootReference.Child("users").Child(sceneIndex).Child(userName).GetValueAsync();
            yield return new WaitUntil(predicate: () => serverData.IsCompleted);

            DataSnapshot snapshot = serverData.Result;

            if (snapshot != null)
            {
                foreach (var child in snapshot.Children)
                {
                    var user = JsonUtility.FromJson<DataToSave>(child.GetRawJsonValue());

                    if (user.TimeScore < timeScore)
                    {
                        string json = JsonUtility.ToJson(new DataToSave(userName, int.Parse(sceneIndex), timeScore));
                        FirebaseDatabase.DefaultInstance.RootReference.Child("users").Child(sceneIndex).Child(userName).SetRawJsonValueAsync(json);
                    }
                    else
                    {
                        string json = JsonUtility.ToJson(new DataToSave(userName, int.Parse(sceneIndex), user.TimeScore));
                        FirebaseDatabase.DefaultInstance.RootReference.Child("users").Child(sceneIndex).Child(userName).SetRawJsonValueAsync(json);
                    }
                }
            }
            else
            {
                Debug.Log("No data, saving new data");
                string json = JsonUtility.ToJson(new DataToSave(userName, int.Parse(sceneIndex), timeScore));
                FirebaseDatabase.DefaultInstance.RootReference.Child("users").Child(sceneIndex).Child(userName).SetRawJsonValueAsync(json);
            }
        }
        */
        
        public void LoadData(string userName)
        {
            StartCoroutine(LoadDataE(userName));
        }

        IEnumerator LoadDataE(string userName)
        {
            var serverData = FirebaseDatabase.DefaultInstance.RootReference.Child("users").Child(userName).GetValueAsync();
            yield return new WaitUntil(predicate: () => serverData.IsCompleted);

            DataSnapshot snapshot = serverData.Result;

            if (snapshot != null)
            {
                foreach (var child in snapshot.Children)
                {
                    var user = JsonUtility.FromJson<DataToSave>(child.GetRawJsonValue());

                    if (!userManyData.Contains(user))
                    {
                        userManyData.Add(user);
                    }
                }

                if (MenuManager.Instance.isContinue == true)
                {
                    var index = userManyData.Count - 1;
                    LoadedData?.Invoke(userManyData[index]);
                    MenuManager.Instance.isContinue = false;
                }
                
                LoadedAllData?.Invoke(userManyData);
            }
            else
            {
                Debug.Log("No data");
            } 

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

}