using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeColorParticlesInterrupteur : MonoBehaviour
{
    void Start()
    {
        ParticleSystem.MainModule main = GetComponent<ParticleSystem>().main;
        Color couleur = GetComponentInParent<MeshRenderer>().material.color;
        couleur.a = 1;
        main.startColor = couleur;
        Destroy(this);
    }
}
