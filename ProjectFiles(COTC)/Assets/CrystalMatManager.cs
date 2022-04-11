using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrystalMatManager : MonoBehaviour
{
    [SerializeField]
    private Material redMat;
    [SerializeField]
    private Material blueMat;
    [SerializeField]
    private Material greenMat;
    [SerializeField]
    private Material yellowMat;
    [SerializeField]
    private Material normalMat;
    [SerializeField]
    private bool multipleMaterials;

    private Renderer crystalRenderer;

    Material[] mats;

    private void Start()
    {
        crystalRenderer = GetComponent<Renderer>();

        if(multipleMaterials)
        {
            mats = gameObject.GetComponent<Renderer>().materials;
        }
    }

    private void OnEnable()
    {
        Dimension.OnDimension += ChangeCrystals;
    }

    private void OnDisable()
    {
        Dimension.OnDimension -= ChangeCrystals;
    }

    void ChangeCrystals(string dimension)
    {
        if (multipleMaterials)
        {
            switch (dimension)
            {
                case "Red":
                    mats[1] = redMat;
                    crystalRenderer.materials = mats;
                    break;
                case "Blue":
                    mats[1] = blueMat;
                    crystalRenderer.materials = mats;
                    break;
                case "Green":
                    mats[1] = greenMat;
                    crystalRenderer.materials = mats;
                    break;
                case "Yellow":
                    mats[1] = yellowMat;
                    crystalRenderer.materials = mats;
                    break;
                case "Main":
                    mats[1] = normalMat;
                    crystalRenderer.materials = mats;
                    break;
            }
        }
        else
        {
            switch (dimension)
            {
                case "Red":
                    crystalRenderer.material = redMat;
                    break;
                case "Blue":
                    crystalRenderer.material = blueMat;
                    break;
                case "Green":
                    crystalRenderer.material = greenMat;
                    break;
                case "Yellow":
                    crystalRenderer.material = yellowMat;
                    break;
                case "Main":
                    crystalRenderer.material = normalMat;
                    break;
            }
        }
    }
}
