using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class ControlAmbiantVolume : MonoBehaviour
{
    public AudioMixer audioMixer;

    private void Start()
    {
        float volume;
        audioMixer.GetFloat("AmbiantVolume", out volume);
        GetComponent<Slider>().value = volume;
    }

    public void SetVolume(float volume)
    {
        audioMixer.SetFloat("AmbiantVolume", volume);
    }

}
