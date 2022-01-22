using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Dimension : MonoBehaviour
{
    public enum Dimensions
    {
        Red,
        Blue,
        Green,
        Purple,
        Main,
    };
    
    [SerializeField]
    GameObject[] hints;

    [SerializeField]
    GameObject[] dimensionList;

    public bool inDimension = false;

    public PostProcessingControll postEffects;

    public static event Action OnLeaveDimension;

    // Start is called before the first frame update
    void Awake()
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
                inDimension = true;
                postEffects.ChangeDimension(Dimensions.Green);
            }
            if (Input.GetKeyDown(KeyCode.M))
            {
                inDimension = true;
                postEffects.ChangeDimension(Dimensions.Purple);
            }
        }
        else
        {
            if (Input.GetKeyDown(KeyCode.Z))
            {
                //player1;
                DisableAll();
                OnLeaveDimension?.Invoke();
            }
            if (Input.GetKeyDown(KeyCode.X))
            {
                //player1;
                DisableAll();
                OnLeaveDimension?.Invoke();
            }
            if (Input.GetKeyDown(KeyCode.N))
            {
                //player1;
                DisableAll();
                OnLeaveDimension?.Invoke();
            }
            if (Input.GetKeyDown(KeyCode.M))
            {
                //player1;
                DisableAll();
                OnLeaveDimension?.Invoke();
            }
        }
    }

    public void Strength()
    {
        DisableAll();
        dimensionList[(int)Dimensions.Red].SetActive(true);
        inDimension = true;
        hints[(int)Dimensions.Red].SetActive(false);
        postEffects.ChangeDimension(Dimensions.Red);
    }

    public void SuperJump()
    {
        DisableAll();
        dimensionList[(int)Dimensions.Blue].SetActive(true);
        inDimension = true;
        hints[(int)Dimensions.Blue].SetActive(false);
        postEffects.ChangeDimension(Dimensions.Blue);
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
        postEffects.ChangeDimension(Dimensions.Main);
        dimensionList[(int)Dimensions.Main].SetActive(true);
        inDimension = false;
    }

}
