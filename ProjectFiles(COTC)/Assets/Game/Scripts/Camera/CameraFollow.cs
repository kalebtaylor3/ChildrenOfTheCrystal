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

    private Vector3 normalRotation = new Vector3(0, 0, 0);
    private Vector3 doorRotation = new Vector3(0, 48.555f, 0);

    private Animator camAm;


    [SerializeField]
    private float offX = 0;

    [SerializeField]
    private float offY = 5;

    [SerializeField]
    private float offZ = -15.22f;

    private Vector3 _originalPos;
    private float _timeAtCurrentFrame;
    private float _timeAtLastFrame;
    private float _fakeDelta;

    void Start()
    {
        cam = GetComponent<Camera>();
        tofar.SetActive(false);
        camAm = GetComponent<Animator>();
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

        _timeAtCurrentFrame = Time.realtimeSinceStartup;
        _fakeDelta = _timeAtCurrentFrame - _timeAtLastFrame;
        _timeAtLastFrame = _timeAtCurrentFrame;

        //if (!target)
        //return;

        // SmoothFollow();
        // if (rotateDamping > 0)
        // SmoothLookAt();
        //else
        //transform.LookAt(target.position);
    }

    public void Shake(float duration, float amount)
    {
        _originalPos = gameObject.transform.localPosition;
        StopAllCoroutines();
        StartCoroutine(cShake(duration, amount));
    }

    public IEnumerator cShake(float duration, float amount)
    {
        float endTime = Time.time + duration;

        while (duration > 0)
        {
            transform.localPosition = _originalPos + Random.insideUnitSphere * amount;

            duration -= _fakeDelta;

            yield return null;
        }

        transform.localPosition = _originalPos;
    }

    public void SetOffsetX(float x)
    {
        offset.x = x;
    }

    public void SetOffsetY(float y)
    {
        offset.y = y;
    }

    public void SetOffsetZ(float z)
    {
        offset.z = z;
    }

    public void RestOffset()
    {
        offset.x = offX;
        offset.y = offY;
        offset.z = offZ;
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
            //SceneManager.LoadScene("Tyler Environment Scene");
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

    public void DoorOpen()
    {
        camAm.SetTrigger("Door");
        offset.x = -4.1f;
        offset.z = -8.62f;
        StartCoroutine(NormalAngle());
    }

    public void SetSmoothTime()
    {
        smoothTime = 0;
    }

    public void ResetSmoothTime()
    {
        smoothTime = 0.39f;
    }

    private IEnumerator NormalAngle()
    {
        yield return new WaitForSeconds(3f);
        camAm.SetTrigger("Normal");
        camAm.ResetTrigger("Door");
        offset.x = 0;
        offset.z = -15.22f;
    }

}
