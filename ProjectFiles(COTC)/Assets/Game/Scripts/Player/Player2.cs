using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player2 : PlayerMove
{
    private void Update()
    {

        float h = Input.GetAxisRaw("HorizontalArrow");
        float v = Input.GetAxisRaw("VerticalArrow");

        this.CalculateMovement(this, h, v, "ArrowJump");
    }

    private void FixedUpdate()
    {
        this.Move(this);
    }
}
