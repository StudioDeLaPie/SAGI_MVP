using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GIFPlayer : MonoBehaviour
{
    public Sprite[] frames;
    public float framesPerSecond = 15.0f;
    public Image image;

    private void Update()
    {
        if (frames.Length == 0)
        {
            Debug.LogError("Le GIF \"" + name + "\" n'a pas d'images renseignées.");
            return;
        }
        int index = (int)(Time.time * framesPerSecond);
        index = index % frames.Length;
        image.sprite = frames[index];
    }
}
