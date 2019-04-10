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

    public float breakForce;  

    private SoundManagerPlayer soundManagerPlayer;    
    private Transform transformRAP;
    private FeedbackRAP feedbackRAP;
    private ConfigurableJoint jointRAP;
    private bool active;
    private Rigidbody touchedObject;
    private float minRangeFromTouchedObject;

    

    // Start is called before the first frame update
    void Start()
    {
        transformRAP = goRAP.GetComponent<Transform>();
        //jointRAP = goRAP.GetComponent<ConfigurableJoint>();
        InitJoint();
        feedbackRAP = GetComponent<FeedbackRAP>();
        goRAP.SetActive(false);
        soundManagerPlayer = GetComponentInChildren<SoundManagerPlayer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (active && jointRAP == null)
        {
            JointBreak();
        }
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (active)
            {
                Detache();
                soundManagerPlayer.PlayOneShotRAP_AttacheDetache();                
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
            touchedObject.AddForce(0.1f, 0.1f, 0.1f);//Wake up le cube pour que le joint s'actualise
        }
    }

    private void JointBreak()
    {
        InitJoint();
        Detache();
        soundManagerPlayer.PlayOneShotRAP_Breack();        
    }

    private void Attache()
    {
        if (CalculateRayCast() && touchedObject != null) //Si on a touché un rigidbody
        {
            if (touchedObject.GetComponent<Cube>() != null && !touchedObject.GetComponent<Cube>().Materialised) //s'il s'agit d'un cube non-matérialisé
            {
                soundManagerPlayer.PlayOneShotRAP_AttacheDetache();
                goRAP.SetActive(true);
                jointRAP.connectedBody = touchedObject;

                //Définition de la distance de l'ancre
                minRangeFromTouchedObject = minRange + (touchedObject.transform.localScale.x * CONST.ROOT3) / 2;
                jointRAP.anchor = new Vector3(0, 0, (minRangeFromTouchedObject + maxRange) / 2.0f);

                touchedObject.GetComponent<Gravity>().enabled = false;
                active = true;
                feedbackRAP.Active(touchedObject.transform);

            }
            else //Si on touche un cube figé
                soundManagerPlayer.PlayOneShotRAP_Fail();
        }
        else //Si on touche rien
            soundManagerPlayer.PlayOneShotRAP_Fail();
    }

    public void Detache()
    {
        jointRAP.connectedBody = null;
        if (touchedObject != null)
            touchedObject.GetComponent<Gravity>().enabled = true;
        goRAP.SetActive(false);
        active = false;
        feedbackRAP.Desactive();
    }

    private bool CalculateRayCast()
    {
        bool result = Physics.Raycast(transformRAP.position, transformRAP.forward, out var hit, maxRange, layerMask.value);
        if (result)
            touchedObject = hit.transform.GetComponent<Rigidbody>();
        return result;
    }

    /// <summary>
    /// Crée un nouveau ConfigurableJoint avec les caractéristiques voulues pour le RAP
    /// </summary>
    private void InitJoint()
    {
        if (goRAP.GetComponent<ConfigurableJoint>() != null)
            Destroy(goRAP.GetComponent<ConfigurableJoint>());

        jointRAP = goRAP.AddComponent<ConfigurableJoint>();
        jointRAP.anchor = Vector3.zero;
        jointRAP.autoConfigureConnectedAnchor = false;
        jointRAP.connectedAnchor = Vector3.zero;
        jointRAP.xMotion = ConfigurableJointMotion.Locked;
        jointRAP.yMotion = ConfigurableJointMotion.Locked;
        jointRAP.zMotion = ConfigurableJointMotion.Locked;

        SoftJointLimitSpring jointLimit = new SoftJointLimitSpring();
        jointLimit.spring = 1;
        jointRAP.linearLimitSpring = jointLimit;

        jointRAP.enablePreprocessing = false;
        jointRAP.breakForce = breakForce;
    }
}
