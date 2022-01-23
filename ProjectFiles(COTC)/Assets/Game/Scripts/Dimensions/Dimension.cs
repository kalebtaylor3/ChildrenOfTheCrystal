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

    [HideInInspector]
    public bool inDimension = false;

    public PostProcessingControll postEffects;

    public PlayerMove[] playersForLayers;

    // Start is called before the first frame update
    void Start()
    {
        postEffects.ChangeDimension(Dimensions.Main);
        dimensionList[(int)Dimensions.Main].SetActive(true);
    }

    private void Update()
    {
        if (!inDimension)
        {
            if (Input.GetKeyDown(KeyCode.Z))
                Strength();
            if (Input.GetKeyDown(KeyCode.X))
                SuperJump();
            if (Input.GetKeyDown(KeyCode.N))
                Green();
            if (Input.GetKeyDown(KeyCode.M))
                Purple();
        }
        else
        {
            if (Input.GetKeyDown(KeyCode.Z))
                DisableAll();
            if (Input.GetKeyDown(KeyCode.X))
                DisableAll();
            if (Input.GetKeyDown(KeyCode.N))
                DisableAll();
            if (Input.GetKeyDown(KeyCode.M))
                DisableAll();
        }

        Physics.IgnoreLayerCollision(3, 9);
        Physics.IgnoreLayerCollision(3, 10);
        Physics.IgnoreLayerCollision(3, 11);
        Physics.IgnoreLayerCollision(3, 12);
    }

    public void Strength()
    {
        DisableAll();
        dimensionList[(int)Dimensions.Red].SetActive(true);
        inDimension = true;
        hints[(int)Dimensions.Red].SetActive(false);
        postEffects.ChangeDimension(Dimensions.Red);
        playersForLayers[1].SetLayerRecursively(playersForLayers[1].gameObject, 3);
    }

    public void SuperJump()
    {
        DisableAll();
        dimensionList[(int)Dimensions.Blue].SetActive(true);
        inDimension = true;
        hints[(int)Dimensions.Blue].SetActive(false);
        postEffects.ChangeDimension(Dimensions.Blue);
        playersForLayers[1].SetLayerRecursively(playersForLayers[1].gameObject, 3);
    }

    public void Purple()
    {
        DisableAll();
        inDimension = true;
        postEffects.ChangeDimension(Dimensions.Purple);
        playersForLayers[0].SetLayerRecursively(playersForLayers[0].gameObject, 3);
    }

    public void Green()
    {
        DisableAll();
        inDimension = true;
        postEffects.ChangeDimension(Dimensions.Green);
        playersForLayers[0].SetLayerRecursively(playersForLayers[0].gameObject, 3);
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
        playersForLayers[0].SetLayerRecursively(playersForLayers[0].gameObject, 0);
        playersForLayers[1].SetLayerRecursively(playersForLayers[1].gameObject, 0);
        inDimension = false;
    }

}
