using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour
{
    private ParticleSystem ps;
    public Teleporter teleporter;

    // Start is called before the first frame update
    void Start()
    {
        ps = GetComponent<ParticleSystem>();
        ps.Stop();
    }

    private void OnTriggerEnter(Collider other)
    {
        //Debug.Log("Collision" + other.name);
        if (other.GetComponentInParent<Cube>() != null)
        {
            //Debug.Log("Activé");
            ps.Play();
            teleporter.TargetActive();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.GetComponentInParent<Cube>() != null)
        {
            //Debug.Log("Désactivé");
            ps.Stop();
            teleporter.TargetDesactive();
        }
    }
}
