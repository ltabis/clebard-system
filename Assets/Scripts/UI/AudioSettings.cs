using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioSettings : MonoBehaviour
{
    private static readonly string MusicPref = "MusicPref";
    private static readonly string SoundsPref = "SoundsPref";
    private float musicsFloat, soundsFloat;

    public AudioSource musicAudio;
    public AudioSource[] soundsAudio;

    private void Awake()
    {
        ContinueSettings();
    }

    private void ContinueSettings()
    {
        musicsFloat = PlayerPrefs.GetFloat(MusicPref);
        soundsFloat = PlayerPrefs.GetFloat(SoundsPref);

        musicAudio.volume = musicsFloat;

        for (int i = 0; i < soundsAudio.Length; i++)
        {
            soundsAudio[i].volume = soundsFloat;
        }
    }
}
