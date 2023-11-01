using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;
using UnityEngine.UI;

public class AudioOption : MonoBehaviour
{
    public AudioMixer gameBgmMixer;
    public Slider audioSlider;

    private void Start()
    {
        float middleValue = (audioSlider.maxValue + audioSlider.minValue) / 2;
        audioSlider.value = middleValue;
    }

    public void AudioControl()
    {
       float soundValue = audioSlider.value;

        //Apply mute effect when the sound value is -40f
        if (soundValue == -40f) gameBgmMixer.SetFloat("BGM", -80);
        else gameBgmMixer.SetFloat("BGM", soundValue);
    }

    public void ToggleAudioVolume()
    {
        AudioListener.volume = AudioListener.volume == 0 ? 1 : 0;
    }
}
