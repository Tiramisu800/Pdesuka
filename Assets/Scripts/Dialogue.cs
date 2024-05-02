using Pdesuka.Manager;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Dialogue : MonoBehaviour
{
    [SerializeField] private Animator _playerAnim;
    [SerializeField] private Animator _friendAnim;

    [SerializeField] private GameObject _gameOver;
    [SerializeField] private TMP_Text _gameOverText;
    [SerializeField] private Button _gameOverRestart;
    [SerializeField] private Button _gameOverGiveUp;

    [SerializeField] private GameObject _Fdialogue2;
    [SerializeField] private GameObject _Fdialogue3;
    [SerializeField] private GameObject _Fdialogue4;
    [SerializeField] private GameObject _Fdialogue5;
    [SerializeField] private GameObject _Fdialogue6;
    [SerializeField] private GameObject _Fdialogue7;
    [SerializeField] private GameObject _Fdialogue8;

    [SerializeField] private GameObject _Pdialogue1;
    [SerializeField] private GameObject _Pdialogue2;
    [SerializeField] private GameObject _Pdialogue3;
    [SerializeField] private GameObject _Pdialogue4;
    [SerializeField] private GameObject _Pdialogue5;
    [SerializeField] private GameObject _Pdialogue6;
    [SerializeField] private GameObject _Pdialogue7;

    [SerializeField] private Button _1PbuttonOption1;
    [SerializeField] private Button _1PbuttonOption2;
    [SerializeField] private Button _2PbuttonOption1;
    [SerializeField] private Button _2PbuttonOption2;
    [SerializeField] private Button _3PbuttonOption1;
    [SerializeField] private Button _3PbuttonOption2;
    [SerializeField] private Button _4PbuttonOption1;
    [SerializeField] private Button _4PbuttonOption2;
    [SerializeField] private Button _5PbuttonOption1;
    [SerializeField] private Button _5PbuttonOption2;
    [SerializeField] private Button _6PbuttonOption1;
    [SerializeField] private Button _6PbuttonOption2;
    [SerializeField] private Button _7Pbutton;

    [SerializeField] private GameObject _whatSign;
    [SerializeField] private GameObject _what2Sign;

    private void OnEnable()
    {
        _gameOverRestart.onClick.AddListener(RestartScene);
        _gameOverGiveUp.onClick.AddListener(GiveUp);

        _1PbuttonOption1.onClick.AddListener( () =>{ GameOver(1); });
        _1PbuttonOption2.onClick.AddListener( () =>{ ShowDialogue(1); });
        _2PbuttonOption1.onClick.AddListener(() => { GameOver(1); });
        _2PbuttonOption2.onClick.AddListener(() => { ShowDialogue(2); });
        _3PbuttonOption1.onClick.AddListener(() => { ShowDialogue(3); });
        _3PbuttonOption2.onClick.AddListener(() => { GameOver(1); });
        _4PbuttonOption1.onClick.AddListener(() => { GameOver(2); });
        _4PbuttonOption2.onClick.AddListener(() => { ShowDialogue(4); });
        _5PbuttonOption1.onClick.AddListener(() => { ShowDialogue(5); });
        _5PbuttonOption2.onClick.AddListener(() => { GameOver(2); });
        _6PbuttonOption1.onClick.AddListener(() => { GameOver(2); });
        _6PbuttonOption2.onClick.AddListener(() => { ShowDialogue(6); });
        _7Pbutton.onClick.AddListener(() => { ShowDialogue(7); });
    }

    private void GameOver(int num)
    {
        _Pdialogue1.SetActive(false);
        _Pdialogue2.SetActive(false);
        _Pdialogue3.SetActive(false);
        _Pdialogue4.SetActive(false);
        _Pdialogue5.SetActive(false);
        _Pdialogue6.SetActive(false);
        _gameOver.SetActive(true);
        MusicManager.Instance.PlayMusic("Lose");
        if (num == 1)
        {
            _gameOverText.text = "*sigh* the hopeless idiot";
        }
        if (num == 2)
        {
            _gameOverText.text = "No, you will be with me forever";
        }
    }
    public void RestartScene()
    {
        int i = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(i);
    }
    public void GiveUp()
    {
        SceneManager.LoadScene(28);
        AdManager.Instance.ShowRewardedAd();
    }
    private void ShowDialogue(int num)
    {
        StartCoroutine(ShowDialogueE(num));
    }

    IEnumerator ShowDialogueE(int n)
    {
        if (n == 1)
        {
            _playerAnim.SetTrigger("A1");
            SoundManager.Instance.PlaySound("Chat");
            yield return new WaitForSeconds(1f);
            _Pdialogue1.SetActive(false);
            _Fdialogue2.SetActive(true);
            SoundManager.Instance.PlaySound("Chat");

        }
        if (n == 2)
        {
            _playerAnim.SetTrigger("A3");
            SoundManager.Instance.PlaySound("Chat");
            yield return new WaitForSeconds(1f);
            _Pdialogue2.SetActive(false);
            _Fdialogue3.SetActive(true);
            SoundManager.Instance.PlaySound("Chat");
        }
        if (n == 3)
        {
            _playerAnim.SetTrigger("A2");
            SoundManager.Instance.PlaySound("Chat");
            yield return new WaitForSeconds(1f);
            _Pdialogue3.SetActive(false);
            _Fdialogue4.SetActive(true);
            _whatSign.SetActive(true);
            SoundManager.Instance.PlaySound("Chat");

        }
        if (n == 4)
        {
            _playerAnim.SetTrigger("A3");
            SoundManager.Instance.PlaySound("Chat");
            yield return new WaitForSeconds(1f);
            _Pdialogue4.SetActive(false);
            _Fdialogue5.SetActive(true);
            SoundManager.Instance.PlaySound("Chat");
        }
        if (n == 5)
        {
            _what2Sign.SetActive(true);
            SoundManager.Instance.PlaySound("Chat");
            yield return new WaitForSeconds(1f);
            _Pdialogue5.SetActive(false);
            SoundManager.Instance.PlaySound("Chat");
            _what2Sign.SetActive(false);
            _Fdialogue6.SetActive(true);
            _friendAnim.SetTrigger("Hide");
        }
        if (n == 6)
        {
            _playerAnim.SetTrigger("Sorry");
            SoundManager.Instance.PlaySound("Chat");
            yield return new WaitForSeconds(1f);
            _Pdialogue6.SetActive(false);
            SoundManager.Instance.PlaySound("Chat");
            MusicManager.Instance.PlayMusic("PreBoss_Cutscene");
            _Fdialogue7.SetActive(true);
            _friendAnim.SetTrigger("HideOut");
        }
        if (n == 7)
        {
            _playerAnim.SetTrigger("Peace");
            SoundManager.Instance.PlaySound("Chat");
            yield return new WaitForSeconds(1f);
            _Pdialogue7.SetActive(false);
            _friendAnim.SetTrigger("Jump");
            SoundManager.Instance.PlaySound("Chat");
            _Fdialogue8.SetActive(true);
        }
        yield return null;
    }
}
