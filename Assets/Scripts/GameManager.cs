using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using Pdesuka.Data;
using Pdesuka.Enemy;
using UnityEngine.SocialPlatforms;
using System;

namespace Pdesuka.Manager
{
    public class GameManager : MonoBehaviour
    {
        [SerializeField] private string _musicName;
        [SerializeField] private Player _player;
        [SerializeField] private GameObject _loseScreen;
        [SerializeField] private TMP_Text _causeDeathText;
        [SerializeField] private GameObject _winScreen;
        [SerializeField] private GameObject _pauseScreen;
        [SerializeField] private GameObject _finish;

        private Animator _anim;
        private bool isPaused = false;
        private float _timeScore = 0f;

        private void Start()
        {
            _anim = GetComponent<Animator>();

            MusicManager.Instance.PlayMusic(_musicName);
        }
        private void Update()
        {
            _timeScore += Time.deltaTime;
        }
        public void FadeToLevel()
        {
            _anim.SetTrigger("Fade");
        }

        private void OnEnable()
        {
            _player.OnKilled += PlayerOnKilled;
            TrapIce.OnKilledByIceSpike += PlayerBecomeKebab;
            TrapSink.OnKilledByColdWater += PlayerSinked;
            TrapElectro.OnKilledByElectro += Player1001volt;
            TrapBat.OnKilledByBat += PlayerBat;
            TrapRat.OnKilledByRat += PlayerRat;
            TrapSlime.OnKilledBySlime += PlayerSlimed;
        }

        private void OnDisable()
        {
            _player.OnKilled -= PlayerOnKilled;
            TrapIce.OnKilledByIceSpike -= PlayerBecomeKebab;
            TrapSink.OnKilledByColdWater -= PlayerSinked;
            TrapElectro.OnKilledByElectro -= Player1001volt;
            TrapBat.OnKilledByBat -= PlayerBat;
            TrapRat.OnKilledByRat -= PlayerRat;
            TrapSlime.OnKilledBySlime -= PlayerSlimed;
        }

        //End of level
        public void LevelComplete()
        {
            _winScreen.SetActive(true);
            _player.enabled = false;

            MusicManager.Instance.PlayMusic("Win");
            
            var username = MenuManager.Instance._userName;
            var i = SceneManager.GetActiveScene().buildIndex;
            var sceneIndex = i.ToString();
            var timescore = _timeScore;

            DataController.Instance.SaveData(username, sceneIndex, timescore);
        }

        //Load level
        public void LoadScene(int sceneNumber)
        {
            SoundManager.Instance.PlaySound("UI");
            SceneManager.LoadScene(sceneNumber);
            if (isPaused == true)
            {
                Time.timeScale = 1.0f;
                isPaused = false;
            }
        }

        //Quit level
        public void SaveQuit()
        {
            SoundManager.Instance.PlaySound("UI");
            AdManager.Instance.ShowRewardedAd();

            var username = MenuManager.Instance._userName;
            var i = SceneManager.GetActiveScene().buildIndex;
            var sceneIndex = i.ToString();
            var timescore = 0;
            DataController.Instance.SaveData(username, sceneIndex, timescore);

            SceneManager.LoadScene(0);
        }

        //Pause
        public void Pause()
        {
            _pauseScreen.SetActive(true);
            SoundManager.Instance.PlaySound("Pause");
            Time.timeScale = 0f;
            isPaused = true;
        }
        public void Continue()
        {
            _pauseScreen.SetActive(false);
            SoundManager.Instance.PlaySound("UI_Close");
            Time.timeScale = 1f;
            isPaused = false;
        }
        
        //Player killed

        private void PlayerOnKilled()
        {
            _finish.SetActive(false);
            _loseScreen.SetActive(true);
            MusicManager.Instance.PlayMusic("Lose");
        }

        // Killed Reasons:
        private void PlayerBecomeKebab()
        {
            _causeDeathText.text = "Become Iced Kebab";
        }
        private void PlayerSinked()
        {
            _causeDeathText.text = "Sinked";
        }
        private void Player1001volt()
        {
            _causeDeathText.text = "Tasted 1001 V";
        }
        private void PlayerBat()
        {
            _causeDeathText.text = "Punched by Bat";
        }
        private void PlayerRat()
        {
            _causeDeathText.text = "Infection by Rat";
        }
        private void PlayerSlimed()
        {
            _causeDeathText.text = "Very Slimed";
        }
    }

}
