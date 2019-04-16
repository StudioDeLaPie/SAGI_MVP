using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CubesTxtCharges : MonoBehaviour
{
    public Text text;
    private Charges charges;

    // Start is called before the first frame update
    void Start()
    {
        charges = GetComponent<Charges>();
    }

    // Update is called once per frame
    void Update()
    {
        text.text = "Charges : " + charges.CurrentCharge + "\nPoids : " + charges.CurrentPoids;
        
    }
}
