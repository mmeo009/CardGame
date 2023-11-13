using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Util;

public class SoundData : GenericSingleton<SoundData>
{
    public AudioSource audio;
    public void PlaySound(string name)
    {
        if(audio != null)
        {
            audio = GetOrAddComponent<AudioSource>(gameObject);
        }

        AudioClip clip = Managers.Data.soundDictionary[name];
        if(clip != null)
        {
            audio.clip = clip;
            audio.Play();
        }
    }
}
