using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System;

public class DimensionButton : MonoBehaviour
{
    public bool isSwitch;
    public bool multiButton;

    [HideInInspector]
    public bool pressed = false;

    bool happenOnce = false;

    [SerializeField] private UnityEvent trigger;
    [SerializeField] private UnityEvent Animation;


    [Header("Dual Button Triggers")]
    [TextArea]
    [Tooltip("Doesn't do anything. Just comments shown in inspector")]
    public string _ = "Only set these triggers if the button will be used as one of the 2 multi buttons";
    [SerializeField] public UnityEvent idle;
    [SerializeField] private UnityEvent idlePress;

    private void OnTriggerEnter(Collider other)
    {
        if (!isSwitch)
        {
            Animation.Invoke();
            StartCoroutine(WaitForButton());
        }


        if (multiButton && !happenOnce)
        {
            pressed = true;
            happenOnce = true;
        }

    }

    private void OnTriggerExit(Collider other)
    {
        if (multiButton)
        {
            happenOnce = false;
            idle.Invoke();
            pressed = false;
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
    }

    public IEnumerator WaitForButton()
    {
        yield return new WaitForSeconds(0.8f);
        trigger.Invoke();
    }
}
