using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gravity : MonoBehaviour
{
    public bool isPlayer = false;

    private Charges charge;
    [SerializeField] private float weightMultiplier = 200;

    private Rigidbody rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        if (!isPlayer)
            charge = GetComponent<Charges>();
    }


    private void FixedUpdate()
    {
        if (isPlayer)
            rb.AddForce(0, -weightMultiplier, 0);
        else
            rb.AddForce(0, -(charge.CurrentPoids * weightMultiplier), 0);
    }
}
