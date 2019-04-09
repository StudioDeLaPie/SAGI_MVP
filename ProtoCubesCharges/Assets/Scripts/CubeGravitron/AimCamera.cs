using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;
using Utilities;

/// <summary>
/// Fait transform.LookAt(mainCamera)
/// </summary>
public class AimCamera : MonoBehaviour
{
    void Update()
    {
        transform.LookAt(Camera.main.transform);
    }
}
