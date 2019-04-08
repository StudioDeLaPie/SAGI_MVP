using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaterialManager : MonoBehaviour
{
    public List<GameObject> feedBackPoids;
    public List<GameObject> feedBackCharges;

    [SerializeField] private new MeshRenderer renderer;

    private Light light;
    private Dictionary<int, Material> materials;
    private string pathMaterials = "Materials/Cubes/";
    private Charges charges;
    private int materialIndex;
    private new Rigidbody rigidbody;


    private void Start()
    {
        Initialisation();
        charges = GetComponent<Charges>();
        rigidbody = GetComponent<Rigidbody>();
        light = GetComponentInChildren<Light>();
        UpdateMaterial();
        ChangeMaterial();
    }

    private void Initialisation()
    {
        materials = new Dictionary<int, Material>();
        materials.Add(-2, Resources.Load(pathMaterials + "Cube-2") as Material);
        materials.Add(-1, Resources.Load(pathMaterials + "Cube-1") as Material);
        materials.Add(0, Resources.Load(pathMaterials + "Cube0") as Material);
        materials.Add(1, Resources.Load(pathMaterials + "Cube1") as Material);
        materials.Add(2, Resources.Load(pathMaterials + "Cube2") as Material);
        materials.Add(3, Resources.Load(pathMaterials + "CubeFige") as Material);
    }

    private void Update()
    {
        UpdateMaterial();
        ChangeMaterial();
    }

    public void UpdateFeedback()
    {
        foreach (GameObject go in feedBackPoids)
        {
            go.SetActive(false);
        }

        if (Math.Abs(charges.CurrentPoids) == 0)
        {
            feedBackPoids[0].SetActive(true);
        }
        else
        {
            int index = Mathf.Abs(charges.CurrentPoids);
            feedBackPoids[index].SetActive(true);

            float orientation = 90;
            if (charges.CurrentPoids < 0)
                orientation *= -1;

            feedBackPoids[index].transform.rotation = Quaternion.identity;
            feedBackPoids[index].transform.Rotate(0, 0, orientation);
        }
        
        foreach (GameObject go in feedBackCharges)
        {
            go.SetActive(false);
        }
        feedBackCharges[charges.CurrentCharge].SetActive(true);

        light.color = materials[materialIndex].color;
    }

    public void UpdateMaterial()
    {
        if (rigidbody.isKinematic)
            materialIndex = 3;
        else
            materialIndex = charges.CurrentPoids;
    }

    private void ChangeMaterial()
    {
        renderer.material = materials[materialIndex];
    }
}
