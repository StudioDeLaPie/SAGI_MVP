using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PorteInterrupteur : MonoBehaviour
{
    public List<Interrupteur> interrupteurs;

    public GameObject goPorte;
    public float tailleMinimum;
    public float vitesse = 0.01f;

    private Vector3 scaleInitial;
    private Coroutine coroutineOuverture;
    private Coroutine coroutineFermeture;

    public AudioClip soundActive;
    private AudioSource audioSource;

    void Start()
    {
        foreach (Interrupteur interrupteur in interrupteurs)
        {
            interrupteur.SetPorte(this);
        }
        scaleInitial = goPorte.transform.localScale;
        //Do GameObject set active true;             
    }

    public void InterrupteurActive()
    {
        CheckAlInterrupteurs();
    }

    public void InterrupteurDesactive()
    {
        CheckAlInterrupteurs();
    }

    private void CheckAlInterrupteurs()
    {
        bool allInterrupteursActivated = true;
        foreach (Interrupteur interrupt in interrupteurs)
        {
            if (interrupt.IsActivated == false) allInterrupteursActivated = false;
        }

        if (allInterrupteursActivated)
        {

            coroutineOuverture = StartCoroutine(OuverturePorte());
        }
        else
        {
            coroutineFermeture = StartCoroutine(FermeturePorte());
        }
    }

    private IEnumerator OuverturePorte()
    {
        if (coroutineFermeture != null)
            StopCoroutine(coroutineFermeture);

        bool isOpen = false;

        while (isOpen == false)
        {
            goPorte.transform.localScale -= new Vector3(vitesse, vitesse, vitesse);

            if (goPorte.transform.localScale.x <= tailleMinimum)
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
            goPorte.transform.localScale += new Vector3(vitesse,vitesse,vitesse);

            if (goPorte.transform.localScale.x >= scaleInitial.x)
            {
                isClose = true;
            }
            yield return null;
        }
        coroutineFermeture = null;
    }

}

