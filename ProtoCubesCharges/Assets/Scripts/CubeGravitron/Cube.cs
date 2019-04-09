using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cube : MonoBehaviour
{
    [SerializeField] private bool materialised = false;
    private Charges charges;
    private Rigidbody rb;
    private CubeFeedbackManager feedbackManager;

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
        feedbackManager = GetComponent<CubeFeedbackManager>();
        feedbackManager.Init(charges.CurrentCharge, charges.CurrentPoids, materialised);
    }

    public void Alourdir()
    {
        charges.RetraitChargeNegative();
        feedbackManager.Charges = charges.CurrentCharge;
        feedbackManager.Poids = charges.CurrentPoids;
    }

    public void Alleger()
    {
        charges.AjoutChargeNegative();
        feedbackManager.Charges = charges.CurrentCharge;
        feedbackManager.Poids = charges.CurrentPoids;
    }

    public void SwitchMaterialisation()
    {
        materialised = !materialised;
        rb.isKinematic = materialised;
        feedbackManager.Materialise = materialised;
    }

    public void AttractionRepulsion(Vector3 hitNormal, bool isAttracting)
    {
        hitNormal = isAttracting ? hitNormal * 150f : hitNormal * -150f; //force de propulsion

        rb.velocity = Vector3.zero;
        rb.AddForce(hitNormal, ForceMode.Impulse); //Application de la force
    }
}
