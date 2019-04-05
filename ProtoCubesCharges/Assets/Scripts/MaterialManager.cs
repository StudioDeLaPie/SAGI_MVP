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
        UpdateFeedback();
        ChangeMaterial();
    }

    private void UpdateFeedback()
    {
        foreach(GameObject go in feedBackPoids)
        {
            go.SetActive(false);
        }
        feedBackPoids[charges.CurrentPoids + 2].SetActive(true);

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
