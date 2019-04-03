using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CAP : MonoBehaviour
{
    public Transform firePoint;
    public LayerMask layerMask;


    private Cube touchedObject;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            if (CalculateRayCast() && touchedObject != null)
            {
                if (GetComponent<RAP>() != null)
                    GetComponent<RAP>().Detache();
                touchedObject.SwitchMaterialisation();
            }
        }
    }

    private bool CalculateRayCast()
    {
        bool result = Physics.Raycast(firePoint.position, firePoint.forward, out var hit, 500f, layerMask.value);
        if (result)
            touchedObject = hit.transform.GetComponent<Cube>();
        return result;
    }
}
