using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TriggerBlocks : MonoBehaviour
{
    [SerializeField] private UnityEvent trigger;

    public Transform playerposition;

    public PlayerMove[] players;
    bool happenOnce = false;

    private void OnTriggerEnter(Collider other)
    {

        if (!happenOnce)
        {
            trigger.Invoke();
            players[0].transform.position = new Vector3(playerposition.position.x, playerposition.position.y, playerposition.position.z);
            players[1].transform.position = new Vector3(playerposition.position.x + 1, playerposition.position.y, playerposition.position.z);
            happenOnce = true;
        }
    }
}
