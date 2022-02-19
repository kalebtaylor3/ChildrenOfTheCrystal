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

        StartCoroutine(DestroyChildren());
        SetLayerRecursively(brokenWall, 2);

        if (brokenWall == true&&!hasHappened)
        {
            breakNoise.Play();
            hasHappened = true;
        }

    }

    public IEnumerator DestroyChildren()
    {
        Debug.Log("wall called");
        yield return new WaitForSeconds(3);
        brokenWall.SetActive(false);
    }

    public void SetLayerRecursively(GameObject obj, int newLayer)
    {
        if (null == obj)
        {
            return;
        }

        obj.layer = newLayer;

        foreach (Transform child in obj.transform)
        {
            SetLayerRecursively(child.gameObject, newLayer);
        }
    }
}
