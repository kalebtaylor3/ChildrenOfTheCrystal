using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightController : MonoBehaviour
{


    public Light lightSource;
    Dimension dimensionController;

    private void Start()
    {
        dimensionController = FindObjectOfType<Dimension>();
    }

    private void Update()
    {
       switch(dimensionController.currentDimension)
        {
            case Dimension.Dimensions.Red:
                lightSource.color = Color.red;
                break;
            case Dimension.Dimensions.Blue:
                lightSource.color = Color.blue;
                break;
            case Dimension.Dimensions.Green:
                lightSource.color = Color.green;
                break;
            case Dimension.Dimensions.Yellow:
                lightSource.color = Color.yellow;
                break;
            case Dimension.Dimensions.Main:
                lightSource.color = Color.cyan;
                break;
        }
    }
}
