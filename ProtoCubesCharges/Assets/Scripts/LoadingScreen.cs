using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoadingScreen : MonoBehaviour
{
    public Image loadingScreenImage;
    public float transitionSpeed;

    [HideInInspector] public bool isOnTransition = false;

    // Start is called before the first frame update
    void Start()
    {
        FadeOut();
    }

    /// <summary>
    /// Disparition de l'écran noir
    /// </summary>
    private void FadeOut()
    {
        isOnTransition = true;
        StartCoroutine(Fade(false));
    }

    /// <summary>
    /// Apparition de l'écran noir
    /// </summary>
    public void FadeIn()
    {
        isOnTransition = true;
        StartCoroutine(Fade(true));
    }

    private IEnumerator Fade(bool apparition)
    {
        while (isOnTransition)
        {
            Color _color = loadingScreenImage.color;
            float speed = transitionSpeed * Time.deltaTime;
            loadingScreenImage.color = new Color(_color.r, _color.g, _color.b, apparition ? _color.a + speed : _color.a - speed);
            isOnTransition = apparition ? loadingScreenImage.color.a < 1 : loadingScreenImage.color.a > 0;
            yield return null;
        }
    }
}
