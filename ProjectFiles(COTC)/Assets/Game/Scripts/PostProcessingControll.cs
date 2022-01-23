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
    private Bloom _bloom;

    private void Awake()
    {
        volume.profile.TryGetSettings(out _colorgraiding);
        volume.profile.TryGetSettings(out _bloom);
    }

    public void ChangeDimension(Dimension.Dimensions dimension)
    {
        switch(dimension)
        {
            case Dimension.Dimensions.Main:
                _colorgraiding.colorFilter.value = colorFilter[0];
                _bloom.intensity.value = 0;
                break;
            case Dimension.Dimensions.Red :
                _colorgraiding.colorFilter.value = colorFilter[1];
                _bloom.intensity.value = 35.91f;
                break;
            case Dimension.Dimensions.Green:
                _colorgraiding.colorFilter.value = colorFilter[2];
                _bloom.intensity.value = 35.91f;
                break;
            case Dimension.Dimensions.Blue:
                _colorgraiding.colorFilter.value = colorFilter[3];
                _bloom.intensity.value = 35.91f;
                break;
            case Dimension.Dimensions.Purple:
                _colorgraiding.colorFilter.value = colorFilter[4];
                _bloom.intensity.value = 35.91f;
                break;
        }
    }
}
