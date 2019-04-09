using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour
{
    private Teleporter teleporter;
    private ParticleSystem ps;
    private bool activated = false;
    public AudioClip soundActivation;
    public AudioClip soundDesactivation;

    private AudioSource audioSource;

    public void SetTeleporter(Teleporter t) { teleporter = t; }
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
        //Debug.Log("Collision" + other.name);
        if (other.GetComponentInParent<Cube>() != null && !activated)
        {
            activated = true;
            ps.Play();
            teleporter.TargetActive();
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
            teleporter.TargetDesactive();
            audioSource.PlayOneShot(soundDesactivation);
        }
    }
}
