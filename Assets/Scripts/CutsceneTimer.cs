using Pdesuka.Manager;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CutsceneTimer : MonoBehaviour
{
    [SerializeField] private string _musicName;
    [SerializeField] private float _time;
    [SerializeField] private int _sceneIndex;
    
    void Start()
    {
        MusicManager.Instance.PlayMusic(_musicName);
    }

    void Update()
    {
        _time -= Time.deltaTime;
        if (_time <= 0)
        {
            SceneManager.LoadScene(_sceneIndex);
        }
    }
}
