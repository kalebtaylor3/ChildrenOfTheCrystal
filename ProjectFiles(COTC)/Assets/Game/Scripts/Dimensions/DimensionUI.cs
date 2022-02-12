using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class DimensionUI : MonoBehaviour
{
    [SerializeField]
    public GameObject redRune;
    public GameObject greenRune;
    public GameObject blueRune;
    public GameObject yellowRune;

    public void ChangeDimension(Dimension.Dimensions dimension)
    {
        switch (dimension)
        {
            case Dimension.Dimensions.Main:
                redRune.SetActive(false);
                greenRune.SetActive(false);
                blueRune.SetActive(false);
                yellowRune.SetActive(false);
                break;
            case Dimension.Dimensions.Red:
                redRune.SetActive(true);
                greenRune.SetActive(false);
                blueRune.SetActive(false);
                yellowRune.SetActive(false);
                break;
            case Dimension.Dimensions.Green:
                redRune.SetActive(false);
                greenRune.SetActive(true);
                blueRune.SetActive(false);
                yellowRune.SetActive(false);
                break;
            case Dimension.Dimensions.Blue:
                redRune.SetActive(false);
                greenRune.SetActive(false);
                blueRune.SetActive(true);
                yellowRune.SetActive(false);
                break;
            case Dimension.Dimensions.Yellow:
                redRune.SetActive(false);
                greenRune.SetActive(false);
                blueRune.SetActive(false);
                yellowRune.SetActive(true);
                break;
        }
    }
}
