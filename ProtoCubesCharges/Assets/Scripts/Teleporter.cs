using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleporter : MonoBehaviour
{
    [SerializeField] private int nbTargetTotal;
    [SerializeField] private int nbTargetActivated;
    public List<Target> targets;
    private ParticleSystem particulesSystem;

    private Collider teleporterZone;

    public void Teleport()
    {
        Debug.Log("Niveau Terminé");
    }

    void Start()
    {
        foreach ( Target target in targets)
        {
            target.teleporter = this;
        }
        nbTargetTotal = targets.Count;
        nbTargetActivated = 0;

        teleporterZone = GetComponent<Collider>();
        teleporterZone.enabled = false;

        particulesSystem = GetComponent<ParticleSystem>();
        particulesSystem.Stop();
    }

    public void TargetActive()
    {
        nbTargetActivated++;
        CheckAllTargets();
    }

    public void TargetDesactive()
    {
        nbTargetActivated--;
        CheckAllTargets();
    }

    private void CheckAllTargets()
    {
        if (nbTargetActivated >= nbTargetTotal)
        {
            teleporterZone.enabled = true;
            particulesSystem.Play();
        }
        else
        {
            particulesSystem.Stop();
            teleporterZone.enabled = false;
        }
    }
    
    public void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            Teleport();
        }
    }
}
