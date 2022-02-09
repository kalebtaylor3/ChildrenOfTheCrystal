using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallStick : MonoBehaviour
{

    private Player2 player;
    private Rigidbody rb;

    public bool horizontal;
    float y;
    float x;

    bool climbing = false;

    private void Update()
    {
        y = Input.GetAxisRaw("VerticalArrow");
        x = Input.GetAxisRaw("HorizontalArrow");

        if (player.dimensionalController.currentDimension != Dimension.Dimensions.Yellow)
        {
            player.climbing = false;
            climbing = false;
        }

        if(climbing)
        {
            rb.useGravity = false;
            if (!horizontal)
            {
                rb.velocity = new Vector3(rb.velocity.x, 0, rb.velocity.z);
                float speedMod = y > 0 ? 0.35f : 1;
                if (Input.GetKey(KeyCode.UpArrow))
                    player.transform.position += new Vector3(0, 3 * Time.deltaTime, 0);
                if (Input.GetKey(KeyCode.DownArrow))
                    player.transform.position += new Vector3(0, -3 * Time.deltaTime, 0);
            }
            else
            {
                rb.velocity = new Vector3(0, 0, rb.velocity.z);
                float speedMod = x > 0 ? 0.35f : 1;
                if (Input.GetKey(KeyCode.RightArrow))
                    player.transform.position += new Vector3(3 * Time.deltaTime, 0, 0);
                if (Input.GetKey(KeyCode.LeftArrow))
                    player.transform.position += new Vector3(-3 * Time.deltaTime, 0, 0);
            }
        }
        else
        {
            rb.useGravity = true;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {

            player = collision.gameObject.GetComponent<Player2>();
            rb = collision.gameObject.GetComponent<Rigidbody>();

            if (player != null)
            {
                if (player.dimensionalController.currentDimension == Dimension.Dimensions.Yellow)
                {
                    //if (horizontal)
                        
                    //else
                        
                }
            }
        }
    }

    private void OnCollisionStay(Collision collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            player = collision.gameObject.GetComponent<Player2>();
            rb = collision.gameObject.GetComponent<Rigidbody>();
            if (player != null)
            {
                if (player.dimensionalController.currentDimension == Dimension.Dimensions.Yellow)
                {
                    Debug.Log(climbing);
                    player.climbing = true;
                    climbing = true;
                }
            }
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        player.climbing = false;
        climbing = false;
    }
}
