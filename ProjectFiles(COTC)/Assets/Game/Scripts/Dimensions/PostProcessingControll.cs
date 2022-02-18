using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;
using System;

public class PostProcessingControll : MonoBehaviour
{
    public PostProcessVolume volume;
    public Color[] colorFilter;

    private ColorGrading _colorgraiding;

    private void Start()
    {
        volume.profile.TryGetSettings(out _colorgraiding);
    }

    public void ChangeDimension(Dimension.Dimensions dimension)
    {
        if (_colorgraiding != null)
        {
            switch (dimension)
            {
                case Dimension.Dimensions.Main:
                    _colorgraiding.colorFilter.value = colorFilter[0];
                    break;
                case Dimension.Dimensions.Red:
                    _colorgraiding.colorFilter.value = colorFilter[1];
                    break;
                case Dimension.Dimensions.Green:
                    _colorgraiding.colorFilter.value = colorFilter[2];
                    break;
                case Dimension.Dimensions.Blue:
                    _colorgraiding.colorFilter.value = colorFilter[3];
                    break;
                case Dimension.Dimensions.Yellow:
                    _colorgraiding.colorFilter.value = colorFilter[4];
                    break;
            }
        }
    }
}
