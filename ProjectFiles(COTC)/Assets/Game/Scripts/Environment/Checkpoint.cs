using UnityEngine;
using UnityEngine.Events;

//simple class to add to checkpoint triggers
[RequireComponent(typeof(CapsuleCollider))]
[RequireComponent(typeof(AudioSource))]
public class Checkpoint : MonoBehaviour 
{
	public Color activeColor = Color.green;	//color when checkpoint is activated
	public float activeColorOpacity = 0.4f;	//opacity when checkpoint is activated
	
	[SerializeField]
	private Health player1Health;

	[SerializeField]
	private Health player2Health;

	private Color defColor;
	private GameObject[] checkpoints;
	private AudioSource aSource;

	private bool player1Once = false;
	private bool player2Once = false;

	[SerializeField] private UnityEvent trigger;


	bool happenOnce = false;

	//setup
	void Awake()
	{
		aSource = GetComponent<AudioSource>();
		if(tag != "Respawn")
		{
			tag = "Respawn";
			Debug.LogWarning ("'Checkpoint' script attached to object without the 'Respawn' tag, tag has been assigned automatically", transform);	
		}
		GetComponent<Collider>().isTrigger = true;
		activeColor.a = activeColorOpacity;
	}
	
	//more setup
	void Start()
	{
		checkpoints = GameObject.FindGameObjectsWithTag("Respawn");
		//player1Health = GameObject.FindGameObjectWithTag("Player").GetComponent<Health>();
		if(!player1Health)
			Debug.LogError("For Checkpoint to work, the Player needs 'Health' script attached", transform);
	}
	
	//set checkpoint
	void OnTriggerEnter(Collider other)
	{

		if(!happenOnce)
        {
			aSource.Play();
			happenOnce = true;
		}

		Debug.Log("checkpoint");
		trigger.Invoke();
		if(other.transform.tag == "Player1")
		{
			//set respawn position in players health script
			player1Health.respawnPos = transform.position;

			if (!player1Once)
			{
				player1Once = true;
			}
		}

		if (other.transform.tag == "Player2")
		{
			//set respawn position in players health script
			player2Health.respawnPos = transform.position;

			if (!player2Once)
			{
				player2Once = true;
			}
		}
	}
}