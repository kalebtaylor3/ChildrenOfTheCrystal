using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialougeTrigger : MonoBehaviour
{
    public Dialouge dialouge;
    bool happenOnce = false;

    private void OnTriggerEnter(Collider other)
    {
        if(!happenOnce)
        {
            FindObjectOfType<DialougeManager>().StartDialouge(dialouge);
            happenOnce = true;
        }
    }
}
