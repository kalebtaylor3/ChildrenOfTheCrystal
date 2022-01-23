using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player1 : PlayerMove
{
    //public GameObject[] Dimensions;
    private bool direction = true;
    [SerializeField]
    private void Update()
    {

        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");

        this.CalculateMovement(this, h, v, "Jump");
        direction = checkDirection();


    }

    private void FixedUpdate()
    {
        this.Move(this);

        if (Input.GetKey(KeyCode.LeftShift))
        {
            if (direction)
                this.pickup(Vector3.right);
            else
                this.pickup(Vector3.left);
        }

        if (Input.GetKey(KeyCode.Space))
        {
            this.ThrowPlayer(Vector3.up);
        }
    }


}
