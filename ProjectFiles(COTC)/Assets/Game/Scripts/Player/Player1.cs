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
        //Physics.IgnoreLayerCollision(3, 2);
        Physics.IgnoreLayerCollision(13, 3);
        Physics.IgnoreLayerCollision(13, 13);


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

        if (playerbeinglifted != null)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                aSource.clip = throwSound;
                aSource.volume = 0.8f;
                aSource.Play();
            }
        }

        //float previousX = transform.position.x;

        //if (transform.position.z == 0)
        //    previousX = transform.position.x;

        //if (transform.position.z != 0)
        //{
        //    Vector3 newPosition = transform.position;
        //    newPosition.x = previousX;
        //    newPosition.z = 0;
        //    transform.position = newPosition;
        //}

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
