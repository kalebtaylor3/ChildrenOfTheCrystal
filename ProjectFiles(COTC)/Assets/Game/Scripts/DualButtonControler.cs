using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System;

public class DualButtonControler : MonoBehaviour
{

    int buttonsPressed = 0;
    [SerializeField] private UnityEvent trigger;

    private void OnEnable()
    {
        DimensionButton.OnPress += Increase;
        DimensionButton.OnLeave += Decrease;
    }

    private void OnDisable()
    {
        DimensionButton.OnPress -= Increase;
        DimensionButton.OnLeave -= Decrease;
    }

    private void Update()
    {
        Debug.Log(buttonsPressed);

        if (buttonsPressed == 2)
            trigger.Invoke();

    }

    void Increase()
    {
        buttonsPressed = buttonsPressed + 1;
    }

    void Decrease()
    {
        if(buttonsPressed - 1 !=0)
        {
            buttonsPressed = buttonsPressed - 1;
        }
        else
        {
            buttonsPressed = 0;
        }
    }
}

