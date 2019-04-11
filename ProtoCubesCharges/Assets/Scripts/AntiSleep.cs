using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AntiSleep : MonoBehaviour
{
    private Rigidbody rg;
    public bool active = false;

    private void Start()
    {
        rg = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
       if(active)
        {
            rg.AddForce(new Vector3(0,0,0.00001f));
        }
    }
}
