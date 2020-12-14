using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class AudioManager : MonoBehaviour
{
    public static readonly string FirstPlay = "FirstPlay";
    public static readonly string MusicPref = "MusicPref";
    public static readonly string SoundsPref = "SoundsPref";
    private int firstPlayInt;
    public Slider musicsSlider, soundsSlider;
    private float musicsFloat, soundsFloat;

    public AudioSource musicAudio;
    public AudioSource[] soundsAudio;
    void Start()
    {
        firstPlayInt = PlayerPrefs.GetInt(FirstPlay);

        if (firstPlayInt == 0)
        {
            musicsFloat = 0.75f;
            soundsFloat = 0.75f;
            musicsSlider.value = musicsFloat;
            soundsSlider.value = soundsFloat;
            PlayerPrefs.SetFloat(MusicPref, musicsFloat);
            PlayerPrefs.SetFloat(SoundsPref, soundsFloat);
            PlayerPrefs.SetInt(FirstPlay, -1);
        }
        else
        {
            musicsFloat = PlayerPrefs.GetFloat(MusicPref);
            musicsSlider.value = musicsFloat;
            soundsFloat = PlayerPrefs.GetFloat(SoundsPref);
            soundsSlider.value = soundsFloat;
        }
    }

    public void SaveSoundSettings()
    {
        PlayerPrefs.SetFloat(MusicPref, musicsSlider.value);
        PlayerPrefs.SetFloat(SoundsPref, soundsSlider.value);
    }

    private void OnApplicationFocus(bool focus)
    {
        if (!focus)
            SaveSoundSettings();
    }

    public void UpdateSounds()
    {
        musicAudio.volume = musicsSlider.value;

        for (int i = 0; i <soundsAudio.Length; i++)
        {
            soundsAudio[i].volume = soundsSlider.value;
        }
    }
}

