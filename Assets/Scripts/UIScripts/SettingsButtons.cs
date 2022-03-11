using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SettingsButtons : MonoBehaviour
{
    [SerializeField] private AudioMixerGroup mixer;
    [SerializeField] private Image effectButton;
    [SerializeField] private Image musicButton;

    private Boolean _effect = true;
    private Boolean _music = true;
    
    public void EffectButton()
    {
        if (_effect)
        {
            mixer.audioMixer.SetFloat("SoundsVolume", -80);
            _effect = false;
        }
        else
        {
            mixer.audioMixer.SetFloat("SoundsVolume", 0);
            _effect = true;
        }
    }
    
    public void MusicButton()
    {
        if (_music)
        {
            mixer.audioMixer.SetFloat("MusicVolume", -80);
            _music = false;
        }
        else
        {
            mixer.audioMixer.SetFloat("MusicVolume", 0);
            _music = true;
        }
    }
}
