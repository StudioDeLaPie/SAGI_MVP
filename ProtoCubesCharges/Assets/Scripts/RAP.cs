using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utilities;

public class RAP : MonoBehaviour
{
    public GameObject goRAP;
    public LayerMask layerMask;
    [Range(0.51f, 10)] public float minRange = 3.0f;
    public float maxRange = 6.0f;
    public float scrollSpeed = 0.1f;

    private Transform transformRAP;
    private ConfigurableJoint jointRAP;
    private bool active;
    private Rigidbody touchedObject;
    private float minRangeFromTouchedObject;

    // Start is called before the first frame update
    void Start()
    {
        transformRAP = goRAP.GetComponent<Transform>();
        jointRAP = goRAP.GetComponent<ConfigurableJoint>();
        goRAP.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (active)
            {
                Detache();
            }
            else
            {
                Attache();
            }
        }
       
        if (Input.mouseScrollDelta.y != 0 && active)
        {
            float newDistance = jointRAP.anchor.z + Input.mouseScrollDelta.y * scrollSpeed;
            newDistance = Mathf.Clamp(newDistance, minRangeFromTouchedObject, maxRange);
            jointRAP.anchor = new Vector3(0, 0, newDistance);
        }
    }

    private void Attache()
    {
        if (CalculateRayCast() && touchedObject != null)
        {
            goRAP.SetActive(true);
            jointRAP.connectedBody = touchedObject;

            //Définition de la distance de l'ancre
            minRangeFromTouchedObject = minRange + (touchedObject.transform.localScale.x * CONST.ROOT3) / 2;
            jointRAP.anchor = new Vector3(0, 0, (minRangeFromTouchedObject + maxRange) / 2.0f);

            touchedObject.GetComponent<Gravity>().enabled = false;
            active = true;
        }
    }

    public void Detache()
    {
        jointRAP.connectedBody = null;
        if (touchedObject != null)
            touchedObject.GetComponent<Gravity>().enabled = true;
        goRAP.SetActive(false);
        active = false;
    }

    private bool CalculateRayCast()
    {
        bool result = Physics.Raycast(transformRAP.position, transformRAP.forward, out var hit, maxRange, layerMask.value);
        if (result)
            touchedObject = hit.transform.GetComponent<Rigidbody>();
        return result;
    }
}
