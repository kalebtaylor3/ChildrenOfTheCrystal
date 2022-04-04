using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetDestroyed : MonoBehaviour
{
    public GameObject rock;
    public GameObject[] spawnOnDeath;
    private void OnTriggerEnter(Collider other)
    {     
            Destroy(rock);
    }
    private void OnCollisionEnter(Collision collision)
    {
            if (spawnOnDeath.Length != 0)
                foreach (GameObject obj in spawnOnDeath)
                    Instantiate(obj, transform.position, Quaternion.Euler(Vector3.zero));

            Destroy(rock);
      //  Destroy(this);
    }
}
