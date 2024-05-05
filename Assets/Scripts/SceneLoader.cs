using Pdesuka.Manager;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    [SerializeField] private string _musicName;
    private void Start()
    {
        MusicManager.Instance.PlayMusic(_musicName);
    }
    public void LoadScene(int sceneIndex)
    {
        SoundManager.Instance.PlaySound("UI");
        SceneManager.LoadScene(sceneIndex);
    }
}
