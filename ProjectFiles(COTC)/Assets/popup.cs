using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class popup : MonoBehaviour
{
    public GameObject popUp;

    private void Start()
    {
        popUp.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        popUp.SetActive(true);
    }

    private void OnTriggerExit(Collider other)
    {
        popUp.SetActive(false);
    }
}
