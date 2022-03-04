using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotate : MonoBehaviour
{
    [SerializeField] private float speed;

    void Update()
    {
        Vector3 rot = transform.localEulerAngles;
        rot.y += speed * Time.deltaTime;
        transform.localEulerAngles = rot;
    }
}
