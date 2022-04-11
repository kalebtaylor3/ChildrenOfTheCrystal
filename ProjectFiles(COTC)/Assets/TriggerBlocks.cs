using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TriggerBlocks : MonoBehaviour
{
    [SerializeField] private UnityEvent trigger;
    int playerCount = 0;

    private void OnTriggerEnter(Collider other)
    {
        bool happenOnce = false;
        playerCount++;
        Debug.Log(playerCount);

        if(playerCount == 2)
            trigger.Invoke();
    }

    private void OnTriggerExit(Collider other)
    {
        playerCount--;
    }
}
