using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CAP : MonoBehaviour
{
    public Transform firePoint;
    public LayerMask layerMask;
    public GameObject feedbackCap;
    public float animationDuration;

    private Cube touchedObject;
    private Coroutine feedbackCoroutine;
    private SoundManagerPlayer soundManagerPlayer;

    private void Start()
    {
        feedbackCap.SetActive(false);
        soundManagerPlayer = GetComponentInChildren<SoundManagerPlayer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("CAP")) // appuie bouton CAP
        {
            RAP rap = GetComponent<RAP>();
            if (rap.GetConnectedObject() != null) // si RAP activé, on fige le cube attaché
            {
                touchedObject = rap.GetConnectedObject().GetComponent<Cube>();
                rap.Detache();
                //On joue pas le feedback parce qu'on est pas forcément en face
                soundManagerPlayer.PlayOneShotCAP_Materialise();
                touchedObject.SwitchMaterialisation();
            }
            else
            { // RAP désactivé donc comportement normal

                if (CalculateRayCast() && touchedObject != null)
                {
                    if (GetComponent<RAP>() != null)
                        GetComponent<RAP>().Detache();

                    if (feedbackCoroutine != null) StopCoroutine(feedbackCoroutine);
                    feedbackCoroutine = StartCoroutine(DisplayFeedback());

                    if (touchedObject.Materialised) soundManagerPlayer.PlayOneShotCAP_Dematerialise();
                    else soundManagerPlayer.PlayOneShotCAP_Materialise();

                    touchedObject.SwitchMaterialisation();
                }
                else
                    soundManagerPlayer.PLayOneShotCAP_Fail();
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
        float time = Time.time;
        feedbackCap.SetActive(true);
        while (Time.time < time + animationDuration)
        {
            yield return null;
        }
        feedbackCap.SetActive(false);
    }
}
