using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreakableWall : MonoBehaviour
{
    bool hasHappened = false;
    private AudioSource breakNoise;

    [SerializeField]
    private GameObject stableWall;
    [SerializeField]
    private GameObject brokenWall;

    private void Start()
    {
        breakNoise = GetComponent<AudioSource>();

        stableWall.SetActive(true);
        brokenWall.SetActive(false);
    }

    public void BreakWall()
    {
        stableWall.SetActive(false);
        brokenWall.SetActive(true);


        if (brokenWall == true&&!hasHappened)
        {
            breakNoise.Play();
            hasHappened = true;
        }

    }
}
