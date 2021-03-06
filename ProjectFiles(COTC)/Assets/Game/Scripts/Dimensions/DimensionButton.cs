using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System;

public class DimensionButton : MonoBehaviour
{
    public bool isSwitch;
    public bool multiButton;
    [SerializeField]
    private bool inDimension;

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

    bool played = false;

    [HideInInspector]
    public bool pulled = false;


    private void OnTriggerEnter(Collider other)
    {

        /*if(inDimension)
        {
            if(other.gameObject.layer != 3 && other.gameObject.layer != 0)
            {
                if (other.tag == "Player1" || other.tag == "Player2")
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
            }
        }
        else
        {
            if (other.tag == "Player1" || other.tag == "Player2")
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
        }*/

    }

    private void OnTriggerExit(Collider other)
    {
        if (multiButton)
        {
            happenOnce = false;
            idle.Invoke();
            pressed = false;
            played = false;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (isSwitch)
        {
            if(!happenOnce)
            {
                if (Input.GetKey(KeyCode.E) || Input.GetKey(KeyCode.P))
                {
                    Animation.Invoke();
                    trigger.Invoke();
                    happenOnce = true;
                    pulled = true;
                }
            }
        }

        if (inDimension)
        {
            if (other.gameObject.layer != 3 && other.gameObject.layer != 0)
            {
                if (other.tag == "Player")
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
            }
        }
        else
        {
            if (other.tag == "Player1" || other.tag == "Player2")
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
        }

    }

    public IEnumerator WaitForButton()
    {
        if (!played)
        {
            GetComponent<AudioSource>().Play();
            played = true;
        }
        yield return new WaitForSeconds(0.8f);
        trigger.Invoke();
    }
}
