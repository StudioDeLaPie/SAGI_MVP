using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gravity : MonoBehaviour
{
    public bool isPlayer = false;
    public float maxVelocityWeight1;
    public float maxVelocityWeight2;

    private Charges charge;
    [SerializeField] private float gravityStrenght;

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
            rb.AddForce(0, -gravityStrenght, 0);
        else if (charge.CurrentPoids != 0)
        {
            //Debug.Log(rb.velocity.y);
            rb.AddForce(0, -(gravityStrenght * Mathf.Sign(charge.CurrentPoids)), 0);
            if (Mathf.Abs(charge.CurrentPoids) == 1 && Mathf.Abs(rb.velocity.y) > maxVelocityWeight1)
            {
                rb.velocity = new Vector3(rb.velocity.x, -(maxVelocityWeight1 * charge.CurrentPoids), rb.velocity.z);
            }
            if (Mathf.Abs(charge.CurrentPoids) == 2 && Mathf.Abs(rb.velocity.y) > maxVelocityWeight2)
            {
                rb.velocity = new Vector3(rb.velocity.x, -(maxVelocityWeight2 * charge.CurrentPoids), rb.velocity.z);
            }
        }
    }
}
