using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingRocks : MonoBehaviour
{

    public GameObject rock;
    // Update is called once per frame


    private void Start()
    {
        InvokeRepeating("Spawn", Random.Range(0,15), Random.Range(0, 15));
    }

    void Spawn()
    {
        //StartCoroutine(WaitForSpawn());
        Instantiate(rock, transform.position, Quaternion.identity);
    }

    //IEnumerator WaitForSpawn()
    //{
    //    Instantiate(rock, transform.position, Quaternion.identity);
    //    yield return new WaitForSeconds(3);
    //}
}
