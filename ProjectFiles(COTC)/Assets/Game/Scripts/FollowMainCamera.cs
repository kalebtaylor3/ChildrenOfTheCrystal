using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowMainCamera : MonoBehaviour
{
    private Camera thisCam;
    [SerializeField]
    private Camera mainCam;

    // Start is called before the first frame update
    void Awake()
    {
        thisCam = GetComponent<Camera>();
        thisCam.fieldOfView = mainCam.fieldOfView;
    }

    // Update is called once per frame
    void Update()
    {
        thisCam.fieldOfView = mainCam.fieldOfView;
    }
}
