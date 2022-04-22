using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class deletewallsound : MonoBehaviour
{
    public GameObject soundObj;

    private void OnTriggerEnter(Collider other)
    {
        Destroy(soundObj);
    }
}
