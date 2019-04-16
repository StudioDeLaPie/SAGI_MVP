using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FeedbackOMG : MonoBehaviour
{
    public Transform gunPoint;
    public GameObject go_charge;
    public float timeToTravel = 0.14f;


    public void ShotAjout(Transform hitPoint)
    {
        StartCoroutine(TranslateCoroutine(gunPoint, hitPoint));
    }

    public void ShotRetrait(Transform hitPoint)
    {
        StartCoroutine(TranslateCoroutine(hitPoint, gunPoint));
    }

    private IEnumerator TranslateCoroutine(Transform start, Transform destination)
    {
        GameObject go = Instantiate(go_charge, start.position, Quaternion.identity);
        float distance = Vector3.Distance(go.transform.position, destination.position);
        float lastDistance = distance;
        float speed = distance / timeToTravel;

        while (distance > 1 && distance <= lastDistance)
        {
            lastDistance = distance;
            go.transform.LookAt(destination);
            go.transform.Translate(Vector3.forward * speed * Time.deltaTime, Space.Self);
            distance = Vector3.Distance(go.transform.position, destination.position);
            yield return null;
        }
        Destroy(go);
    }
}
