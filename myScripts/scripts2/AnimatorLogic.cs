using UnityEngine;
using System.Collections;

public class AnimatorLogic : MonoBehaviour {
	
	//[SerializeField]
	public float health;
	private float maxHealth;

	private Animator animator;
	public AudioClip jumpsound;

	public float rotationspeed;
	private float jumppower = 0.0f;
	public float jumppowerspeed;
	public float maxjumppower;
	private bool groundCheck;
	public Camera daCam;


	private float speed;
	private float horizontal = 0.0f;
	private float vertical = 0.0f;
	

 	private void Start () {
		maxHealth = health;
		DamagePlayer(25);

		print("player health is:" + health);

		animator = GetComponent<Animator>();
	}
	
	private void Update () {

		if (health <= 0){
			Debug.Log("the player has died" + health);
			Destroy(gameObject, 2f);
		}

		horizontal = Input.GetAxis("Horizontal");
		vertical = Input.GetAxis("Vertical");
		Vector3 stick = new Vector3(horizontal,0, vertical);


		
		speed = Mathf.Clamp (new Vector2(horizontal, vertical).sqrMagnitude,0f,1f);

			

		animator.SetFloat("Speed",speed);
		
		if(speed >0.1){
			Vector3 camDir = daCam.transform.rotation * stick;
			Vector3 objectDir = transform.position + camDir;
			Quaternion targetRotation = Quaternion.LookRotation(objectDir - transform.position);
			Quaternion constrained = Quaternion.Euler(0.0f, targetRotation.eulerAngles.y, 0.0f);
			transform.rotation = Quaternion.Slerp(transform.rotation, constrained, Time.deltaTime * 20f );
		}


		if(Input.GetButton ("Jump") && jumppower <maxjumppower){
			jumppower += Time.deltaTime * jumppowerspeed;
			if (jumppower>maxjumppower)
				jumppower=maxjumppower;
		}

		if(Input.GetButtonUp ("Jump") ){
			if (jumppower<(maxjumppower/3))
				jumppower=(maxjumppower/3);
			if(groundCheck){
				GetComponent<Rigidbody>().AddRelativeForce (Vector3.up * jumppower);
				GetComponent<AudioSource>().PlayOneShot(jumpsound);
			}
			jumppower=0f;

		}

		animator.SetFloat("jumpPower",jumppower/maxjumppower);
		
		if (groundCheck==false)
			animator.SetBool("groundCheck",false);
		else
			animator.SetBool("groundCheck",true);

		
	}

	public void DamagePlayer(int damage){
		this.health -= damage;
	}

	#region setfunks
	public void setGroundCheck(bool groundCheck)
	{
		this.groundCheck = groundCheck;
	}
	#endregion

	private void OnGUI(){
		GUI.backgroundColor = Color.yellow;
		GUI.Box(new Rect(10f,10f,Screen.width / 4f /(maxjumppower/jumppower), 20f),"jump");
		GUI.Box(new Rect(10f,50f,Screen.width / 4f /(maxHealth/health), 20f),"health");
	}

}
