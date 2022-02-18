using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Player2 : PlayerMove
{
    private bool direction;
    public GameObject speedTrail;

    private Rigidbody rb;

    float y;
    float x;
    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void OnEnable()
    {
        WallStick.OnWall += ClimbWall;
        WallStick.OffWall += OffWall;
    }

    private void Update()
    {

        if (beingLifted)
        {
            transform.SetPositionAndRotation(transform.parent.position, Quaternion.identity);
        }

        y = Input.GetAxisRaw("VerticalArrow");
        x = Input.GetAxisRaw("HorizontalArrow");

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

        if(this.dimensionalController.currentDimension != Dimension.Dimensions.Yellow)
        {
            OffWall();
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


    void OffWall()
    {
        rb.useGravity = true;
    }

    void ClimbWall(bool horizontal)
    {
        if(dimensionalController.currentDimension == Dimension.Dimensions.Yellow)
        {
            rb.useGravity = false;

            if (!horizontal)
            {
                rb.velocity = new Vector3(0, 0, 0);
                float speedMod = y > 0 ? 0.35f : 1;
                if (Input.GetKey(KeyCode.UpArrow))
                    this.transform.position += new Vector3(0, 3 * Time.deltaTime, 0);
                if (Input.GetKey(KeyCode.DownArrow))
                    this.transform.position += new Vector3(0, -3 * Time.deltaTime, 0);

            }

            if (horizontal)
            {
                rb.velocity = new Vector3(0, 0, 0);
                float speedMod = x > 0 ? 0.35f : 1;
                if (Input.GetKey(KeyCode.RightArrow))
                    this.transform.position += new Vector3(3 * Time.deltaTime, 0, 0);
                if (Input.GetKey(KeyCode.LeftArrow))
                    this.transform.position += new Vector3(-3 * Time.deltaTime, 0, 0);
            }
        }
    }
}
