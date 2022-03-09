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
            if (other.gameObject.tag == "Player")
            {
                Player1 p1 = other.GetComponent<Player1>();

                if (p1)
                {
                    givePower.Invoke();
                    this.gameObject.SetActive(false);
                }
            }
            else
            {
                return;
            }
        }

        if (player2)
        {
            if (other.gameObject.tag == "Player")
            {
                Player2 p2 = other.GetComponent<Player2>();

                if (p2)
                {
                    givePower.Invoke();
                    this.gameObject.SetActive(false);
                }
            }
            else
            {
                return;
            }
        }
    }
}