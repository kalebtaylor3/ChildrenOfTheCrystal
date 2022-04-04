using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetDestroyed : MonoBehaviour
{
     public GameObject rock;
    private void OnTriggerEnter(Collider other)
    {     
            Destroy(rock);
    }
    private void OnCollisionEnter(Collision collision)
    {
        Destroy(rock);
      //  Destroy(this);
    }
}
