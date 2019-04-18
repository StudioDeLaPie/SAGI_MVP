using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelSelector : MonoBehaviour
{
    public GameObject cellulePref;
    public GameObject grid;

    private int nbLevel;

    private void Start()
    {
        Initialisation();

        CreationCellules();
    }

    private void CreationCellules()
    {
        GameObject cel;

        for (int numLevel = 1; numLevel < nbLevel+1; numLevel++)
        {
            cel = GameObject.Instantiate(cellulePref);
            cel.transform.SetParent(grid.transform);
            cel.name = "Cellule Level " + numLevel;
            cel.GetComponent<UIButtonInitialisation>().numLevel = numLevel;
        }
    }

    private void Initialisation()
    {
        nbLevel = SceneManager.sceneCountInBuildSettings -3;
        if (nbLevel < 0) Debug.LogError("Level Selector: Le nombre de level est négatif");
    }
}
