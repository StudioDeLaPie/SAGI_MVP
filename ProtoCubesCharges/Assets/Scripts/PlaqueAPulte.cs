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
        Rigidbody rg = other.GetComponent<Rigidbody>();

        if(rg == null)//Si on a pas trouvé de rigidbody c'est qu'il s'agit d'un cube ou autre chose
        {
            rg = other.gameObject.GetComponentInParent<Rigidbody>();//On cherche dans les parents un rigidbody
        }

        if (rg != null) 
        {            
            rg.velocity = Vector3.zero;
            rg.AddForce(Vector3.up * force);
            audioSource.PlayOneShot(soundBounce);
            StartCoroutine(AnimationActivation());
        }       
    }
}
