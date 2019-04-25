using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaqueAPulte : MonoBehaviour
{
    public GameObject simpleAnimate;
    public float hauteurCube2 = 10;
    public float durationApparition = 0.2f;

    public AudioClip soundBounce;

    private float gravity = 42.6f;
    private AudioSource audioSource;
    private float lastSound;

    void Start()
    {
        simpleAnimate.SetActive(false);
        audioSource = GetComponent<AudioSource>();
        lastSound = Time.time;
    }

    private IEnumerator AnimationActivation()
    {
        float time = Time.time;
        simpleAnimate.SetActive(true);
        while (Time.time < time + durationApparition)
        {
            yield return null;
        }
        simpleAnimate.SetActive(false);
    }

    private void Projection(Collider other)
    {
        Rigidbody rg = other.transform.root.GetComponent<Rigidbody>(); //on récupère le rigidbody à la racine

        if (rg == null)//Au cas où le rigidbody n'est pas à la racine
        {
            rg = other.transform.root.GetComponentInChildren<Rigidbody>();//On cherche dans les enfants
        }

        if (rg != null && other.transform.root.GetComponent<Gravity>().enabled)
        {
            Charges charges = other.transform.root.GetComponent<Charges>();
            Vector3 newVelocity = transform.InverseTransformVector(rg.velocity);

            switch (charges.CurrentPoids)
            {
                case 1:
                case -1:
                    newVelocity.y = Mathf.Sqrt(hauteurCube2 * 4 * gravity);
                    break;
                case 0:
                case 2:
                case -2:
                    newVelocity.y = Mathf.Sqrt(hauteurCube2 * 2 * gravity);
                    break;
            }

            rg.velocity = transform.TransformVector(newVelocity);

            if (Time.time > lastSound + durationApparition)
            {
                audioSource.PlayOneShot(soundBounce);
                lastSound = Time.time;
            }
            StartCoroutine(AnimationActivation());
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        Projection(other);
    }

    private void OnTriggerStay(Collider other)
    {
        Projection(other);
    }

}
