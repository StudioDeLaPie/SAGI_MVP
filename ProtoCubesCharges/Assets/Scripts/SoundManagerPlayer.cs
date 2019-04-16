using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManagerPlayer : MonoBehaviour
{           
    public AudioClip soundOMG_Positif;
    public AudioClip soundOMG_Negatif;
    public AudioClip soundOMG_Fail;

    public AudioClip soundRAP_AttacheDetache;
    public AudioClip soundRAP_Breack;
    public AudioClip soundRAP_Fail;

    public AudioClip soundCAP_Materialise;
    public AudioClip soundCAP_Dematerialise;
    public AudioClip soundCAP_Fail;

    public AudioClip soundORA_Charge;
    public AudioClip soundORA_Shot;
   

    private AudioSource audioSourceRAP;
    private AudioSource audioSourceCAP;
    private AudioSource audioSourceOMG;
    private AudioSource audioSourceORA;

    private void Start()
    {
        RecuperationAudioSources();
    }

    private void RecuperationAudioSources()
    {
        audioSourceRAP = GetComponents<AudioSource>()[0];
        audioSourceCAP = GetComponents<AudioSource>()[1];
        audioSourceOMG = GetComponents<AudioSource>()[2];
        audioSourceORA = GetComponents<AudioSource>()[3];
    }


    public void PlayOneShotOMGPositif()
    {
        audioSourceOMG.PlayOneShot(soundOMG_Positif);
    }

    public void PlayOneShotOMG_Negatif()
    {
        audioSourceOMG.PlayOneShot(soundOMG_Negatif);
    }

    public void PlayOneShotOMG_Fail()
    {
        audioSourceOMG.PlayOneShot(soundOMG_Fail);
    }

    public void PlayOneShotRAP_AttacheDetache()
    {
        audioSourceRAP.PlayOneShot(soundRAP_AttacheDetache);
    }

    public void PlayOneShotRAP_Breack()
    {
        audioSourceRAP.PlayOneShot(soundRAP_Breack);
    }

    public void PlayOneShotRAP_Fail()
    {
        audioSourceRAP.PlayOneShot(soundRAP_Fail);
    }

    public void PlayOneShotCAP_Materialise()
    {
        audioSourceCAP.PlayOneShot(soundCAP_Materialise);
    }

    public void PlayOneShotCAP_Dematerialise()
    {
        audioSourceCAP.PlayOneShot(soundCAP_Dematerialise);
    }

    public void PLayOneShotCAP_Fail()
    {
        audioSourceCAP.PlayOneShot(soundCAP_Fail);
    }

    public void PlayOneShotORA_Charge()
    {
        audioSourceORA.PlayOneShot(soundORA_Charge);
    }

    public void PlayOneShotORA_Shot()
    {
        audioSourceORA.PlayOneShot(soundORA_Shot);
    }
}
