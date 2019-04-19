using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlecheTransparencyActivator : MonoBehaviour
{
    public float minDistance = 0.5f;

    private SphereCollider _collider;

    // Start is called before the first frame update
    void Start()
    {
        _collider = GetComponent<SphereCollider>();
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Fige"))
        {
            float percentage = (Vector3.Distance(transform.position, other.ClosestPoint(transform.position)) - minDistance) / _collider.radius;
            //Debug.Log(percentage);
            other.GetComponentInParent<CubeFeedbackManager>().TransparencyPercentage = percentage;
        }
    }
}
