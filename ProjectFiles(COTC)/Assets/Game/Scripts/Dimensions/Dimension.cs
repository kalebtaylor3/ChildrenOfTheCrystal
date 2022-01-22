using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dimension : MonoBehaviour
{

    public enum Dimensions
    {
        Red,
        Blue,
        Green,
        Main,
    };
    
    [SerializeField]
    GameObject[] hints;

    [SerializeField]
    GameObject[] dimensionList;
    public int a = 1;

    private bool inDimension = false;


    public Material[] player1;
    public Material[] player2;

    // Start is called before the first frame update
    void Start()
    {
        DisableAll();
        ClearColour();
    }

    private void Update()
    {
        if (!inDimension)
        {
            if (Input.GetKeyDown(KeyCode.Z))
            {
                //player1;
                Strength();
                hints[(int)Dimensions.Red].SetActive(false);
                ChangeColour(1, Color.red);
            }
            if (Input.GetKeyDown(KeyCode.X))
            {
                //player1;
                SuperJump();
                hints[(int)Dimensions.Blue].SetActive(false);
                ChangeColour(1, Color.blue);
            }

            if (Input.GetKeyDown(KeyCode.N))
            {
                //player1;
                Strength();
                ChangeColour(2, Color.red);
            }
            if (Input.GetKeyDown(KeyCode.M))
            {
                //player1;
                SuperJump();
                ChangeColour(2, Color.blue);
            }
        }
        else
        {
            if (Input.GetKeyDown(KeyCode.Z))
            {
                //player1;
                DisableAll();
                ClearColour();
            }
            if (Input.GetKeyDown(KeyCode.X))
            {
                //player1;
                DisableAll();
                ClearColour();
            }

            if (Input.GetKeyDown(KeyCode.N))
            {
                //player1;
                DisableAll();
                ClearColour();
            }
            if (Input.GetKeyDown(KeyCode.M))
            {
                //player1;
                DisableAll();
                ClearColour();
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

    void ChangeColour(int player, Color color)
    {
        if(player == 1)
        {
            for (int i = 0; i < player1.Length; i++)
            {
                player1[i].color = color;
            }
        }
        else if( player == 2)
        {
            for (int i = 0; i < player2.Length; i++)
            {
                player2[i].color = color;
            }
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

    void ClearColour()
    {
        for (int i = 0; i < player1.Length; i++)
        {
            player1[i].color = Color.white;
        }
        for (int i = 0; i < player2.Length; i++)
        {
            player2[i].color = Color.white;
        }
    }   

    public void DisableAll()
    {
        for (int i = 0; i < dimensionList.Length; i++)
        {
            dimensionList[i].SetActive(false);
        }

        for (int i = 0; i < hints.Length; i++)
        {
            hints[i].SetActive(true);
        }
        dimensionList[(int)Dimensions.Main].SetActive(true);
        inDimension = false;
    }

}
