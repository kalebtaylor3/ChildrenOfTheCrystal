using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(Camera))]
public class CameraFollow : MonoBehaviour
{

    public List<Transform> actualTargets;

    public List<Transform> startTargets;

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
    private AudioSource camSource;

    void Start()
    {
        cam = GetComponent<Camera>();
        tofar.SetActive(false);
        camAm = GetComponent<Animator>();
        camSource = GetComponent<AudioSource>();
    }

    public void SetTargets()
    {
        actualTargets[0] = startTargets[0];
        actualTargets[1] = startTargets[1];
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

    public void PlaySwoosh()
    {
        camSource.Play();
    }

    void LateUpdate()
    {

        if (GetGreatestDistance() >= 50f)
        {
            
            if(actualTargets[0].position.x > actualTargets[1].position.x)
            {
                actualTargets[1].transform.position = new Vector3(actualTargets[0].position.x, actualTargets[0].position.y, actualTargets[0].position.z);
            }

            if (actualTargets[1].position.x > actualTargets[0].position.x)
            {
                actualTargets[0].transform.position = new Vector3(actualTargets[1].position.x, actualTargets[1].position.y, actualTargets[1].position.z);
            }
        }


        if (actualTargets.Count == 0)
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
        var bounds = new Bounds(actualTargets[0].position, Vector3.zero);
        for (int i = 0; i < actualTargets.Count; i++)
        {
            bounds.Encapsulate(actualTargets[i].position);
        }

        return bounds.size.x;
    }

    Vector3 GetCenterPoint()
    {
        if (actualTargets.Count == 1)
        {
            return actualTargets[0].position;
        }

        var bounds = new Bounds(actualTargets[0].position, Vector3.zero);
        for (int i = 0; i < actualTargets.Count; i++)
        {
            bounds.Encapsulate(actualTargets[i].position);
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
        PlaySwoosh();
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
        PlaySwoosh();
    }

}
