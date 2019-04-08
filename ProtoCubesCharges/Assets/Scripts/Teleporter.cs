using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Teleporter : MonoBehaviour
{
    public List<Target> targets;

    public delegate void TargetUpdate();
    public event TargetUpdate OnTargetUpdate;

    [SerializeField] private int nbTargets;
    private ParticleSystem particulesSystem;
    private Collider teleporterZone;

    void Start()
    {
        foreach ( Target target in targets)
        {
            target.SetTeleporter(this);
        }
        nbTargets = targets.Count;

        teleporterZone = GetComponent<Collider>();
        teleporterZone.enabled = false;

        particulesSystem = GetComponent<ParticleSystem>();
        particulesSystem.Stop();
    }

    public void TargetActive()
    {
        CheckAllTargets();
    }

    public void TargetDesactive()
    {
        CheckAllTargets();
    }

    private void CheckAllTargets()
    {
        OnTargetUpdate(); //événement lancé pour que l'UI se mette à jour

        bool allTargetsActivated = true;
        foreach (Target t in targets)
        {
            if (t.IsActivated == false) allTargetsActivated = false;
        }

        if (allTargetsActivated)
        {
            teleporterZone.enabled = true;
            particulesSystem.Play();
        }
        else
        {
            particulesSystem.Stop();
            particulesSystem.Clear();
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

    public void Teleport()
    {
        Debug.Log("Niveau Terminé");
        int index = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(index + 1, LoadSceneMode.Single);
    }
}
