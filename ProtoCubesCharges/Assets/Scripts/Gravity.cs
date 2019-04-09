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
            //Chute
            rb.AddForce(0, -(gravityStrenght * Mathf.Sign(charge.CurrentPoids)), 0);

            //Contrôle de la vélocité maximale sur l'axe de chute
            bool brake = false;
            switch (charge.CurrentPoids)
            {
                case 2:
                    if (rb.velocity.y < -maxVelocityWeight2)
                        brake = true;
                    break;
                case 1:
                    if (rb.velocity.y < -maxVelocityWeight1)
                        brake = true;
                    break;
                case -1:
                    if (rb.velocity.y > maxVelocityWeight1)
                        brake = true;
                    break;
                case -2:
                    if (rb.velocity.y > maxVelocityWeight2)
                        brake = true;
                    break;
            }
            if (brake)
            {
                rb.velocity = new Vector3(rb.velocity.x,
                                            -((Mathf.Abs(charge.CurrentPoids) == 1 ? maxVelocityWeight1 : maxVelocityWeight2) * charge.CurrentPoids),
                                            rb.velocity.z);
            }
        }
    }
}
