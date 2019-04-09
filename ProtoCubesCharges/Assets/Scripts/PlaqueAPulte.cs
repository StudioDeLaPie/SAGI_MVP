using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaqueAPulte : MonoBehaviour
{
    public GameObject simpleAnimate;
    public float force = 15000;
    public float durationApparition = 0.2f;

    public AudioClip soundBounce;

    private AudioSource audioSource;

    void Start()
    {
        simpleAnimate.SetActive(false);
        audioSource = GetComponent<AudioSource>();
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

    private void OnTriggerEnter(Collider other)
    {
        Rigidbody rg = other.transform.root.GetComponent<Rigidbody>(); //on récupère le rigidbody à la racine

        if(rg == null)//Au cas où le rigidbody n'est pas à la racine
        {
            rg = other.transform.root.GetComponentInChildren<Rigidbody>();//On cherche dans les enfants
        }

        if (rg != null)
        {
            Vector3 newVelocity = transform.InverseTransformVector(rg.velocity);
            newVelocity.y = force;
            rg.velocity = transform.TransformVector(newVelocity);
            audioSource.PlayOneShot(soundBounce);
            //rg.velocity = Vector3.zero;
            //rg.AddForce(Vector3.up * force);
            StartCoroutine(AnimationActivation());
        }
    }
}
