using UnityEngine;

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
	private Renderer render;
	private AudioSource aSource;

	//setup
	void Awake()
	{
		render = GetComponent<Renderer>();
		aSource = GetComponent<AudioSource>();
		if(tag != "Respawn")
		{
			tag = "Respawn";
			Debug.LogWarning ("'Checkpoint' script attached to object without the 'Respawn' tag, tag has been assigned automatically", transform);	
		}
		GetComponent<Collider>().isTrigger = true;
		
		if(render)
			defColor = render.material.color;
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
		if(other.transform.tag == "Player1")
		{
			//set respawn position in players health script
			player1Health.respawnPos = transform.position;
			
			//toggle checkpoints
				foreach (GameObject checkpoint in checkpoints)
					checkpoint.GetComponent<Renderer>().material.color = defColor;
				aSource.Play();
				render.material.color = activeColor;
		}

		if (other.transform.tag == "Player2")
		{
			//set respawn position in players health script
			player2Health.respawnPos = transform.position;

			//toggle checkpoints
				foreach (GameObject checkpoint in checkpoints)
					checkpoint.GetComponent<Renderer>().material.color = defColor;
				aSource.Play();
				render.material.color = activeColor;
		}
	}
}