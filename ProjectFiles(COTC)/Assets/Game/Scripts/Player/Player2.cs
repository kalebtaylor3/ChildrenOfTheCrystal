using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player2 : PlayerMove
{
    private bool direction;
    public GameObject speedTrail;

    private void Update()
    {

        float h = Input.GetAxisRaw("HorizontalArrow");
        float v = Input.GetAxisRaw("VerticalArrow");

        this.CalculateMovement(this, h, v, "ArrowJump");
        direction = this.checkDirection();

        if(this.dimensionalController.currentDimension == Dimension.Dimensions.Blue)
        {
            SprintPower();
        }
        else
        {
            NormalSpeed();
        }

    }

    private void FixedUpdate()
    {
        this.Move(this);

        if (Input.GetKey(KeyCode.RightControl))
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

    void SprintPower()
    {
        this.accel = 250;
        speedTrail.SetActive(true);
    }
    void NormalSpeed()
    {
        this.accel = 50;
        speedTrail.SetActive(false);
    }
}
