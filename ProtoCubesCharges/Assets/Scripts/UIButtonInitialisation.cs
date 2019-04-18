using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIButtonInitialisation : MonoBehaviour
{
    public int numLevel;

    public TextMeshProUGUI tittle;

    void Start()
    {
        tittle.text = "Level " + numLevel;
    }

    public void LaunchLevel()
    {
        SceneManager.LoadScene(numLevel, LoadSceneMode.Single);
    }
}
