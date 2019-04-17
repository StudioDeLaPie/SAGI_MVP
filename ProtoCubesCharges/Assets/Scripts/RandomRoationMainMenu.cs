using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Utility;
using Utilities;

public class RandomRoationMainMenu : MonoBehaviour
{
    public float minRotation = 1;
    public float maxRotation = 8;

    void Start()
    {
        GetComponent<AutoMoveAndRotate>().rotateDegreesPerSecond.value = 
        new Vector3(Aleatoire.AleatoireBetweenFloat(minRotation, maxRotation), 
                    Aleatoire.AleatoireBetweenFloat(minRotation, maxRotation), 
                    Aleatoire.AleatoireBetweenFloat(minRotation, maxRotation));       
    }
}
