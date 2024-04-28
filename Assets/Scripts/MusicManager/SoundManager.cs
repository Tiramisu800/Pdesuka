using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace Pdesuka.Manager
{
    public class SoundManager : MonoBehaviour
    {
        public static SoundManager Instance;

        [SerializeField] private SoundLibrary sfxLibrary;
        [SerializeField] private AudioSource sfxSource;

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

        public void PlaySound(string soundName)
        {
            var clip = sfxLibrary.GetClipFromName(soundName);
            sfxSource.PlayOneShot(clip);
        }

    }


}
