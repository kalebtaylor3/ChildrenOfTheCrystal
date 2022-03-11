using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TrapDoor : MonoBehaviour
{

    [SerializeField] private UnityEvent trigger;
    [SerializeField] private UnityEvent text;
    // Start is called before the first frame update
    private void OnTriggerEnter(Collider other)
    {
        StartCoroutine(waitForText());
        text.Invoke();
    }

    IEnumerator waitForText()
    {
        yield return new WaitForSeconds(3);
        trigger.Invoke();
    }
}
