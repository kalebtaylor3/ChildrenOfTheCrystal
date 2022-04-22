using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TriggerTimer : MonoBehaviour
{

    [SerializeField] private UnityEvent trigger;
    public Camera mainCam;

    public float shakeInX = 0.5f;
    public float shakeInY = 0.2f;

    bool happenOnce = false;

    // Start is called before the first frame update

    private void OnTriggerEnter(Collider other)
    {
        if (happenOnce)
        {
            trigger.Invoke();
            mainCam.GetComponent<CameraFollow>().Shake(shakeInX, shakeInY);
            happenOnce = true;
        }
    }
}
