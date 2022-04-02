using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RockSpawner : MonoBehaviour
{

    // The enemy prefab to be spawned.
    public GameObject Rock;

    // An array of the spawn points this enemy can spawn from.
    public Transform[] spawnPoints;

    public float maxTime = 1;

    public float minTime = 1;


   public Vector3 rockDirection;

    //rock velocity
    //private float velocity = 5;

    //current time
    private float time;

    //The time to spawn the object
    private float spawnTime;
   void Awake()
    {
        rockDirection = new Vector3(0.0f, -1.0f, 0.0f);

    }
     void Update()
    {
      //  Spawn();
    }
    // Start is called before the first frame update
    void Start()
    {
        SetRandomTime();
        time = 0;
    }

    // Update is called once per frame
  
    //Sets the random time between minTime and maxTime
    void SetRandomTime()
    {
        //spawnTime = Random.Range(minTime, maxTime);
        spawnTime = 0.4f;
    }
    void FixedUpdate()
    {
        //Counts up
        time += Time.deltaTime;
        //Check if its the right time to spawn the object
        if (time >= spawnTime)
        {
            Debug.Log("Time to spawn: " + Rock.name);
            Spawn();
            SetRandomTime();
            time = 0;
        }   
    }
   public void Spawn()
    {
        Rock.GetComponent<Rigidbody>().MovePosition(rockDirection);
        // Find a random index between zero and one less than the number of spawn points.
        int spawnPointIndex = Random.Range(0, spawnPoints.Length);
        // Create an instance of the enemy prefab at the randomly selected spawn point's position and rotation.
        Instantiate(Rock, spawnPoints[spawnPointIndex].position, spawnPoints[spawnPointIndex].rotation);
    }
}
