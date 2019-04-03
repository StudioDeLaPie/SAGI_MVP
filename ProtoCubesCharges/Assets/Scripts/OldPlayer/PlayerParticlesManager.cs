using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerParticlesManager : MonoBehaviour {

    public ParticleSystem groundParticles;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.contacts[0].point.y < transform.position.y)
        {
            groundParticles.Stop();
            groundParticles.Play();
        }
    }
}
