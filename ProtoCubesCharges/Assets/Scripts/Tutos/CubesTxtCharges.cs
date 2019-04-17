using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CubesTxtCharges : MonoBehaviour
{
    public Text text;
    public Target disableTextWhenActiveTarget;
    public Interrupteur disableTextWhenActiveInterrupt1;
    public Interrupteur disableTextWhenActiveInterrupt2;
    private Charges charges;

    // Start is called before the first frame update
    void Start()
    {
        charges = GetComponent<Charges>();
    }

    // Update is called once per frame
    void Update()
    {
        if (disableTextWhenActiveTarget.IsActivated && disableTextWhenActiveInterrupt1.IsActivated && disableTextWhenActiveInterrupt2.IsActivated)
        {
            Destroy(text);
            Destroy(this);
        }
        text.text = "Charges : " + charges.CurrentCharge + "\nPoids : " + charges.CurrentPoids;

    }
}
