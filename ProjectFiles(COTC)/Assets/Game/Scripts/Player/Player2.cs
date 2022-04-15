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

        Physics.IgnoreLayerCollision(0, 2);
        //Physics.IgnoreLayerCollision(3, 2);


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
            if(Input.GetKeyDown(KeyCode.RightControl) && Input.GetKey(KeyCode.RightArrow))
            {
                rb.AddForce(Vector3.right * 1500);
            }

            if (Input.GetKeyDown(KeyCode.RightControl) && Input.GetKey(KeyCode.LeftArrow))
            {
                rb.AddForce(Vector3.left * 1500);
            }
        }
        else
        {
            NormalSpeed();
        }

        if(this.dimensionalController.currentDimension != Dimension.Dimensions.Yellow)
        {
            OffWall();
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

        if (Input.GetKey(KeyCode.RightControl))
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

    void SprintPower()
    {
        this.accel = 200;
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
        animator.SetTrigger("Normal");
    }

    void ClimbWall(bool horizontal)
    {
        if(dimensionalController.currentDimension == Dimension.Dimensions.Yellow)
        {
            rb.useGravity = false;

            if (!horizontal)
            {
                animator.SetTrigger("Vert");
                rb.velocity = new Vector3(0, 0, 0);
                float speedMod = y > 0 ? 0.35f : 1;
                if (Input.GetKey(KeyCode.UpArrow))
                {
                    this.transform.position += new Vector3(0, 5 * Time.deltaTime, 0);
                    animator.SetFloat("climbSpeed", 1);
                }
                else if (Input.GetKey(KeyCode.DownArrow))
                {
                    this.transform.position += new Vector3(0, -5 * Time.deltaTime, 0);
                    animator.SetFloat("climbSpeed", 1);
                }
                else
                {
                    animator.SetFloat("climbSpeed", 0);
                }
            }

            if (horizontal)
            {
                animator.SetTrigger("Horz");
                rb.velocity = new Vector3(0, 0, 0);
                float speedMod = x > 0 ? 0.35f : 1;
                if (Input.GetKey(KeyCode.RightArrow))
                {
                    this.transform.position += new Vector3(5 * Time.deltaTime, 0, 0);
                    animator.SetFloat("climbSpeed", 1);
                }
                else if(Input.GetKey(KeyCode.LeftArrow))
                {
                    animator.SetFloat("climbSpeed", 1);
                    this.transform.position += new Vector3(-5 * Time.deltaTime, 0, 0);
                }
                else
                {
                    animator.SetFloat("climbSpeed", 0);
                }
            }
        }
    }
}
