using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class GizmoPlaqueAPulte : MonoBehaviour
{
    public bool active = true;
    public GameObject gizmo;

    private float gravity = 42.6f;
    private PlaqueAPulte plaque;

    // Start is called before the first frame update
    void Start()
    {
        plaque = GetComponent<PlaqueAPulte>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!active)
        {
            gizmo.SetActive(false);
            return;
        }
        else 
        {
            gizmo.SetActive(true);
            Vector3 gizmoScale = gizmo.transform.localScale;
            float taille = (plaque.force * plaque.force) / (2 * gravity); //calcul de la hauteur d'un projectile
            gizmo.transform.localScale = new Vector3(gizmoScale.x, taille, gizmoScale.z);
            gizmo.transform.localPosition = new Vector3(0, taille / 2, 0);
        }

        if (transform.localScale.y != 1)
        {
            Debug.LogError("La PlaqueAPulte \"" + name + "\" n'a pas une échelle Y de 1. Modification.");
            transform.localScale = new Vector3(transform.localScale.x, 1, transform.localScale.z);

        }


    }

}
