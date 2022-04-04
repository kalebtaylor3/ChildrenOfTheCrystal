using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fadeMusicIn : MonoBehaviour
{

    public AudioSource audio;
    private float audio2Volume;

    private void Start()
    {
		StartCoroutine(FadeIn(audio, 15));
    }

    public static IEnumerator FadeIn(AudioSource audioSource, float FadeTime)
	{
		audioSource.Play();
		audioSource.volume = 0f;
		while (audioSource.volume < 0.132f)
		{
			audioSource.volume += Time.deltaTime / FadeTime;
			yield return null;
		}
	}
}
