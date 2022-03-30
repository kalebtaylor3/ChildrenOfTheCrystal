using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TriggerTimer : MonoBehaviour
{

    [SerializeField] private UnityEvent trigger;
    // Start is called before the first frame update

    private void OnTriggerEnter(Collider other)
    {
        trigger.Invoke();
    }
}
