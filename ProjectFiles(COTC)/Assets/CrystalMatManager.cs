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


    private Renderer crystalRenderer;



    private void Start()
    {
        crystalRenderer = GetComponent<Renderer>();
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
