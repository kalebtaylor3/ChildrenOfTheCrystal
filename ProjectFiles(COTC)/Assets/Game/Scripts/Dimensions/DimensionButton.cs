using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System;

public class DimensionButton : MonoBehaviour
{
    public bool isSwitch;
    public bool multiButton;

    public static event Action OnPress;
    public static event Action OnLeave;
    bool happenOnce = false;

    [SerializeField] private UnityEvent trigger;
    [SerializeField] private UnityEvent Animation;

    [SerializeField] private UnityEvent idle;

    private void OnTriggerEnter(Collider other)
    {
        if(!isSwitch)
        {
            Animation.Invoke();
            StartCoroutine(WaitForButton());
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(multiButton)
        {
            OnLeave?.Invoke();
            happenOnce = false;
            idle.Invoke();
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (isSwitch)
        {
            if (Input.GetKey(KeyCode.E) || Input.GetKey(KeyCode.P))
            {
                Animation.Invoke();
                trigger.Invoke();
            }
        }

        if (multiButton && !happenOnce)
        {
            OnPress?.Invoke();
            happenOnce = true;
        }
    }

    public IEnumerator WaitForButton()
    {
        yield return new WaitForSeconds(0.8f);
        trigger.Invoke();
    }
}
