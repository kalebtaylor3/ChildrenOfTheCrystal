using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaitForCredits : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(CallCredits());
    }

    IEnumerator CallCredits()
    {
        yield return new WaitForSeconds(6);
        Application.LoadLevel(1);
    }
}
