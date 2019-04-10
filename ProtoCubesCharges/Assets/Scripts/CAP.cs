using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CAP : MonoBehaviour
{
    public Transform firePoint;
    public LayerMask layerMask;
    public GameObject feedbackCap;

    private Cube touchedObject;
    private Coroutine feedbackCoroutine;

    private void Start()
    {
        feedbackCap.SetActive(false);
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

                if (feedbackCoroutine != null) StopCoroutine(feedbackCoroutine);

                feedbackCoroutine = StartCoroutine(DisplayFeedback());
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

    private IEnumerator DisplayFeedback()
    {
        //float time = Time.time;
        int nbFrame = 0;
        feedbackCap.SetActive(true);
        //if (Time.time < time + 0.5f)
        if (nbFrame < 3)
        {
            nbFrame++;
            yield return null;
        }
        feedbackCap.SetActive(false);
    }
}
