using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
using Utilities;

/// <summary>
/// Règle le volume de l'audioMixer pour le son ambiant grace au slider (UI inGame)
/// </summary>

public class ControlAmbiantVolume : MonoBehaviour
{
    public AudioMixer audioMixer;

    private void Start()
    {
        float volume;
        audioMixer.GetFloat("AmbiantVolume", out volume);
        volume = SoundUtilities.DecibelToLinear(volume);
        GetComponent<Slider>().value = volume;
    }

    public void SetVolume(float volume)
    {
        audioMixer.SetFloat("AmbiantVolume", SoundUtilities.LinearToDecibel(volume));
    }
}
