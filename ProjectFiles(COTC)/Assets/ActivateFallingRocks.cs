using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivateFallingRocks : MonoBehaviour
{
    public GameObject RockSpawner;
    private void OnTriggerEnter(Collider other)
    {

        RockSpawner.SetActive(true);

    }
}
