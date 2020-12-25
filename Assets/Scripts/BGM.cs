using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGM : MonoBehaviour
{
    public AudioSource bgm;
    public void play()
    {
        if (!bgm.isPlaying)
        {
            bgm.Play();
        }
    }

    public void pause()
    {
        if (!bgm.isPlaying)
        {
            bgm.Pause();
        }
    }
    public void changeVolume(float volume)
    {
        bgm.volume = volume;
    }
}
