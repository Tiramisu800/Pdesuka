using System.Collections;
using System.Collections.Generic;
using System.IO;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.TextCore.Text;
using Pdesuka.Data;
using UnityEngine.UI;
using Firebase.Database;
using static Pdesuka.Data.DataController;
using System;

namespace Pdesuka.Manager
{
    public class MenuManager : MonoBehaviour
    {
        public static MenuManager Instance;

        [SerializeField] private TMP_InputField _inputName;
        public string _userName;
        public bool isContinue = false;

        private void Awake()
        {
            if(Instance == null)
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
            MusicManager.Instance.PlayMusic("Menu");
        }
        private void OnEnable()
        {
            DataController.Instance.LoadedData += LoadedData;
        }

        //Start Game
        public void SetUserName()
        {
            SoundManager.Instance.PlaySound("UI");
            _userName = _inputName.text;
        }
        public void GoNextScene()
        {
            SceneManager.LoadScene(27);
        }
  
        //Continue
        public void LoadData()
        {
            isContinue = true;
            SoundManager.Instance.PlaySound("UI");
            DataController.Instance.LoadData(_userName);
            Debug.Log("Data Loaded");
        }

        private void LoadedData(DataController.DataToSave loadedData)
        {
            Debug.Log(loadedData.UserName);
            if (loadedData.TimeScore == 0)
            {
                SceneManager.LoadScene(loadedData.CurrentLevelIndex);
            }
            else
            {
                SceneManager.LoadScene(loadedData.CurrentLevelIndex + 1);
            }
            
        }

    }

}
