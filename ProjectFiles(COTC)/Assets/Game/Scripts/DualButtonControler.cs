using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System;

public class DualButtonControler : MonoBehaviour
{

    int buttonsPressed = 0;
    [SerializeField] private UnityEvent trigger;

    public DimensionButton[] buttons;

    #region PressdBools
    bool pressOnce1 = false;
    bool leaveOnce1 = false;
    bool pressOnce2 = false;
    bool leaveOnce2 = false;
    #endregion

    private void Update()
    {

        Debug.Log(buttonsPressed);

        if (buttonsPressed == 2)
        {
            trigger.Invoke();
            buttonsPressed = 0;
        }

        if(buttons[0].pressed)
        {
            if(!pressOnce1)
            {
                pressOnce1 = true;
                leaveOnce1 = false;
                Increase();
            }
        }
        else if(!buttons[0].pressed)
        {
            if(!leaveOnce1)
            {
                leaveOnce1 = true;
                pressOnce1 = false;
                Decrease();
            }
        }

        if(buttons[1].pressed)
        {
            if (!pressOnce2)
            {
                pressOnce2 = true;
                leaveOnce2 = false;
                Increase();
            }
        }
        else if (!buttons[1].pressed)
        {
            if (!leaveOnce2)
            {
                leaveOnce2 = true;
                pressOnce2 = false;
                Decrease();
            }
        }

        if (buttonsPressed < 0)
            buttonsPressed = 0;

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

