using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dimension : MonoBehaviour
{
    [SerializeField]
    GameObject strengthGO;
    public int a = 1;

    // Start is called before the first frame update
    void Start()
    {
 
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.X))
        {
            Debug.Log("V was pressed");
            strength();
        }

        if (Input.GetKeyDown(KeyCode.Z))
        {
            Debug.Log("V was pressed");
            main();
        }
    }

    private void strength()
    {
            strengthGO.SetActive(false);
    }
    private void main()
    {
        strengthGO.SetActive(true);
    }

}
