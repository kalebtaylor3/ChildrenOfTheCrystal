using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lava : MonoBehaviour
{
    [SerializeField]
    float speed;

    Renderer rend;
    
    void Start()
    {
        rend = GetComponent<Renderer>();
    }

    
    void Update()
    {
        float offset = Time.time * speed;
        rend.material.SetTextureOffset("_MainTex", new Vector2(offset, 0));
    }
}
