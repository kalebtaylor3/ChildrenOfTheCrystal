using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class WallStick : MonoBehaviour
{
    public bool horizontal;


    public static event Action<bool> OnWall;
    public static event Action OffWall;

    private void OnCollisionStay(Collision collision)
    {
        if(collision.gameObject.tag == "Player")
            OnWall?.Invoke(horizontal);
    }

    private void OnCollisionExit(Collision collision)
    {
        OffWall?.Invoke();
    }
}
