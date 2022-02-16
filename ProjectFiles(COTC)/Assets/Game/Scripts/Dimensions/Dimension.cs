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
        Yellow,
        Main,
    };
    
    [SerializeField]
    GameObject[] hints;

    public Dimensions currentDimension = Dimensions.Main;

    [SerializeField]
    GameObject[] dimensionList;

    [HideInInspector]
    public bool inDimension = false;

    public PostProcessingControll postEffects;

    public PlayerMove[] playersForLayers;


    [SerializeField]
    public GameObject redRune;
    public GameObject greenRune;
    public GameObject blueRune;
    public GameObject yellowRune;

    [SerializeField]
    private bool hasRed = false;
    [SerializeField]
    private bool hasBlue = false;
    [SerializeField]
    private bool hasGreen = false;
    [SerializeField]
    private bool hasYellow = false;


    // Start is called before the first frame update
    void Start()
    {
        postEffects.ChangeDimension(Dimensions.Main);
        dimensionList[(int)Dimensions.Main].SetActive(true);

        redRune.SetActive(false);
        greenRune.SetActive(false);
        blueRune.SetActive(false);
        yellowRune.SetActive(false); // yes I know this is bad code

    }

    private void Update()
    {
        if (!inDimension)
        {
            if (Input.GetKeyDown(KeyCode.Z))
                Strength();
            if (Input.GetKeyDown(KeyCode.X))
                Green();
            if (Input.GetKeyDown(KeyCode.N))
                SuperSpeed();
            if (Input.GetKeyDown(KeyCode.M))
                Yellow();
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
        if(hasRed)
        {
            DisableAll();
            dimensionList[(int)Dimensions.Red].SetActive(true);
            inDimension = true;
            hints[(int)Dimensions.Red].SetActive(false);
            postEffects.ChangeDimension(Dimensions.Red);
            playersForLayers[1].SetLayerRecursively(playersForLayers[1].gameObject, 3);

            redRune.SetActive(true);
            greenRune.SetActive(false);
            blueRune.SetActive(false);
            yellowRune.SetActive(false);

            currentDimension = Dimensions.Red;
        }
    }

    public void SuperSpeed()
    {
        if (hasBlue)
        {
            DisableAll();
            dimensionList[(int)Dimensions.Blue].SetActive(true);
            inDimension = true;
            hints[(int)Dimensions.Blue].SetActive(false);
            postEffects.ChangeDimension(Dimensions.Blue);
            playersForLayers[0].SetLayerRecursively(playersForLayers[0].gameObject, 3);

            redRune.SetActive(false);
            greenRune.SetActive(false);
            blueRune.SetActive(true);
            yellowRune.SetActive(false);

            currentDimension = Dimensions.Blue;
        }
    }

    public void Yellow()
    {
        if (hasYellow)
        {
            DisableAll();
            dimensionList[(int)Dimensions.Yellow].SetActive(true);
            inDimension = true;
            hints[(int)Dimensions.Yellow].SetActive(false);
            postEffects.ChangeDimension(Dimensions.Yellow);
            playersForLayers[0].SetLayerRecursively(playersForLayers[0].gameObject, 3);

            redRune.SetActive(false);
            greenRune.SetActive(false);
            blueRune.SetActive(false);
            yellowRune.SetActive(true);

            currentDimension = Dimensions.Yellow;
        }
    }

    public void Green()
    {
        if (hasGreen)
        {
            dimensionList[(int)Dimensions.Green].SetActive(true);
            inDimension = true;
            hints[(int)Dimensions.Green].SetActive(false);
            postEffects.ChangeDimension(Dimensions.Green);
            playersForLayers[1].SetLayerRecursively(playersForLayers[1].gameObject, 3);

            redRune.SetActive(false);
            greenRune.SetActive(true);
            blueRune.SetActive(false);
            yellowRune.SetActive(false);

            currentDimension = Dimensions.Green;
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
        postEffects.ChangeDimension(Dimensions.Main);
        dimensionList[(int)Dimensions.Main].SetActive(true);
        playersForLayers[0].SetLayerRecursively(playersForLayers[0].gameObject, 13);
        playersForLayers[1].SetLayerRecursively(playersForLayers[1].gameObject, 13);
        inDimension = false;

        redRune.SetActive(false);
        greenRune.SetActive(false);
        blueRune.SetActive(false);
        yellowRune.SetActive(false);

        currentDimension = Dimensions.Main;
    }

    public void HasRed()
    {
        hasRed = true;
    }

    public void HasGreen()
    {
        hasGreen = true;
    }
    public void HasBlue()
    {
        hasBlue = true;
    }
    public void HasYellow()
    {
        hasYellow = true;
    }
}
