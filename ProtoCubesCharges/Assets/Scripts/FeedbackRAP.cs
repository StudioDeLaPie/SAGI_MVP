using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FeedbackRAP : MonoBehaviour
{
    public LineRenderer lineRenderer;
    public Transform gunPoint;

    private Transform _touchedObject;
    private bool active;

    private void Start()
    {
        lineRenderer.enabled = false;
        active = false;
    }

    private void Update()
    {
        if (active)
        {
            lineRenderer.SetPosition(0, gunPoint.position);
            lineRenderer.SetPosition(1, _touchedObject.position);
        }
    }

    public void Active(Transform touchedObject)
    {
        _touchedObject = touchedObject;
        lineRenderer.SetPosition(0, gunPoint.position);
        lineRenderer.SetPosition(1, _touchedObject.position);
        lineRenderer.enabled = true;
        active = true;
    }

    public void Desactive()
    {
        lineRenderer.enabled = false;
        active = false;
    }
}
