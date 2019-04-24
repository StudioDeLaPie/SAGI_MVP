using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TextLevel : MonoBehaviour
{
    public TextMeshProUGUI textLevel;

    private void Start()
    {
        textLevel.text = "Level " + SceneManager.GetActiveScene().buildIndex;
    }

    void Update()
    {
        textLevel.text = "Level " + SceneManager.GetActiveScene().buildIndex;
    }
}
