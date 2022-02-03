using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorOpen : MonoBehaviour
{

    private Animator door;

    private void Start()
    {
        door = GetComponent<Animator>();
    }
    public void Open()
    {
        door.Play("DoorTemp");
    }
}
