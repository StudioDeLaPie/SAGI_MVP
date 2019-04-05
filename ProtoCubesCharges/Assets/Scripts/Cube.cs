using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cube : MonoBehaviour
{
    public GameObject go_hologramme;
    public GameObject go_materialise;

    private Charges charges;
    private bool materialised;
    private AimCamera feedbacks;
    private Rigidbody rb;

    public int NbCharges
    {
        get
        {
            if (materialised)
                return 0;
            else
                return charges.CurrentCharge;
        }
    }

    public int NbChargesAjoutables
    {
        get
        {
            if (materialised)
                return 0;
            else
                return charges.MaxCharges - charges.CurrentCharge;
        }
    }

    public bool Materialised
    {
        get { return materialised; }
    }


    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        charges = GetComponent<Charges>();
        go_hologramme.SetActive(true);
        go_materialise.SetActive(false);
        feedbacks = GetComponentInChildren<AimCamera>();
        feedbacks.Materialise = false;
    }

    public void Alourdir()
    {
        charges.RetraitChargeNegative();
    }

    public void Alleger()
    {
        charges.AjoutChargeNegative();
    }

    public void SwitchMaterialisation()
    {
        materialised = !materialised;
        rb.isKinematic = materialised;
        go_hologramme.SetActive(!go_hologramme.activeSelf);
        go_materialise.SetActive(!go_materialise.activeSelf);
        feedbacks.Materialise = materialised;
    }

    public void AttractionRepulsion(Vector3 hitNormal, bool isAttracting)
    {
        hitNormal = isAttracting ? hitNormal * 150f : hitNormal * -150f; //force de propulsion

        rb.velocity = Vector3.zero;
        rb.AddForce(hitNormal, ForceMode.Impulse); //Application de la force
    }
}
