using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour
{
    private ParticleSystem ps;

    // Start is called before the first frame update
    void Start()
    {
        ps = GetComponent<ParticleSystem>();
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Collision" + other.name);
        if (other.GetComponentInParent<Cube>() != null)
        {
            Debug.Log("Activé");
            ps.Play();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.GetComponentInParent<Cube>() != null)
        {
            Debug.Log("Désactivé");
            ps.Stop();
        }
    }
}
