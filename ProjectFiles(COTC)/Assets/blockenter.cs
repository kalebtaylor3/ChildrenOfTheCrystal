using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class blockenter : MonoBehaviour
{
    public PlayerMove player1;
    public PlayerMove player2;

    bool happenOnce1 = false;
    bool happenOnce2 = false;

    int count;

    [SerializeField] private UnityEvent trigger;

    public bool forcePlayer;
    bool dontHappenAgain = false;

    private void Update()
    {
        Debug.Log(count);

        if (count == 2)
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
                if (forcePlayer && !dontHappenAgain)
                {
                    player2.transform.position = other.transform.position + new Vector3(5, 0, 0);
                    dontHappenAgain = true;
                }
                happenOnce1 = true;
            }
        }

        if (other.GetComponent<PlayerMove>() == player2)
        {
            if (!happenOnce2)
            {
                count = count + 1;
                if (forcePlayer && !dontHappenAgain)
                {
                    player1.transform.position = other.transform.position + new Vector3(5, 0, 0);
                    dontHappenAgain = true;
                }
                happenOnce2 = true;
            }
        }
    }
}
