using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class DialougeTrigger : MonoBehaviour
{
    public Dialouge dialouge;
    bool happenOnce = false;
    public bool trap;
    private DialougeManager manger;
    public bool twoWay;
    public Dialouge nextDialouge;
    bool diplay2Once = false;

    [SerializeField] private UnityEvent trigger;
    public void NextDialouge()
    {
        if (manger)
        {
            if (trap)
                trigger.Invoke();
            else if (!diplay2Once)
            {
                if (twoWay)
                    manger.StartDialouge(nextDialouge);
                else
                    return;

                diplay2Once = true;
            }
        }
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
