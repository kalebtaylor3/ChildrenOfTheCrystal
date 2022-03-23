using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorOpen : MonoBehaviour
{

    private Animator door;
    private AudioSource doorSource;

    private void Start()
    {
        door = GetComponent<Animator>();
        doorSource = GetComponent<AudioSource>();
    }
    public void Open()
    {
        StartCoroutine(wait());
    }

    IEnumerator wait()
    {
        yield return new WaitForSeconds(1.5f);
        door.Play("DoorTemp");
        doorSource.Play();
    }
}
