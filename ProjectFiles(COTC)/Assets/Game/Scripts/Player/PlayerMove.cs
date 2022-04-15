using UnityEngine;
using System.Collections;
using System;

//handles player movement, utilising the CharacterMotor class
[RequireComponent(typeof(CharacterMotor))]
[RequireComponent(typeof(DealDamage))]
[RequireComponent(typeof(AudioSource))]
[RequireComponent(typeof(Rigidbody))]
public class PlayerMove : MonoBehaviour 
{
	//setup
	public bool sidescroller;					//if true, won't apply vertical input
	public Transform mainCam, floorChecks;		//main camera, and floorChecks object. FloorChecks are raycasted down from to check the player is grounded.
	public Animator animator;					//object with animation controller on, which you want to animate
	public AudioClip jumpSound;					//play when jumping
	public AudioClip landSound;                 //play when landing on ground
	public AudioClip dimensionSound;
	public AudioClip runeSound;
	public AudioClip throwSound;
	public AudioClip liftSound;
	public AudioClip punchSound;

	//movement
	public float accel = 70f;					//acceleration/deceleration in air or on the ground
	public float airAccel = 18f;			
	public float decel = 7.6f;
	public float airDecel = 1.1f;
	[Range(0f, 5f)]
	public float rotateSpeed = 0.7f, airRotateSpeed = 0.4f;	//how fast to rotate on the ground, how fast to rotate in the air
	public float maxSpeed = 9;								//maximum speed of movement in X/Z axis
	public float slopeLimit = 40, slideAmount = 35;			//maximum angle of slopes you can walk on, how fast to slide down slopes you can't
	public float movingPlatformFriction = 7.7f;				//you'll need to tweak this to get the player to stay on moving platforms properly
	
	//jumping
	public Vector3 jumpForce =  new Vector3(0, 13, 0);		//normal jump force
	public Vector3 secondJumpForce = new Vector3(0, 13, 0); //the force of a 2nd consecutive jump
	public Vector3 thirdJumpForce = new Vector3(0, 13, 0);	//the force of a 3rd consecutive jump
	public float jumpDelay = 0.1f;							//how fast you need to jump after hitting the ground, to do the next type of jump
	public float jumpLeniancy = 0.17f;						//how early before hitting the ground you can press jump, and still have it work
	[HideInInspector]
	public int onEnemyBounce;					
	
	private int onJump;
	[HideInInspector]
	public bool grounded;
	private Transform[] floorCheckers;
	private Quaternion screenMovementSpace;
	private float airPressTime, groundedCount, curAccel, curDecel, curRotateSpeed, slope;
	private Vector3 direction, screenMovementForward, screenMovementRight, movingObjSpeed;

	public Vector3 moveDirection;

	private CharacterMotor characterMotor;
	private EnemyAI enemyAI;
	private DealDamage dealDamage;
	private Rigidbody rigid;
	[HideInInspector]
	public AudioSource aSource;
	public AudioSource jSource;

	[HideInInspector]
	public bool canMove = true;

	public Transform pickupLoc;
	private float originOffset = 0.4f;
	private bool facingRight = true;

	public Dimension dimensionalController;

	[HideInInspector]
	public bool lifting = false;
	[HideInInspector]
	public bool beingLifted = false;

	public LayerMask ingoreMe;

	[HideInInspector]
	public bool climbing = false;

	private PlayerMove playerbeingpickedup;
	[HideInInspector]
	public PlayerMove playerbeinglifted;
	private int liftTime = 5;

	//setup
	void Awake()
	{
		//create single floorcheck in centre of object, if none are assigned
		if(!floorChecks)
		{
			floorChecks = new GameObject().transform;
			floorChecks.name = "FloorChecks";
			floorChecks.parent = transform;
			floorChecks.position = transform.position;
			GameObject check = new GameObject();
			check.name = "Check1";
			check.transform.parent = floorChecks;
			check.transform.position = transform.position;
			Debug.LogWarning("No 'floorChecks' assigned to PlayerMove script, so a single floorcheck has been created", floorChecks);
		}
		//assign player tag if not already
		if(tag != "Player")
		{
			tag = "Player";
			Debug.LogWarning ("PlayerMove script assigned to object without the tag 'Player', tag has been assigned automatically", transform);
		}
		//usual setup
		mainCam = GameObject.FindGameObjectWithTag("MainCamera").transform;
		dealDamage = GetComponent<DealDamage>();
		characterMotor = GetComponent<CharacterMotor>();
		rigid = GetComponent<Rigidbody>();
		aSource = GetComponent<AudioSource>();
		//gets child objects of floorcheckers, and puts them in an array
		//later these are used to raycast downward and see if we are on the ground
		floorCheckers = new Transform[floorChecks.childCount];
		for (int i=0; i < floorCheckers.Length; i++)
			floorCheckers[i] = floorChecks.GetChild(i);
	}

	public void CalculateMovement(PlayerMove player, float h, float v, string jumpKey)
    {
		player.rigid.WakeUp();
		//handle jumping
		player.JumpCalculations(jumpKey, player);
		//adjust movement values if we're in the air or on the ground
		player.curAccel = (player.grounded) ? player.accel : player.airAccel;
		player.curDecel = (player.grounded) ? player.decel : player.airDecel;
		player.curRotateSpeed = (player.grounded) ? player.rotateSpeed : player.airRotateSpeed;

		//get movement axis relative to camera
		player.screenMovementSpace = Quaternion.Euler(0, mainCam.eulerAngles.y, 0);
		player.screenMovementForward = player.screenMovementSpace * Vector3.forward;
		player.screenMovementRight = player.screenMovementSpace * Vector3.right;

		//get movement input, set direction to move in
		//float h = Input.GetAxisRaw("Horizontal");
		//float v = Input.GetAxisRaw("Vertical");

		//only apply vertical input to movemement, if player is not sidescroller
		if (!player.sidescroller)
			player.direction = (player.screenMovementForward * v) + (player.screenMovementRight * h);
		else
			player.direction = Vector3.right * h;

		player.moveDirection = player.transform.position + player.direction;
	}

    private void OnTriggerStay(Collider other)
    {
        if(other.tag == "Breakable")
        {
			if(this.GetComponent<Player1>()!= null)
            {
				BreakableWall wall = other.GetComponent<BreakableWall>();

				if (wall != null)
				{
					if (dimensionalController.currentDimension == Dimension.Dimensions.Red)
					{
						Debug.Log("Thats a wall");
						if (Input.GetKeyDown(KeyCode.LeftControl))
						{
							aSource.volume = 1;
							aSource.clip = punchSound;
							aSource.Play();
							if (!wall.leverControlled)
                            {
								StartCoroutine(WaitForWall(wall));
								if (wall.punchDown)
									animator.SetTrigger("PunchDown");
								else
									animator.SetTrigger("Punch");
							}
						}
					}
				}
			}
		}

		if (other.tag == "Player")
		{
			if (other.GetComponent<Player1>() != null)
			{
				playerbeingpickedup = other.GetComponent<PlayerMove>();
			}

			if (other.GetComponent<Player2>() != null)
			{
				playerbeingpickedup = other.GetComponent<PlayerMove>();
			}

		}

	}

	IEnumerator WaitForWall(BreakableWall wall)
    {
		yield return new WaitForSeconds(0.8f);
		wall.BreakWall();
		wall.DestroyChildren();
		mainCam.GetComponent<CameraFollow>().Shake(0.5f, 0.2f);
	}

    public void SetLayerRecursively(GameObject obj, int newLayer)
	{
		if (null == obj)
		{
			return;
		}

		obj.layer = newLayer;

		foreach (Transform child in obj.transform)
		{
			Transform checker = transform.FindChild("PlayerChecker");
			if (null == child)
			{
				continue;
			}
			SetLayerRecursively(child.gameObject, newLayer);
			SetLayerRecursively(checker.gameObject, 2);
		}
	}

	void LeaveDimension()
    {
		SetLayerRecursively(gameObject, 0);
    }

	public void Move(PlayerMove player)
    {
		if(canMove)
        {
			//are we grounded
			player.grounded = player.IsGrounded();
			//move, rotate, manage speed
			player.characterMotor.MoveTo(player.moveDirection, player.curAccel, 0.7f, true);
			if (player.rotateSpeed != 0 && player.direction.magnitude != 0)
				player.characterMotor.RotateToDirection(player.moveDirection, player.curRotateSpeed * 5, true);
			player.characterMotor.ManageSpeed(player.curDecel, player.maxSpeed + player.movingObjSpeed.magnitude, true);
			//set animation values
			if (player.animator)
			{
				player.animator.SetBool("HoldingPickup", player.lifting);
				player.animator.SetFloat("DistanceToTarget", player.characterMotor.DistanceToTarget);
				player.animator.SetBool("Grounded", player.grounded);
				player.animator.SetFloat("YVelocity", GetComponent<Rigidbody>().velocity.y);
			}
		}
	}
	
	//prevents rigidbody from sliding down slight slopes (read notes in characterMotor class for more info on friction)
	void OnCollisionStay(Collision other)
	{
		//only stop movement on slight slopes if we aren't being touched by anything else
		if (other.collider.tag != "Untagged" || grounded == false)
			return;
		//if no movement should be happening, stop player moving in Z/X axis
		if(direction.magnitude == 0 && slope < slopeLimit && rigid.velocity.magnitude < 2)
		{
			//it's usually not a good idea to alter a rigidbodies velocity every frame
			//but this is the cleanest way i could think of, and we have a lot of checks beforehand, so it should be ok
			rigid.velocity = Vector3.zero;
		}
	}
	
	//returns whether we are on the ground or not
	//also: bouncing on enemies, keeping player on moving platforms and slope checking
	private bool IsGrounded() 
	{

		facingRight = checkDirection();

		//get distance to ground, from centre of collider (where floorcheckers should be)
		float dist = GetComponent<Collider>().bounds.extents.y;
		//check whats at players feet, at each floorcheckers position

		if (climbing)
			return true;

		//if check for camera cutscene
		foreach (Transform check in floorCheckers)
		{
			RaycastHit hit;
			if(Physics.Raycast(check.position, Vector3.down, out hit, dist + 0.05f))
			{
				if(!hit.transform.GetComponent<Collider>().isTrigger)
				{
					//slope control
					slope = Vector3.Angle (hit.normal, Vector3.up);
					//slide down slopes
					if(slope > slopeLimit && hit.transform.tag != "Pushable")
					{
						Vector3 slide = new Vector3(0f, -slideAmount, 0f);
						rigid.AddForce (slide, ForceMode.Force);
					}
					//enemy bouncing
					if (hit.transform.tag == "Enemy" && rigid.velocity.y < 0)
					{
						enemyAI = hit.transform.GetComponent<EnemyAI>();
						enemyAI.BouncedOn();
						onEnemyBounce ++;
						dealDamage.Attack(hit.transform.gameObject, 1, 0f, 0f);
					}
					else
						onEnemyBounce = 0;
					//moving platforms
					if (hit.transform.tag == "MovingPlatform" || hit.transform.tag == "Pushable")
					{
						movingObjSpeed = hit.transform.GetComponent<Rigidbody>().velocity;
						movingObjSpeed.y = 0f;
						//9.5f is a magic number, if youre not moving properly on platforms, experiment with this number
						rigid.AddForce(movingObjSpeed * movingPlatformFriction * Time.fixedDeltaTime, ForceMode.VelocityChange);
					}
					else
					{
						movingObjSpeed = Vector3.zero;
					}
					//yes our feet are on something
					return true;
				}
			}
		}
		movingObjSpeed = Vector3.zero;
		//no none of the floorchecks hit anything, we must be in the air (or water)
		return false;
	}
	
	//jumping
	private void JumpCalculations(string key, PlayerMove player)
	{
		//keep how long we have been on the ground
		player.groundedCount = (player.grounded) ? player.groundedCount += Time.deltaTime : 0f;
		
		//play landing sound
		if(player.groundedCount < 0.25 && player.groundedCount != 0 && !GetComponent<AudioSource>().isPlaying && player.landSound && GetComponent<Rigidbody>().velocity.y < 1)
		{
			player.aSource.volume = Mathf.Abs(GetComponent<Rigidbody>().velocity.y)/40;
			//player.aSource.volume = 0.4f;
			player.aSource.clip = player.landSound;
			player.aSource.Play ();
		}
		//if we press jump in the air, save the time
		if (Input.GetButtonDown (key) && !player.grounded)
			player.airPressTime = Time.time;
		
		//if were on ground within slope limit
		if (player.grounded && player.slope < player.slopeLimit)
		{
			//and we press jump, or we pressed jump justt before hitting the ground
			if (Input.GetButtonDown(key) || (player.airPressTime + player.jumpLeniancy > Time.time) && (Time.time > player.jumpLeniancy)) 
			{
				//increment our jump type if we haven't been on the ground for long
				player.onJump = (player.groundedCount < player.jumpDelay) ? Mathf.Min(2, player.onJump + 1) : 0;
				//execute the correct jump (like in mario64, jumping 3 times quickly will do higher jumps)
				if (player.onJump == 0)
						Jump (player.jumpForce);
				else if (player.onJump == 1)
						Jump (player.secondJumpForce);
				else if (player.onJump == 2){
						Jump (player.thirdJumpForce);
					player.onJump --;
				}
			}
		}
	}
	
	//push player at jump force
	public void Jump(Vector3 jumpVelocity)
	{
		if(jumpSound)
		{
			jSource.volume = 1;
			jSource.clip = jumpSound;
			jSource.Play();
		}
		rigid.velocity = new Vector3(rigid.velocity.x, 0f, rigid.velocity.z);
		rigid.AddRelativeForce (jumpVelocity, ForceMode.Impulse);
		airPressTime = 0f;
	}

	public void pickup(Vector3 Direction)
	{

		if(playerbeingpickedup != null && !dimensionalController.inDimension)
        {
			if (!aSource.isPlaying)
			{
				aSource.clip = liftSound;
				aSource.volume = 0.8f;
				aSource.Play();
			}
			lifting = true;
			playerbeingpickedup.beingLifted = true;
			playerbeingpickedup.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotation | RigidbodyConstraints.FreezePositionZ;
			playerbeingpickedup.GetComponent<Rigidbody>().isKinematic = true;
			playerbeingpickedup.transform.parent = this.pickupLoc.transform;
			playerbeingpickedup.transform.position = pickupLoc.position;
			//playerbeingpickedup.GetComponent<BoxCollider>().isTrigger = true;
			playerbeingpickedup.canMove = false;
			//StartCoroutine(pickupCoolDown(playerbeingpickedup));
			playerbeinglifted = playerbeingpickedup;
			playerbeingpickedup.animator.SetTrigger("Sit");
			playerbeinglifted.animator.ResetTrigger("Thrown");
		}

		//RaycastHit hit = PickUpRayCheck(Direction);

		//if(hit.collider.gameObject == this.gameObject)
  //      {
		//	return;
  //      }
		//else if (hit.collider.tag == "Player")
		//{
		//	lifting = true;
		//	hit.collider.GetComponent<PlayerMove>().beingLifted = true;
		//	hit.collider.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotation | RigidbodyConstraints.FreezePositionZ;
		//	hit.collider.GetComponent<Rigidbody>().isKinematic = true;
		//	hit.collider.transform.parent = this.pickupLoc.transform;
		//	hit.collider.transform.position = pickupLoc.position;
		//	hit.collider.GetComponent<BoxCollider>().isTrigger = true;
		//	hit.collider.GetComponent<PlayerMove>().canMove = false;
		//	//lifted player will begin playing the being lifted animation
		//	Debug.Log("Picking up " + hit.collider.name);

		//	StartCoroutine(pickupCoolDown(hit));
		//	playerbeinglifted = hit.collider.GetComponent<PlayerMove>();
		//}
	}

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
			if(other.GetComponent<Player1>() != null)
            {
				playerbeingpickedup = other.GetComponent<PlayerMove>();
            }

			if (other.GetComponent<Player2>() != null)
			{
				playerbeingpickedup = other.GetComponent<PlayerMove>();
			}

		}
    }


    private void OnTriggerExit(Collider other)
    {
		if (other.tag == "Player")
        {
			StopCoroutine(pickupCoolDown(playerbeingpickedup));
			playerbeingpickedup = null;
			liftTime = 5;
		}
    }

	IEnumerator pickupCoolDown(PlayerMove liftedPlayer)
	{
		yield return new WaitForSeconds(liftTime);

		lifting = false;
		liftedPlayer.GetComponent<Rigidbody>().isKinematic = false;
		liftedPlayer.transform.parent = null;
		//liftedPlayer.GetComponent<BoxCollider>().isTrigger = false;
		liftedPlayer.canMove = true;
		liftedPlayer.beingLifted = false;
		playerbeinglifted = null;
	}

	public void ThrowPlayer()
	{

		if(playerbeinglifted != null)
        {
			playerbeinglifted.transform.parent = null;
			//playerbeinglifted.GetComponent<BoxCollider>().isTrigger = false;
			playerbeinglifted.canMove = true;
			playerbeinglifted.beingLifted = false;
			playerbeinglifted.GetComponent<Rigidbody>().isKinematic = false;
            if (facingRight)
            {
				playerbeinglifted.GetComponent<Rigidbody>().AddForce(Vector3.right * 55);
				playerbeinglifted.GetComponent<Rigidbody>().AddForce(Vector3.up * 45);
			}
            else
            {
				playerbeinglifted.GetComponent<Rigidbody>().AddForce(Vector3.left * 55);
				playerbeinglifted.GetComponent<Rigidbody>().AddForce(Vector3.up * 45);
			}

			playerbeinglifted.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotation | RigidbodyConstraints.FreezePositionZ;
			StartCoroutine(ClearPlayer());
			playerbeinglifted.animator.SetTrigger("Thrown");
			playerbeinglifted.animator.ResetTrigger("Sit");
			lifting = false;

		}

		//RaycastHit hit = ThrowCheck(Vector3.up);
		//if (hit.collider.gameObject == gameObject)
		//{
		//	return;
		//}

		//if (hit.collider.gameObject.tag == "Player" && hit.collider.gameObject.tag != "Default")
		//{
		//	lifting = false;
		//	hit.collider.transform.parent = null;
		//	hit.collider.GetComponent<BoxCollider>().isTrigger = false;
		//	hit.collider.GetComponent<PlayerMove>().canMove = true;
		//	hit.collider.GetComponent<PlayerMove>().beingLifted = false;
		//	hit.collider.GetComponent<Rigidbody>().isKinematic = false;

		//	if (facingRight)
		//	{
		//		hit.collider.GetComponent<Rigidbody>().AddForce(Vector3.right * 100);
		//		hit.collider.GetComponent<Rigidbody>().AddForce(Vector3.up * 100);
		//	}
		//	else
		//	{
		//		hit.collider.GetComponent<Rigidbody>().AddForce(Vector3.left * 100);
		//		hit.collider.GetComponent<Rigidbody>().AddForce(Vector3.up * 100);
		//	}
		//	hit.collider.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotation | RigidbodyConstraints.FreezePositionZ;


		//	Debug.Log("Threw " + hit.collider.name);
	//}
	}

	IEnumerator ClearPlayer()
    {
		yield return new WaitForSeconds(0.5f);
		playerbeinglifted = null;
    }

	public RaycastHit ThrowCheck(Vector3 directin)
	{

		RaycastHit hit;

		Vector3 startingPosition = new Vector3(transform.position.x, transform.position.y + 1f, transform.position.z);

		Debug.DrawRay(startingPosition, Vector3.up, Color.red);
		if (Physics.Raycast(transform.position, directin, out hit, ~ingoreMe))
		{
			return hit;
		}
		else
		{
			return hit;
		}
	}

	public bool checkDirection()
	{
		if (transform.rotation.eulerAngles.y > 135)
			return false;
		if (transform.rotation.eulerAngles.y <= 135)
			return true;

		return true;
	}
}