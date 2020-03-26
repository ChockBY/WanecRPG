using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour {


    private static AudioManager instance;
    public static AudioManager Intance
    {
        get {

            if (instance == null)
            {
                instance = new GameObject("AudioManager").AddComponent<AudioManager>();

            }

            return instance;

        }






    }
    private AudioSource bgAudio;
    private AudioSource soundAudio;

    private AudioClip[] bgClips;
    private AudioClip[] soundClips;


    private void Awake()
    {
        bgAudio = gameObject.AddComponent<AudioSource>();
        soundAudio = gameObject.AddComponent<AudioSource>();

        bgClips = Resources.LoadAll<AudioClip>("Music");
        soundClips = Resources.LoadAll<AudioClip>("Music");

    }
    public void PlayBgAudio(string name)
    {
        for (int i = 0; i < bgClips.Length; i++)
        {
            if(name== bgClips[i].name)
            {
                bgAudio.clip = bgClips[i];
                bgAudio.Play();
                break;

            }
        }

    }
    public void PlaySoundAudio(string name)
    {
        for (int i = 0; i < soundClips.Length; i++)
        {
            if (name == soundClips[i].name)
            {
                soundAudio.clip = soundClips[i];
                soundAudio.Play();
                break;

            }
        }

    }


}
