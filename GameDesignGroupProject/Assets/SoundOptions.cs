using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class SoundOptions : MonoBehaviour
{
    [SerializeField] private AudioMixer audioMixer;

    public void SetMasterVolume(float volume) {
        audioMixer.SetFloat("Volume", volume);
    }

    public void SetMusicVolume(float volume) {
        audioMixer.SetFloat("Music", volume);
    }
    
    public void SetSoundVolume(float volume) {
        audioMixer.SetFloat("Sound", volume);
    }
    
}
