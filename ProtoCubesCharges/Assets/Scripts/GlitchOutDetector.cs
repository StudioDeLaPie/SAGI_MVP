using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlitchOutDetector : MonoBehaviour
{
    private GameObject errorMessage;

    private bool messageAffiche = false;
    // Start is called before the first frame update
    void Start()
    {
        errorMessage = GameObject.FindGameObjectWithTag("ErrorMessage");
        errorMessage.SetActive(false);
    }

    private void AfficheMessage()
    {
        errorMessage.SetActive(true);
        messageAffiche = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!messageAffiche)
            AfficheMessage();
    }

    private void OnTriggerStay(Collider other)
    {
        if (!messageAffiche)
            AfficheMessage();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (!messageAffiche)
            AfficheMessage();
    }

    private void OnCollisionStay(Collision collision)
    {
        if (!messageAffiche)
            AfficheMessage();
    }
}
