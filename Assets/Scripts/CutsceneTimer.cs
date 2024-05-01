using Pdesuka.Manager;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CutsceneTimer : MonoBehaviour
{
    [SerializeField] private float _time;
    [SerializeField] private int _sceneIndex;
    

    void Update()
    {
        _time -= Time.deltaTime;
        if (_time <= 0)
        {
            SceneManager.LoadScene(_sceneIndex);
        }
    }
}
