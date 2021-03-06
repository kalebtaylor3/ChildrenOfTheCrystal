using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System;

public class DialougeTrigger : MonoBehaviour
{
    public Dialouge dialouge;
    bool happenOnce = false;
    private DialougeManager manger;
    public bool twoWay;
    public Dialouge nextDialouge;
    bool diplay2Once = false;

    public void NextDialouge()
    {
        if (manger)
        {
            if (!diplay2Once)
            {
                if (twoWay)
                    manger.StartDialouge(nextDialouge);
                else
                    return;

                diplay2Once = true;
            }
        }
    }

    private void Update()
    {

        //if(manger)
        //{
        //    if (manger.sentences.Count == 0)
        //        trigger.Invoke();
        //}
    }

    private void OnTriggerEnter(Collider other)
    {
        if(!happenOnce)
        {
            manger = FindObjectOfType<DialougeManager>();
            manger.StartDialouge(dialouge);

            happenOnce = true;
        }
    }
}
