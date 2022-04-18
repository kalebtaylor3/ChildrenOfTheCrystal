using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class BlockerEnter : MonoBehaviour
{

    public PlayerMove player1;
    public PlayerMove player2;

    bool happenOnce1 = false;
    bool happenOnce2 = false;

    int count;

    [SerializeField] private UnityEvent trigger;

    private void Update()
    {
        Debug.Log(count);

        if(count == 2)
        {
            Debug.Log("blocking");
            trigger.Invoke();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<PlayerMove>() == player1)
        {
            if (!happenOnce1)
            {
                count = count + 1;
                happenOnce1 = true;
            }
        }

        if (other.GetComponent<PlayerMove>() == player2)
        {
            if (!happenOnce2)
            {
                count = count + 1;
                happenOnce2 = true;
            }
        }
    }
}
