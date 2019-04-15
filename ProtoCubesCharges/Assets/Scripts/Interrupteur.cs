using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interrupteur : MonoBehaviour
{
    private ParticleSystem ps;
    private bool activated = false;

    public AudioClip soundActivation;
    public AudioClip soundDesactivation;

    public delegate void InterrupteurUpdate();
    public event InterrupteurUpdate OnInterrupteurUpdate;

    private AudioSource audioSource;

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
            OnInterrupteurUpdate();
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
            OnInterrupteurUpdate();
            audioSource.PlayOneShot(soundDesactivation);
        }
    }
}

