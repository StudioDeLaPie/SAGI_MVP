using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;
using Utilities;

public class AimCamera : MonoBehaviour
{
    public GameObject go_fleches;
    public GameObject go_charges;

    private bool materialise;
    private bool prevMaterialise;

    public bool Materialise
    {
        set { materialise = value; }
    }

    // Start is called before the first frame update
    void Start()
    {
        ConstraintSource source = new ConstraintSource();
        source.sourceTransform = Camera.main.transform;
        source.weight = 1;
        foreach (AimConstraint constraint in GetComponentsInChildren<AimConstraint>())
        {
            constraint.AddSource(source);
            constraint.constraintActive = true;
        }

        materialise = false;
        prevMaterialise = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (materialise)
        {
            if (!prevMaterialise)
            {
                go_fleches.transform.Translate(0, 0, (transform.root.localScale.x * CONST.ROOT3) / 2f, Space.World);
                go_charges.transform.Translate(0, 0, (transform.root.localScale.x * CONST.ROOT3) / 2f, Space.World);
            }
            transform.LookAt(Camera.main.transform);
        }
        else
        {
            if (prevMaterialise)
            {
                go_fleches.transform.position = transform.position;
                go_charges.transform.position = transform.position;
                transform.rotation = Quaternion.identity;
            }
        }

        prevMaterialise = materialise;
    }


}
