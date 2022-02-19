using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Player1 : PlayerMove
{
    //public GameObject[] Dimensions;
    private bool direction = true;

    private DoubleJumpEnabler doubleJump;


    private void Start()
    {
        doubleJump = GetComponent<DoubleJumpEnabler>();
    }

    private void Update()
    {

        Physics.IgnoreLayerCollision(0, 2);
        Physics.IgnoreLayerCollision(3, 2);


        if (beingLifted)
            transform.SetPositionAndRotation(transform.parent.position, Quaternion.identity);

        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");

        this.CalculateMovement(this, h, v, "Jump");
        direction = checkDirection();

        if(this.dimensionalController.currentDimension == Dimension.Dimensions.Green)
        {
            EnableJump();
        }
        else
        {
            DisableJump();
        }
    }

    private void FixedUpdate()
    {
        this.Move(this);

        if (Input.GetKey(KeyCode.LeftControl))
        {
            if (direction)
                this.pickup(Vector3.right);
            else
                this.pickup(Vector3.left);
        }

        if (Input.GetKey(KeyCode.Space))
        {
            this.ThrowPlayer();
        }
    }

    void EnableJump()
    {
        doubleJump.enabled = true;
    }

    void DisableJump()
    {
        doubleJump.enabled = false;
    }

}
