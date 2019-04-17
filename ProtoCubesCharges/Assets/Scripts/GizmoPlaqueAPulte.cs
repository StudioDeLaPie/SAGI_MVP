
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[ExecuteInEditMode]
public class GizmoPlaqueAPulte : MonoBehaviour
{
    public bool active = true;
    public GameObject gizmo;

    private PlaqueAPulte plaque;

    // Start is called before the first frame update
    void Start()
    {
#if (UNITY_EDITOR)
        plaque = GetComponent<PlaqueAPulte>();
        return;
#else
        Destroy(gizmo);
        Destroy(this);
#endif
    }

#if (UNITY_EDITOR)
    // Update is called once per frame
    void Update()
    {
        if (!active || EditorApplication.isPlaying)
        {
            gizmo.SetActive(false);
            return;
        }
        else
        {
            gizmo.SetActive(true);
            Vector3 gizmoScale = gizmo.transform.localScale;
            //float haut = (plaque.force * plaque.force) / (2 * gravity); //calcul de la hauteur d'un projectile
            gizmo.transform.localScale = new Vector3(gizmoScale.x, plaque.hauteurCube2 * 2, gizmoScale.z);
            gizmo.transform.localPosition = new Vector3(0, plaque.hauteurCube2, 0);
        }

        if (transform.localScale.y != 1)
        {
            Debug.LogError("La PlaqueAPulte \"" + name + "\" n'a pas une échelle Y de 1. Modification.");
            transform.localScale = new Vector3(transform.localScale.x, 1, transform.localScale.z);

        }

    }
#endif

}
