using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dimension : MonoBehaviour
{

    public enum Dimensions
    {
        Red,
        Main,
        Hints,
        Blue,
        Green
    };

    [SerializeField]
    GameObject[] dimensionList;
    public int a = 1;

    private bool inDimension = false;

    // Start is called before the first frame update
    void Start()
    {
        DisableAll();
    }

    private void Update()
    {
        if (!inDimension)
        {
            if (Input.GetKeyDown(KeyCode.Z))
            {
                //player1;
                Strength();
            }
            if (Input.GetKeyDown(KeyCode.X))
            {
                //player1;
                SuperJump();
            }

            if (Input.GetKeyDown(KeyCode.N))
            {
                //player1;
                Strength();
            }
            if (Input.GetKeyDown(KeyCode.M))
            {
                //player1;
                SuperJump();
            }
        }
        else
        {
            if (Input.GetKeyDown(KeyCode.Z))
            {
                //player1;
                DisableAll();
            }
            if (Input.GetKeyDown(KeyCode.X))
            {
                //player1;
                DisableAll();
            }

            if (Input.GetKeyDown(KeyCode.N))
            {
                //player1;
                DisableAll();
            }
            if (Input.GetKeyDown(KeyCode.M))
            {
                //player1;
                DisableAll();
            }
        }
    }

    public void Strength()
    {
        if (!inDimension)
        {
            DisableAll();
            dimensionList[(int)Dimensions.Red].SetActive(true);
            inDimension = true;
        }
    }

    public void SuperJump()
    {
        if (!inDimension)
        {
            DisableAll();
            dimensionList[(int)Dimensions.Blue].SetActive(true);
            inDimension = true;
        }
    }

    public void DisableAll()
    {
        for (int i = 0; i < dimensionList.Length; i++)
        {
            dimensionList[i].SetActive(false);
        }
        dimensionList[(int)Dimensions.Main].SetActive(true);
        dimensionList[(int)Dimensions.Hints].SetActive(true);
        inDimension = false;
    }

}
