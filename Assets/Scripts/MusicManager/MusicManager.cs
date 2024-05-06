using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Pdesuka.Manager
{
    public class MusicManager : MonoBehaviour
    {
        public static MusicManager Instance;

        [SerializeField] private MusicLibrary musicLibrary;
        [SerializeField] private AudioSource musicSource;
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
        public void PlayMusic(string trackName, float fadeDuration = 0.5f)
        {
            var clip = musicLibrary.GetClipFromName(trackName);
            StartCoroutine(MusicCrossfade(clip, fadeDuration));
        }
        IEnumerator MusicCrossfade(AudioClip nextTrack, float fadeDuration = 0.5f)
        {
            float percent = 0;
            while (percent < 1)
            {
                percent += Time.deltaTime * 1 / fadeDuration;
                musicSource.volume = Mathf.Lerp(1f, 0, percent);
                yield return null;
            }

            musicSource.clip = nextTrack;
            musicSource.Play();

            percent = 0;
            while (percent < 1)
            {
                percent += Time.deltaTime * 1 / fadeDuration;
                musicSource.volume = Mathf.Lerp(0, 1f,percent);
                yield return null;
            }
        }


    }

}
