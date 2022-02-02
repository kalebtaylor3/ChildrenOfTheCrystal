using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class DimensionButton : MonoBehaviour
{
    public bool isSwitch;

    [SerializeField] private UnityEvent trigger;
    [SerializeField] private UnityEvent Animation;

    private void OnTriggerEnter(Collider other)
    {
        if(!isSwitch)
        {
            Animation.Invoke();
            StartCoroutine(WaitForButton());
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (isSwitch)
        {
            if (Input.GetKey(KeyCode.E))
            {
                Animation.Invoke();
                trigger.Invoke();
            }
        }
    }

    public IEnumerator WaitForButton()
    {
        yield return new WaitForSeconds(0.8f);
        trigger.Invoke();
    }
}
