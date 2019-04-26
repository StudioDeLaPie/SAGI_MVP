using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Teleporter : MonoBehaviour
{
    public List<Target> targets;

    public delegate void TargetUpdate();
    public event TargetUpdate OnTargetUpdate;

    public AudioClip soundActive;

    private AudioSource audioSource;

    private ParticleSystem particulesSystem;
    private Collider teleporterZone;

    void Start()
    {
        foreach (Target target in targets)
        {
            target.SetTeleporter(this);
        }

        teleporterZone = GetComponent<Collider>();
        teleporterZone.enabled = false;

        particulesSystem = GetComponent<ParticleSystem>();
        particulesSystem.Stop();

        audioSource = GetComponent<AudioSource>();
        audioSource.loop = true;
        audioSource.clip = soundActive;
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
            audioSource.Play();
        }
        else
        {
            audioSource.Stop();
            particulesSystem.Stop();
            particulesSystem.Clear();
            teleporterZone.enabled = false;
        }
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            // Si la dernière scène avant l'écran de fin
            if (SceneManager.GetActiveScene().buildIndex == SceneManager.sceneCountInBuildSettings - 3) // - 3 = endScreen, selectorLevel, count
            {
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
            }
            other.transform.root.GetComponent<PlayerMovementController>().enabled = false;
            other.transform.root.GetComponent<Rigidbody>().isKinematic = true;
            other.transform.root.GetComponentInChildren<BlackScreenTransition>().FadeIn();
            StartCoroutine(Teleport(other.transform.root.GetComponentInChildren<BlackScreenTransition>()));
        }
    }

    public IEnumerator Teleport(BlackScreenTransition blackScreen)
    {
        while (blackScreen.isOnTransition)
            yield return null;
        yield return new WaitForSeconds(0.5f);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1, LoadSceneMode.Single);
    }
}
