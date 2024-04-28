using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Pdesuka.Manager
{
    public class MusicLibrary : MonoBehaviour
    {
        public Music[] tracks;

        public AudioClip GetClipFromName(string trackName)
        {
            foreach (var track in tracks)
            {
                if (track.nameID == trackName)
                {
                    return track.clip;
                }
            }
            return null;
        }
        
    }

    [Serializable]
    public struct Music
    {
        public string nameID;
        public AudioClip clip;
    }
}
