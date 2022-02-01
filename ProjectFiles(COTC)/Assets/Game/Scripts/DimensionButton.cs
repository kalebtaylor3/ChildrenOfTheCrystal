using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class DimensionButton : MonoBehaviour
{
    [SerializeField] private UnityEvent trigger;
    [SerializeField] private UnityEvent Animation;


    private void OnTriggerEnter(Collider other)
    {
        Animation.Invoke();
        StartCoroutine(WaitForButton());
    }

    public IEnumerator WaitForButton()
    {
        yield return new WaitForSeconds(0.8f);
        trigger.Invoke();
    }
}
