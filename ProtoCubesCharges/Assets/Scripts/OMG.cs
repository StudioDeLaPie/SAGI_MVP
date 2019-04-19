using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OMG : MonoBehaviour
{
    [SerializeField] private LayerMask layerMask;
    [SerializeField] private float cooldownTriggers = 0.5f;
    [SerializeField] private Transform firePoint;
    [SerializeField] private int currentCharges;
    [SerializeField] private int maxCharges;

    public Text txt_charges;


    private RaycastHit hit;
    private Cube touchedObject;
    private float lastShot;
    private SoundManagerPlayer soundManagerPlayer;
    private FeedbackOMG feedback;

    private void Start()
    {
        lastShot = Time.time;
        txt_charges.text = "Charges : " + currentCharges + " / " + maxCharges;
        soundManagerPlayer = GetComponentInChildren<SoundManagerPlayer>();
        feedback = GetComponent<FeedbackOMG>();
    }

    private void Update()
    {
        if (Input.GetButtonDown("OMGAjout") || (Input.GetAxis("OMGAjout") != 0 && Time.time > lastShot + cooldownTriggers))
        {
            Shot(true);
        }

        if (Input.GetButtonDown("OMGRetrait") || (Input.GetAxis("OMGRetrait") != 0 && Time.time > lastShot + cooldownTriggers))
        {
            Shot(false);
        }
    }

    private void Shot(bool ajout)
    {
        if (CalculateRayCast())
        {
            if (touchedObject != null)
            {
                if (ajout && currentCharges > 0 && touchedObject.NbChargesAjoutables > 0) //Ajout
                {
                    touchedObject.Alleger();
                    currentCharges--;
                    soundManagerPlayer.PlayOneShotOMGPositif();
                    feedback.ShotAjout(hit.transform);
                }
                else if (!ajout && currentCharges < maxCharges && touchedObject.NbCharges > 0) //Retrait
                {
                    touchedObject.Alourdir();
                    currentCharges++;
                    soundManagerPlayer.PlayOneShotOMG_Negatif();
                    feedback.ShotRetrait(hit.transform);
                }
                else if ((ajout && (currentCharges == 0 || touchedObject.NbChargesAjoutables == 0)) || (!ajout && (currentCharges == maxCharges || touchedObject.NbCharges == 0)))
                    soundManagerPlayer.PlayOneShotOMG_Fail();

                txt_charges.text = "Charges : " + currentCharges + " / " + maxCharges;
            }
            else
                soundManagerPlayer.PlayOneShotOMG_Fail();

        }
        else
            soundManagerPlayer.PlayOneShotOMG_Fail();
        lastShot = Time.time;
    }

    private bool CalculateRayCast()
    {
        bool result = Physics.Raycast(firePoint.position, firePoint.forward, out hit, 500f, layerMask.value);
        if (result)
            touchedObject = hit.transform.GetComponent<Cube>();
        return result;
    }
}
