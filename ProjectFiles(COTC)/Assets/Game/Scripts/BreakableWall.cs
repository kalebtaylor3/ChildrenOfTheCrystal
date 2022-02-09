using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreakableWall : MonoBehaviour
{
    [SerializeField]
    private GameObject stableWall;
    [SerializeField]
    private GameObject brokenWall;

    private void Start()
    {
        stableWall.SetActive(true);
        brokenWall.SetActive(false);
    }

    public void BreakWall()
    {
        stableWall.SetActive(false);
        brokenWall.SetActive(true);
    }
}
