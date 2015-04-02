using UnityEngine;
using System.Collections;

public class movetowards : MonoBehaviour {
	public Transform target;
	public int moveSpeed;
	public int walkSpeed;
	public int runSpeed;
	public int rotationspeeed;
	public float jumppower = 1000;
	public float jumpdetectdistance = 10;
	public bool collisiondetect ;
	public float jumpforce;
	public GameObject targetobject;
	
	private float distance = 0;
	private Transform myTransform;
	//private CharacterController charController;
	

	
	void Awake(){
		myTransform = transform;
	}

	// Use this for initialization
	void Start () {
		
		//charController = GetComponent<CharacterController>();
		
		GameObject go = GameObject.FindGameObjectWithTag("movetarget");
		
		target = go.transform;
	
	}
	
	// Update is called once per frame
	void Update () {
		
		
		distance = Vector3.Distance(targetobject.transform.position, transform.position);
		
		if (distance>2 && !Input.GetKey(KeyCode.LeftShift)){
			moveSpeed = walkSpeed;
			move();
		}
		
		 else if(distance>2 && Input.GetKey(KeyCode.LeftShift) ){
			moveSpeed = runSpeed;
			move();
		}
		else{
			GetComponent<Animation>().CrossFade("idle", 0.5f);
		}
		if(Input.GetKeyDown(KeyCode.Space)){
			
		}
		
		/*	var up = transform.TransformDirection(Vector3.up);
		Debug.DrawRay(transform.position, -up * jumpdetectdistance, Color.blue);
		
		
		if(Input.GetKeyDown(KeyCode.Space) && Physics.Raycast(transform.position,-up,jumpdetectdistance) || Input.GetKeyDown(KeyCode.Space) &&  collisiondetect == true){
			jump();
		}*/ 
		
	}
	
	void move(){
	//	Debug.DrawLine(target.position, myTransform.position, Color.red);
		//look at target
		myTransform.rotation = Quaternion.Slerp(myTransform.rotation, Quaternion.LookRotation(target.position - myTransform.position), rotationspeeed *Time.deltaTime);
		
		//move towards target
		/*var fwd = transform.forward * 50;
		rigidbody.AddForce(fwd);*/
		myTransform.position += myTransform.forward * moveSpeed * Time.deltaTime;
		
		
		if(moveSpeed == walkSpeed){
			GetComponent<Animation>().CrossFade("walk", 0.5f);
		}
		if(moveSpeed > walkSpeed){
			GetComponent<Animation>().CrossFade("runtest", 0.5f);
		}
	}
/*	
	void jump(){
		
			transform.rigidbody.AddForce(Vector3.up * jumppower );
		
	}
	void OnCollisionEnter() {
		
		collisiondetect = true;
		
		Debug.Log("in contact");
	}
	void OnCollisionExit() {
		
		collisiondetect = false;
		
		Debug.Log("off contact");
	}*/

}
