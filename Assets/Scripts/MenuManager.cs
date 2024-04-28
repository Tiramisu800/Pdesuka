using System.Collections;
using System.Collections.Generic;
using System.IO;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.TextCore.Text;
using Pdesuka.Data;
using UnityEngine.UI;

namespace Pdesuka.Manager
{
    public class MenuManager : MonoBehaviour
    {
        public static MenuManager Instance;

        public TMP_InputField _inputName;
        public string _userName;

        [SerializeField] private Button _playNewGame;
        [SerializeField] private Button _continueGame;

        private DataController _dataController;

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
        public void OnEnable()
        {
            DataController.OnDataLoaded += UsePlayerData;
            _playNewGame.onClick.AddListener(SetUserName);
            _continueGame.onClick.AddListener(LoadPlayerData);
        }
        public void SetUserName()
        {
            SoundManager.Instance.PlaySound("UI");
            _userName = _inputName.text;
            SceneManager.LoadScene(27);
        }
  
        public void LoadPlayerData()
        {
            SoundManager.Instance.PlaySound("UI");
            _dataController.LoadData(_inputName.text);
            
        }

        public void UsePlayerData(DataToSave data)
        {
            SceneManager.LoadScene(data.CurrentLevelIndex);
        }
        
    }

}
