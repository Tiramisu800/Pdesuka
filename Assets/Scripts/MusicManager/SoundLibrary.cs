using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Pdesuka.Manager
{
    public class SoundLibrary : MonoBehaviour
    {
        public SFX[] SFXs;

        public AudioClip GetClipFromName(string name)
        {
            foreach (var sfx in SFXs)
            {
                if (sfx.nameID == name)
                {
                    return sfx.clip;
                }
            }
            return null;
        }
    }


    [Serializable]
    public struct SFX
    {
        public string nameID;
        public AudioClip clip;
    }
}
