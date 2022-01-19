using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player2 : PlayerMove
{
    private bool direction;

    private void Update()
    {

        float h = Input.GetAxisRaw("HorizontalArrow");
        float v = Input.GetAxisRaw("VerticalArrow");

        this.CalculateMovement(this, h, v, "ArrowJump");
        direction = this.checkDirection();
    }

    private void FixedUpdate()
    {
        this.Move(this);

        if (Input.GetKey(KeyCode.RightShift))
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
