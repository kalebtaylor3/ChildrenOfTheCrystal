using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomizeMass : MonoBehaviour
{
    // Start is called before the first frame update
    private Rigidbody rigid;
    bool move = true;

    private void Start()
    {
        rigid = GetComponent<Rigidbody>();
        rigid.mass = Random.Range(80, 150);
        rigid.drag = Random.Range(0.02f, 0.06f);
    }

    private void Update()
    {
        if(move)
           transform.Translate(Vector3.down * Random.RandomRange(0.4f, 0.9f));
    }

    private void OnCollisionEnter(Collision collision)
    {
        move = false;
    }
}
