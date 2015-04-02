using UnityEngine;
using System.Collections;

public class ghost : MonoBehaviour {

	public GameObject targetobject;
	public Transform target;
	public float rotationspeeed;
	public float antiBumpFactor = 0.7f;
	private float distance = 0;
	private Transform myTransform;

	public float speed = 6.0F;
	public float jumpSpeed = 8.0F;
	public float gravity = 20.0F;
	private Vector3 moveDirection = Vector3.zero;

	void Awake(){
		myTransform = transform;
	}


	void FixedUpdate() {

		distance = Vector3.Distance(targetobject.transform.position, transform.position);
		if (distance>0){
			rotate();
		}

		CharacterController controller = GetComponent<CharacterController>();
		if (controller.isGrounded) {


			moveDirection = new Vector3 (0f, -antiBumpFactor , 1f);
			moveDirection = transform.TransformDirection(moveDirection);
			moveDirection *= speed;


			if (Input.GetButton("Jump"))
				moveDirection.y = jumpSpeed;
			
		}

		moveDirection.y -= gravity * Time.deltaTime;
		controller.Move(/*(target.transform.forward * Input.GetAxis("Vertical") * speed +  target.transform.right * Input.GetAxis("Horizontal") * speed +*/ moveDirection * Time.deltaTime );
	}
	void rotate() {
		myTransform.rotation = Quaternion.Slerp(myTransform.rotation, Quaternion.LookRotation(target.position - myTransform.position), rotationspeeed *Time.deltaTime);
	}
}