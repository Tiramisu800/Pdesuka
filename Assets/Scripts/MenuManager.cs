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
        [SerializeField] private Button _play;
        [SerializeField] private Button _continue;

        [SerializeField] private TMP_InputField _inputName;
        private string _userName;

        private void Start()
        {
            MusicManager.Instance.PlayMusic("Menu");
        }
        private void OnEnable()
        {
            DataController.Instance.LoadedData += LoadedData;
            _play.onClick.AddListener(() => { SetUserName();
                GoNextScene();
            });
            _continue.onClick.AddListener(() =>
            {
                SetUserName();
                LoadData();
            });
        }

        //Start Game
        public void SetUserName()
        {
            SoundManager.Instance.PlaySound("UI");
            if (_inputName.text == string.Empty)
            {
                PlayerPrefs.SetString("Username", "guest");
            }
            else
            {
                PlayerPrefs.SetString("Username", _inputName.text);
            }
            PlayerPrefs.Save();

            if (PlayerPrefs.HasKey("Username"))
            {
                Debug.Log(PlayerPrefs.GetString("Username"));
            }

            _userName = PlayerPrefs.GetString("Username");
        }
        public void GoNextScene()
        {
            SceneManager.LoadScene(27);
        }
  
        //Continue
        public void LoadData()
        {
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
