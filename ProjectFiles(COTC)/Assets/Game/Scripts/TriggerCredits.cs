using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TriggerCredits : MonoBehaviour
{
    public AudioSource music;
    public Animator animation;

    private void OnTriggerEnter(Collider other)
    {
        StartCoroutine(FadeOut(music, 3));
        StartCoroutine(waitForBlack());
    }

    IEnumerator waitForBlack()
    {
        animation.SetTrigger("black");
        yield return new WaitForSeconds(4);
        Application.LoadLevel(1);
    }

    public static IEnumerator FadeOut(AudioSource audioSource, float FadeTime)
    {
        float startVolume = audioSource.volume;

        while (audioSource.volume > 0)
        {
            audioSource.volume -= startVolume * Time.deltaTime / FadeTime;

            yield return null;
        }

        audioSource.Stop();
        audioSource.volume = startVolume;
    }
}
