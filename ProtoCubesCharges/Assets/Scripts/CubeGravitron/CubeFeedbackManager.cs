using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utilities;

public class CubeFeedbackManager : MonoBehaviour
{
    public GameObject go_hologramme;
    public GameObject go_materialise;

    public GameObject go_parentFleches;
    public GameObject go_parentCharges;

    public Material defaultFlecheMaterial;
    public Material transparentFlecheMaterial;
    public List<GameObject> go_list_fleches;

    public Material defaultChargeMaterial;
    public Material transparentChargeMaterial;
    public List<GameObject> go_list_charges;

    public List<Material> materials;

    private bool _materialise;
    private int _nbCharges;
    private int _poids;
    private MeshRenderer _hologrammeMesh;
    private Light _light;

    private float _transparencyPercentage;
    private bool _isTransparent = false;
    private float lastTransparencyUpdate;

    public bool Materialise
    {
        set
        {
            bool _prevMaterialise = _materialise;
            _materialise = value;

            if (_materialise && !_prevMaterialise) //Materialisation
            {
                Materialisation();

            }
            else if (!_materialise && _prevMaterialise) //Dematerialisation
            {
                Dematerialisation();
            }
        }
    }

    public int Charges
    {
        set
        {
            _nbCharges = value;
            UpdateCubeFeedback();
        }
    }

    public int Poids
    {
        set
        {
            _poids = value;
            UpdateCubeFeedback();
        }
    }

    public float TransparencyPercentage
    {
        set
        {
            _transparencyPercentage = value;
            lastTransparencyUpdate = Time.time;
            if (!_isTransparent)
            {
                _isTransparent = true;
                StartCoroutine(TransparencyCoroutine());
            }
        }
    }

    public void Init(int nbCharges, int poids, bool materialised)
    {
        _hologrammeMesh = go_hologramme.GetComponentInChildren<MeshRenderer>();
        _light = GetComponentInChildren<Light>();

        Materialise = materialised;
        _nbCharges = nbCharges;
        _poids = poids;
        UpdateCubeFeedback();

        transparentFlecheMaterial = new Material(transparentFlecheMaterial);
        transparentChargeMaterial = new Material(transparentChargeMaterial);
    }

    private void UpdateCubeFeedback()
    {
        UpdateFlechesPoids();
        UpdateCharges();
        UpdateMaterial();
        UpdateLight();
    }

    private IEnumerator TransparencyCoroutine()
    {
        //Initialisation
        List<Outline> outlines = new List<Outline>();
        foreach (GameObject go in go_list_fleches) //MAJ matériau flèches + récupération des outlines
            foreach (MeshRenderer mesh in go.GetComponentsInChildren<MeshRenderer>())
            {
                mesh.material = transparentFlecheMaterial;
                outlines.Add(mesh.GetComponent<Outline>());
            }

        foreach (GameObject charge in go_list_charges) //MAJ matériau charges
            charge.GetComponent<MeshRenderer>().material = transparentChargeMaterial;
        Color outlineColor = outlines[0].OutlineColor;

        //MAJ transparence
        while (_isTransparent && Time.time < lastTransparencyUpdate + 0.5)
        {
            transparentFlecheMaterial.color = new Color(transparentFlecheMaterial.color.r, transparentFlecheMaterial.color.g, transparentFlecheMaterial.color.b, _transparencyPercentage);
            transparentChargeMaterial.color = new Color(transparentChargeMaterial.color.r, transparentChargeMaterial.color.g, transparentChargeMaterial.color.b, _transparencyPercentage);
            foreach (Outline outline in outlines)
                outline.OutlineColor = new Color(outlineColor.r, outlineColor.g, outlineColor.b, _transparencyPercentage);
            yield return null;
        }

        //Fin
        foreach (GameObject go in go_list_fleches) //Reset matériaux fleches
            foreach (MeshRenderer mesh in go.GetComponentsInChildren<MeshRenderer>())
                mesh.material = defaultFlecheMaterial;
        foreach (Outline outline in outlines) //Reset outlines fleches
            outline.OutlineColor = new Color(outlineColor.r, outlineColor.g, outlineColor.b, 100);
        foreach (GameObject charge in go_list_charges) //Reset matériaux charges
            charge.GetComponent<MeshRenderer>().material = defaultChargeMaterial;

        _isTransparent = false;
    }

    private void Materialisation()
    {
        //Change l'aspect du cube
        go_hologramme.SetActive(false);
        go_materialise.SetActive(true);

        //Avance les fleches et charges pour qu'elles sortent du cube
        go_parentFleches.transform.Translate(0, 0, (transform.root.localScale.x * CONST.ROOT3) / 2f, Space.Self);
        go_parentCharges.transform.Translate(0, 0, (transform.root.localScale.x * CONST.ROOT3) / 2f, Space.Self);
    }

    private void Dematerialisation()
    {
        //Change l'aspect du cube
        go_hologramme.SetActive(true);
        go_materialise.SetActive(false);

        //Remet les fleches et charges au centre du cube
        go_parentFleches.transform.position = transform.position;
        go_parentCharges.transform.position = transform.position;

        //Désactive la transparence des flèches et charges
        _isTransparent = false;
    }

    private void UpdateCharges()
    {
        foreach (GameObject go in go_list_charges)
        {
            go.SetActive(false);
        }

        for (int i = 0; i < _nbCharges; i++)
        {
            go_list_charges[i].SetActive(true);
            go_list_charges[i].transform.SetPositionAndRotation(go_parentCharges.transform.position, go_parentCharges.transform.rotation);
            go_list_charges[i].transform.Translate(0, 0.35f * transform.localScale.y, 0, Space.Self);
            go_list_charges[i].transform.RotateAround(go_parentCharges.transform.position, go_list_charges[i].transform.forward, i * (360 / _nbCharges));
        }
    }

    private void UpdateFlechesPoids()
    {
        foreach (GameObject go in go_list_fleches)
        {
            go.SetActive(false);
        }

        if (_poids == 0)
        {
            go_list_fleches[0].SetActive(true);
        }
        else
        {
            int index = Mathf.Abs(_poids);
            go_list_fleches[index].SetActive(true);

            float orientation = 90 * (_poids / Mathf.Abs(_poids)); //récupère le signe du poids

            go_list_fleches[index].transform.localRotation = Quaternion.identity;
            go_list_fleches[index].transform.Rotate(0, 0, orientation, Space.Self);
        }
    }

    private void UpdateMaterial()
    {
        _hologrammeMesh.material = materials[_nbCharges];
    }

    private void UpdateLight()
    {
        _light.color = materials[_nbCharges].color;
    }

}
