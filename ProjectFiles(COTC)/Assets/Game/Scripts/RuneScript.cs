using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class RuneScript : MonoBehaviour
{
    [SerializeField] private UnityEvent givePower;

    [SerializeField]
    private bool player2;


    private void OnTriggerEnter(Collider other)
    {
        if (!player2)
        {
            if (other.gameObject.tag == "Player1")
            {
                givePower.Invoke();
                this.gameObject.SetActive(false);
            }
            else
            {
                return;
            }
        }

        if (player2)
        {
            if (other.gameObject.tag == "Player2")
            {
                givePower.Invoke();
                this.gameObject.SetActive(false);
            }
            else
            {
                return;
            }
        }
    }
}
