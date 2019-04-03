using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ORA : MonoBehaviour
{
    [SerializeField] private LayerMask layerMask;
    [SerializeField] private Transform firePoint;
    [SerializeField] private GameObject prefabFaceOverlay;
    [SerializeField] private float range = 6f;
    private RaycastHit hit;
    private Vector3 hitNormal;
    private Cube touchedObject;
    private GameObject faceOverlay;
    private OMG Omg;


    private void Start()
    {
        faceOverlay = Instantiate(prefabFaceOverlay);
        DontDestroyOnLoad(faceOverlay);
        Omg = GetComponent<OMG>();
    }

    void Update()
    {
        if (Input.GetKey(KeyCode.LeftShift))
        {
            if (Omg.isActiveAndEnabled)
                Omg.enabled = false;

            if (CalculateRaycast())
            {

                if (touchedObject != null)
                {
                    CalculateHitNormal();
                    UpdateOverlay();
                    if (Input.GetButtonDown("Fire1"))
                    {
                        touchedObject.AttractionRepulsion(hitNormal, false);
                    }
                    else if (Input.GetButtonDown("Fire2"))
                    {
                        touchedObject.AttractionRepulsion(hitNormal, true);
                    }
                }
                else
                {
                    faceOverlay.SetActive(false);
                }
            }
            else
            {
                faceOverlay.SetActive(false);
            }
        }
        if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            Omg.enabled = true;
            faceOverlay.SetActive(false);
        }
    }

    void OnDestroy()
    {
        Destroy(faceOverlay);
    }

    void UpdateOverlay()
    {
        faceOverlay.transform.position = hit.transform.position;
        Vector3 rotation = Vector3.zero;

        if (hitNormal == Vector3.up)
            rotation = new Vector3(-90, 0, 0);
        else if (hitNormal == Vector3.down)
            rotation = new Vector3(90, 0, 0);
        else if (hitNormal == Vector3.forward)
            rotation = Vector3.zero;
        else if (hitNormal == Vector3.back)
            rotation = new Vector3(0, 180, 0);
        else if (hitNormal == Vector3.left)
            rotation = new Vector3(0, -90, -90);
        else if (hitNormal == Vector3.right)
            rotation = new Vector3(0, 90, 90);

        faceOverlay.transform.eulerAngles = rotation;
        faceOverlay.transform.localScale = hit.transform.localScale;

        faceOverlay.SetActive(true);
        // soundPlayerManager.StartOverlay();
    }

    private void CalculateHitNormal()
    {
        hitNormal = hit.normal;
        Vector3 absNormal = new Vector3(Mathf.Abs(hit.normal.x), Mathf.Abs(hit.normal.y), Mathf.Abs(hit.normal.z));
        hitNormal = new Vector3
        {
            x = absNormal.x > absNormal.y && absNormal.x > absNormal.z ? 1 : 0,
            y = absNormal.y > absNormal.x && absNormal.y > absNormal.z ? 1 : 0,
            z = absNormal.z > absNormal.x && absNormal.z > absNormal.y ? 1 : 0
        };
        hitNormal = Vector3.Scale(hitNormal, hit.normal).normalized;
    }

    private bool CalculateRaycast()
    {
        bool result = Physics.Raycast(firePoint.position, firePoint.forward, out hit, range, layerMask.value);
        if (result)
            touchedObject = hit.transform.GetComponent<Cube>();
        return result;
    }
}
