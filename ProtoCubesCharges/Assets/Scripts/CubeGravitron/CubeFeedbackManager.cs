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

    public List<GameObject> go_list_fleches;
    public List<GameObject> go_list_charges;

    public List<Material> materials;

    private bool _materialise;
    private int _nbCharges;
    private int _poids;
    private MeshRenderer _hologrammeMesh;
    private Light _light;

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

    public void Init(int nbCharges, int poids, bool materialised)
    {
        _hologrammeMesh = go_hologramme.GetComponentInChildren<MeshRenderer>();
        _light = GetComponentInChildren<Light>();

        Materialise = materialised;
        _nbCharges = nbCharges;
        _poids = poids;
        UpdateCubeFeedback();
    }

    private void UpdateCubeFeedback()
    {
        UpdateFlechesPoids();
        UpdateCharges();
        UpdateMaterial();
        UpdateLight();
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
            go_list_charges[i].transform.Translate(0, 0.35f, 0, Space.Self);
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
