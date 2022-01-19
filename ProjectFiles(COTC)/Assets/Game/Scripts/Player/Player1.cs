using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player1 : PlayerMove
{

    private void Update()
    {

        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");

        this.CalculateMovement(this, h, v, "Jump");
    }

    private void FixedUpdate()
    {
        this.Move(this);
    }
}
