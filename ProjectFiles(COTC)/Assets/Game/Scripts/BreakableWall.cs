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

        foreach (Transform child in brokenWall.transform)
        {
            StartCoroutine(DestroyChildren(child.gameObject));

        }

        if (brokenWall == true&&!hasHappened)
        {
            breakNoise.Play();
            hasHappened = true;
        }

    }

    IEnumerator DestroyChildren(GameObject child)
    {
        yield return new WaitForSeconds(3);
        Destroy(child);
    }
}
