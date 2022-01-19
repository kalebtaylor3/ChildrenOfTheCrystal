using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(Camera))]
public class CameraFollow : MonoBehaviour
{

    public List<Transform> targets;

    public Vector3 offset;

    private Vector3 velocity;
    public float smoothTime = 0.5f;

    public float minZoom = 60f;

    public float maxZoom = 90f;
    public float zoomLimiter;
    private Camera cam;
    private bool startCountDown = false;

    private bool dead = false;
    public GameObject tofar;
    float deathCounter = 0;

    public bool lockRotation;                                   
    public float inputRotationSpeed = 100;                     
    public bool mouseFreelook;                                  
    public float rotateDamping = 100;                           
    public GameObject waterFilter;                              

    //private Transform followTarget;
    private bool camColliding;


    void Start()
    {
        cam = GetComponent<Camera>();
        tofar.SetActive(false);
    }

    void Awake()
    {
        //followTarget = new GameObject().transform;  //create empty gameObject as camera target, this will follow and rotate around the player
        //followTarget.name = "Camera Target";
        if (waterFilter)
            waterFilter.GetComponent<Renderer>().enabled = false;
        //if (!target)
            //Debug.LogError("'CameraFollow script' has no target assigned to it", transform);

        //don't smooth rotate if were using mouselook
        if (mouseFreelook)
            rotateDamping = 0f;
    }

    //run our camera functions each frame
    void Update()
    {
        //if (!target)
            //return;

       // SmoothFollow();
       // if (rotateDamping > 0)
           // SmoothLookAt();
        //else
            //transform.LookAt(target.position);
    }

    void LateUpdate()
    {

        if (GetGreatestDistance() >= 29.0f)
        {
            deathCounter += 10 * Time.deltaTime;
            startCountDown = true;
            tofar.SetActive(true);
            Debug.Log(deathCounter);
        }
        else
        {
            deathCounter = 0;
            startCountDown = false;
            dead = false;
            tofar.SetActive(false);
        }

        if (deathCounter >= 30)
        {
            dead = true;
        }

        if (dead == true)
        {
            SceneManager.LoadScene("Demo Scene");
            dead = false;
        }



        if (targets.Count == 0)
        {
            return;
        }

        Move();
        Zoom();

    }

    void Move()
    {
        Vector3 centerPoint = GetCenterPoint();

        Vector3 newPosition = centerPoint + offset;

        transform.position = Vector3.SmoothDamp(transform.position, newPosition, ref velocity, smoothTime);
    }

    void Zoom()
    {

        float newZoom = Mathf.Lerp(minZoom, maxZoom, GetGreatestDistance() / zoomLimiter);
        cam.fieldOfView = Mathf.Lerp(cam.fieldOfView, newZoom, Time.deltaTime);
    }

    float GetGreatestDistance()
    {
        var bounds = new Bounds(targets[0].position, Vector3.zero);
        for (int i = 0; i < targets.Count; i++)
        {
            bounds.Encapsulate(targets[i].position);
        }

        return bounds.size.x;
    }

    Vector3 GetCenterPoint()
    {
        if (targets.Count == 1)
        {
            return targets[0].position;
        }

        var bounds = new Bounds(targets[0].position, Vector3.zero);
        for (int i = 0; i < targets.Count; i++)
        {
            bounds.Encapsulate(targets[i].position);
        }

        return bounds.center;

    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Water" && waterFilter)
            waterFilter.GetComponent<Renderer>().enabled = true;
    }

    //toggle waterfilter, is camera clipping walls?
    void OnTriggerExit(Collider other)
    {
        if (other.tag == "Water" && waterFilter)
            waterFilter.GetComponent<Renderer>().enabled = false;
    }

}
