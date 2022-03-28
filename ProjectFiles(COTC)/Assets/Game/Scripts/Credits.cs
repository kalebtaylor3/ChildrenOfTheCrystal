using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Credits : MonoBehaviour
{
    public AnimationClip animation;


    public void Start()
    {
        StartCoroutine(WaitForAnimation());
    }

    IEnumerator WaitForAnimation()
    {
        yield return new WaitForSeconds(animation.length);
        Application.LoadLevel(0);
    }
}
