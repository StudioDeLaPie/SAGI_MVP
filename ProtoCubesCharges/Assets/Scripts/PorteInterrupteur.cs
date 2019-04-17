using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PorteInterrupteur : MonoBehaviour
{
    public List<Interrupteur> interrupteurs;

    public GameObject goPorte;
    public float tailleMinimum = 0.2f;
    public float vitesse = 0.01f;

    public AudioClip soundActive;
    public AudioClip soundDesactive;

    private AudioSource audioSource;
    private Vector3 scaleInitial;
    private Coroutine coroutineOuverture;
    private Coroutine coroutineFermeture;
    private bool ouvert = false;


    void Start()
    {
        foreach (Interrupteur interrupteur in interrupteurs)
        {
            interrupteur.OnInterrupteurUpdate += CheckAlInterrupteurs;
        }
        scaleInitial = goPorte.transform.localScale;
        StartCoroutine(FermeturePorte());
        //Do GameObject set active true; 

        audioSource = GetComponent<AudioSource>();
    }

    private void CheckAlInterrupteurs()
    {
        bool allInterrupteursActivated = true;
        foreach (Interrupteur interrupt in interrupteurs)
        {
            if (interrupt.IsActivated == false) allInterrupteursActivated = false;
        }

        if (allInterrupteursActivated && !ouvert)
        {
            coroutineOuverture = StartCoroutine(OuverturePorte());
            audioSource.PlayOneShot(soundActive);
            ouvert = true;
        }
        else if (!allInterrupteursActivated && ouvert)
        {
            coroutineFermeture = StartCoroutine(FermeturePorte());
            audioSource.PlayOneShot(soundDesactive);

            ouvert = false;
        }
    }

    private IEnumerator OuverturePorte()
    {
        if (coroutineFermeture != null)
            StopCoroutine(coroutineFermeture);

        bool isOpen = false;

        while (isOpen == false)
        {
            goPorte.transform.localScale *= vitesse;

            if (goPorte.transform.localScale.magnitude <= tailleMinimum)
            {
                isOpen = true;
            }
            yield return null;
        }
        goPorte.SetActive(false);
        coroutineOuverture = null;
    }

    private IEnumerator FermeturePorte()
    {
        if (coroutineOuverture != null)
            StopCoroutine(coroutineOuverture);

        goPorte.SetActive(true);

        bool isClose = false;

        while (isClose == false)
        {
            if (goPorte.transform.localScale.x >= scaleInitial.x)
            {
                goPorte.transform.localScale = scaleInitial;
                isClose = true;
            }
            else
                goPorte.transform.localScale /= vitesse;

            yield return null;
        }
        coroutineFermeture = null;
    }

}

