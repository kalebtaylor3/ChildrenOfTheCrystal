using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TriggerBlocks : MonoBehaviour
{
    [SerializeField] private UnityEvent trigger;

    private void OnTriggerEnter(Collider other)
    {
        trigger.Invoke();
    }
}
