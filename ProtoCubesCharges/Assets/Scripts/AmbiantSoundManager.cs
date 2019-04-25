using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Utilities;

public class AmbiantSoundManager : MonoBehaviour
{
    public AudioSource audioSource;
    public List<AudioClip> soundsLevel = new List<AudioClip>();
    public List<AudioClip> soundsMenu = new List<AudioClip>();

    public int minSecondOfBlank = 30;
    public int maxSecondOfBlank = 60;

    void Start()
    {
        if (GameObject.FindGameObjectsWithTag("AmbiantSoundManager").Length <= 1)
        {
            DontDestroyOnLoad(gameObject);
            LaunchFirstSound();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    IEnumerator WaitEndClip(AudioClip sound)
    {
        //Debug.Log("On attend : " + sound.length + " Secondes avant le lancement de la prochaine musique");
        yield return new WaitForSeconds(sound.length);

        //La musique est terminé
        StartCoroutine(Wait(Aleatoire.AleatoireBetween(minSecondOfBlank, maxSecondOfBlank))); //On attend
    }

    IEnumerator Wait(int seconds)
    {
        //Debug.Log("On attend un blanc de : " + seconds + " Secondes avant le lancement de la prochaine musique");
        yield return new WaitForSeconds(seconds);

        //On a finit d'attendre
        LaunchSound();
    }

    private void LaunchSound()
    {
        //Debug.Log("Lacement du son");
        AudioClip clip = NextSound();
        
            audioSource.clip = clip;
            audioSource.Play();
            StartCoroutine(WaitEndClip(clip));
    }

    /// <summary>
    /// Lance la toute première musique dans le menu Start
    /// Pour avoir des musiques particulière et sans fondu entrant
    /// </summary>
    private void LaunchFirstSound()
    {
        //Debug.Log("Lacement du son");
        AudioClip clip = FirstSound();

        audioSource.clip = clip;
        audioSource.Play();
        StartCoroutine(WaitEndClip(clip));
    }


    private AudioClip NextSound()
    {
            int numSound = Aleatoire.AleatoireBetween(1, soundsLevel.Count - 1); //On selectionne un index aleatoire (sauf le premier élèment)
            AudioClip tempSound = soundsLevel[numSound]; //On stock le clip audio
            soundsLevel.RemoveAt(numSound); //On l'efface de la list
            soundsLevel.Insert(0, tempSound); //on l'insert au debut pour ne pas le réutiliser la prochaine fois

            return soundsLevel[0];
    }

    private AudioClip FirstSound()
    {
        int numSound = Aleatoire.AleatoireBetween(0, soundsMenu.Count - 1); //On selectionne un index aleatoire (sauf le premier élèment)
        AudioClip tempSound = soundsMenu[numSound]; //On stock le clip audio
        soundsMenu.RemoveAt(numSound); //On l'efface de la list
        soundsMenu.Insert(0, tempSound); //on l'insert au debut pour ne pas le réutiliser la prochaine fois

        return soundsMenu[0];
    }
}
