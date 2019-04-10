using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interrupteur : MonoBehaviour
{
    private PorteInterrupteur porteInterrupteur;
    private ParticleSystem ps;
    private bool activated = false;

    public AudioClip soundActivation;
    public AudioClip soundDesactivation;

    private AudioSource audioSource;

    public void SetPorte(PorteInterrupteur porte) { porteInterrupteur = porte; }
    public bool IsActivated { get { return activated; } }


    // Start is called before the first frame update
    void Start()
    {
        ps = GetComponentInChildren<ParticleSystem>();
        ps.gameObject.SetActive(true);
        ps.Stop();

        audioSource = GetComponent<AudioSource>();
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.GetComponentInParent<Cube>() != null && !activated)
        {
            activated = true;
            ps.Play();
            porteInterrupteur.InterrupteurActive();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponentInParent<Cube>() != null && !activated)
        {
            audioSource.PlayOneShot(soundActivation);
        }
    }


    private void OnTriggerExit(Collider other)
    {
        if (other.GetComponentInParent<Cube>() != null && activated)
        {
            activated = false;
            ps.Stop();
            ps.Clear();
            porteInterrupteur.InterrupteurDesactive();
            audioSource.PlayOneShot(soundDesactivation);
        }
    }
}

