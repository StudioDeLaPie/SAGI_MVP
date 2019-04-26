using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlaqueMort : MonoBehaviour
{
    private SoundManagerPlayer soundManagerPlayer;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.tag == "Player")
        {
            soundManagerPlayer = collision.transform.root.GetComponentInChildren<SoundManagerPlayer>();

            StartCoroutine(LaunchDeath());
        }
    }

    private IEnumerator LaunchDeath()
    {
        GameObject player = soundManagerPlayer.transform.root.gameObject;

        player.GetComponent<Rigidbody>().isKinematic = true; //On fige le player
        player.GetComponent<PlayerMovementController>().enabled = false;//On l'empêche de bouger (surout pour les sons de pas)

        soundManagerPlayer.PlayOneShotMort();//On joue le son de mort

        player.GetComponentInChildren<BlackScreenTransition>().FadeIn(); //On effectue un fondu

        while (soundManagerPlayer.AudioSourceMort.isPlaying) //tant que le son n'est pas finit
        {
            yield return null;//On skip
        }

        //Et ensuite on recharge la scène
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex, LoadSceneMode.Single);
    }
}
