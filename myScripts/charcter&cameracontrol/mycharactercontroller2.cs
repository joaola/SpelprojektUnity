using UnityEngine;
using System.Collections;

public class mycharactercontroller2 : MonoBehaviour {

	public float defaultspeed = 20f;
	public float shiftspeed = 50f;
	public float defaultAirspeed = 20f;
	public float jumpSpeed = 35f;
	public float gravity = 40f;
	public GameObject targetObject;
	public Transform target;
	public float rotationspeed = 20;
	public float antiBumpFactor = .75f;
	public LayerMask wallLayer;
	
	public RaycastHit hit;
	public RaycastHit hit2;
	public RaycastHit hit3;
	
	protected Animator animator;
	
	
	//private float airspeed;
	private float speed;
	private Transform myTransform ;
	private float distance;
	private Vector3 moveDirection = Vector3.zero;
	private CharacterController controller;
	
	void Awake(){ 
		
		
		animator = GetComponent<Animator>();
		controller = GetComponent<CharacterController>();
		
	}
	
	void Start(){
		//airspeed = defaultAirspeed;
		myTransform = transform;
		speed = defaultspeed;
	}
	
	void Update () {
		
		//Vector3 horizontalVelocity = controller.velocity;
        //horizontalVelocity = new Vector3(controller.velocity.x, 0, controller.velocity.z);
        //float horizontalSpeed = horizontalVelocity.magnitude;
		
		
		if(animator)
		{
			if(Input.GetKeyDown(KeyCode.W)){
				animator.SetFloat("Speed", 10);
			}
		}
		
		
		rotateTowardsTarget();
		
		moveOnGround();
		
		edgeHanging();
		
		if ((controller.collisionFlags & CollisionFlags.Above) != 0){
				moveDirection.y = 0.0f;
			}
		
		
		distance = Vector3.Distance(targetObject.transform.position, transform.position);
		
		
		
		//gravity
		moveDirection.y -= gravity * Time.deltaTime;
		
		// Move the controller
		//controller.Move(((Vector3.ClampMagnitude((target.transform.forward * Input.GetAxis("Vertical")  +  target.transform.right * Input.GetAxis("Horizontal")),1f)) * speed + moveDirection) * Time.deltaTime);
		controller.Move(( moveDirection) * Time.deltaTime);
	
	}
	
	void moveOnGround(){
		if(controller.isGrounded){
			
			
			
			if(distance >0){
				
				
				
					
        		moveDirection = new Vector3 (0f, -antiBumpFactor , 0f);
				
				if(Input.GetButton("run")){
					speed = Mathf.MoveTowards(speed, shiftspeed, 100f * Time.deltaTime);
				}
				
				else{
					speed = defaultspeed;
				}

			}
			else{
				
        		speed = 0f;
        	
        	}
			
			moveDirection = transform.TransformDirection(moveDirection);
			
			moveDirection *= speed ;
			
			
			
			if (Input.GetButtonDown ("Jump") && controller.isGrounded) {
				
            	moveDirection.y = jumpSpeed;

        	}
		}
	}
	
	void edgeHanging(){
		

	
		
		var fwd = transform.TransformDirection(Vector3.forward);
		
		var up = transform.TransformDirection(Vector3.up);
		
		Debug.DrawRay(transform.position + new Vector3(0,3f,0), fwd * 1f, Color.blue);
		
		Debug.DrawRay(transform.position + new Vector3(0,3.5f,0), fwd * 1f, Color.blue);
		
		//Debug.DrawRay(transform.position + new Vector3(0,3.5f,0) + fwd, -up * 1, Color.blue);
		
		
		if(Physics.Raycast(transform.position + new Vector3(0,3f,0), fwd,out hit, 1f) && !Physics.Raycast(transform.position + new Vector3(0,3.5f,0), fwd,out hit2, 1f)){
			
			

			if(hit.transform.gameObject.layer == LayerMask.NameToLayer("walls")){
			Debug.DrawRay(hit.point + fwd * 0.05f + new Vector3(0,4f,0), -up * 5f, Color.blue);
				
			if(Physics.Raycast(hit.point + new Vector3(0,1f,0) + fwd * 0.05f, -up, out hit3, 10f)){
				
				//float edgeYDistance = hit3.point.y - transform.position.y;
					
					
				
					transform.position = Vector3.Lerp (transform.position, new Vector3(transform.position.x, hit3.point.y - 3.4f, transform.position.z), 10f * Time.deltaTime);
				
					
					
				//Debug.Log(edgeYDistance);
				
				//Instantiate(prefab,hit3.point,hit3.transform.rotation);
				//Debug.Log("hey");
			}
				gravity = 0f;
				moveDirection.y = 0f;
				antiBumpFactor = 0f;
				Vector3 rot = Vector3.RotateTowards(-transform.forward,hit.normal,20f * Time.deltaTime,0.0f);
				//var rot = Quaternion.FromToRotation(Vector3.zero,hit.normal);
				//rot = Quaternion.RotateTowards(Quaternion.identity, rot, Time.deltaTime*10);
				transform.rotation = Quaternion.LookRotation(-rot);
				
			}
		}
		else{
			
			
			//controller.height = Mathf.MoveTowards(controller.height, 22f, 50f * Time.deltaTime);
			//controller.center = new Vector3(0f,Mathf.MoveTowards(controller.center.y, 11.35f, 50f * Time.deltaTime),0f);
			
			gravity = 150f;
			antiBumpFactor = 1f;
			
		}
		
	}
	
	void rotateTowardsTarget(){
		if( !((target.position - myTransform.position) == Vector3.zero)){
			myTransform.rotation = Quaternion.Slerp(myTransform.rotation, Quaternion.LookRotation(target.position - myTransform.position), rotationspeed *Time.deltaTime);
		}
	}
	
	/*void LateUpdate(){
		var right2 = transform.TransformDirection(Vector3.right);
		
		Debug.DrawRay(transform.position + new Vector3(0,1f,0), right2 * 2f, Color.blue);
		
		if(Input.GetKey(KeyCode.E)){
		
			if(Physics.Raycast(transform.position + new Vector3(0,1f,0), right2, 2f, wallLayer )){
				
				Debug.Log("rightwallrun");
			}
		}
		if(Input.GetKey(KeyCode.Q)){
		
			if(Physics.Raycast(transform.position + new Vector3(0,1f,0), -right2, 2f, wallLayer )){
				
				Debug.Log("leftwallrun");
			}
		}
	}*/
}
