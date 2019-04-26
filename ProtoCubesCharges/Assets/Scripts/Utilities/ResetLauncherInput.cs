using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetLauncherInput : MonoBehaviour
{
    void Update()
    {
        if(Input.GetKey(KeyCode.R) && (Input.GetKey(KeyCode.LeftControl) || Input.GetKey(KeyCode.RightControl)))
            PlayerPrefs.DeleteAll();
    }
}
