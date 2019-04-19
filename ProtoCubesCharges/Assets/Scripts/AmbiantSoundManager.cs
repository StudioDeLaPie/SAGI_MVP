using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Utilities;

public class AmbiantSoundManager : MonoBehaviour
{
    public AudioSource audioSource;
    public List<AudioClip> soundsLevel = new List<AudioClip>();

    public int minSecondOfBlank = 30;
    public int maxSecondOfBlank = 60;

    void Start()
    {
        if (GameObject.FindGameObjectsWithTag("AmbiantSoundManager").Length <= 1)
        {
            DontDestroyOnLoad(gameObject);
            LaunchSound();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    IEnumerator WaitEndClip(AudioClip sound)
    {
        Debug.Log("On attend : " + sound.length + " Secondes avant le lancement de la prochaine musique");
        yield return new WaitForSeconds(sound.length);

        //La musique est terminé
        StartCoroutine(Wait(Aleatoire.AleatoireBetween(minSecondOfBlank, maxSecondOfBlank))); //On attend
    }

    IEnumerator Wait(int seconds)
    {
        Debug.Log("On attend un blanc de : " + seconds + " Secondes avant le lancement de la prochaine musique");
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

    private AudioClip NextSound()
    {
            int numSound = Aleatoire.AleatoireBetween(1, soundsLevel.Count - 1); //On selectionne un index aleatoire (sauf le premier élèment)
            AudioClip tempSound = soundsLevel[numSound]; //On stock le clip audio
            soundsLevel.RemoveAt(numSound); //On l'efface de la list
            soundsLevel.Insert(0, tempSound); //on l'insert au debut pour ne pas le réutiliser la prochaine fois

            return soundsLevel[0];
    }
}
