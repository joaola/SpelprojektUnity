using UnityEngine;
using System.Collections;

public class charaterAnimatorController : MonoBehaviour {
	
	[SerializeField]
	private Animator animator;
	[SerializeField]
	private float directionDampTime = 1.5f;
	[SerializeField]
	private Camera gamecam;
	[SerializeField]
	private float directionSpeed = 3.0f;
	
	private float speed = 0.0f;
	private float direction = 0.0f;
	private float horizontal = 0.0f;
	private float vertical = 0.0f;
	
	
	void Start(){ 
		animator = GetComponent<Animator>();
		
		if(animator.layerCount>=2){
			animator.SetLayerWeight(1, 1);
		}
	}
	
	// Update is called once per frame
	void Update () {
		
		if(animator)
		{
			horizontal = Input.GetAxis("Horizontal");
			vertical = Input.GetAxis("Vertical");
			
			speed = new Vector2(horizontal, vertical).sqrMagnitude;
			
			stickToWorldSpace(this.transform, gamecam.transform, ref direction, ref speed);
			
			animator.SetFloat("Speed",speed);
			animator.SetFloat("Direction",direction,directionDampTime,Time.deltaTime);
			
			
			
		}
		
	}
	
	
	
	public void stickToWorldSpace(Transform root, Transform camera, ref float directionOut, ref float speedOut){
		
		Vector3 rootDirection = root.forward;
		
		Vector3 stickDirection = new Vector3(horizontal,0, vertical);
		
		speedOut = stickDirection.sqrMagnitude;
		
		//get camera rotation
		Vector3 cameraDirection = camera.forward;
		cameraDirection.y = 0.0f;//kill Y
		Quaternion referentialShift = Quaternion.FromToRotation(Vector3.forward, cameraDirection);
		
		Vector3 moveDirection = referentialShift * stickDirection;
		Vector3 axisSign = Vector3.Cross(moveDirection, rootDirection);
		
		//Debug.DrawRay(new Vector3(root.position.x, root.position.y + 2f,root.position.z), moveDirection, Color.green);
		//Debug.DrawRay(new Vector3(root.position.x, root.position.y + 2f,root.position.z), axisSign, Color.red);
		//Debug.DrawRay(new Vector3(root.position.x, root.position.y + 2f,root.position.z), rootDirection, Color.magenta);
		//Debug.DrawRay(new Vector3(root.position.x, root.position.y + 2f,root.position.z), stickDirection, Color.blue);
		
		float angleRootToMove = Vector3.Angle(rootDirection,moveDirection) * (axisSign.y >= 0 ? -1f : 1f);
		
		angleRootToMove /= 180f;
		
		directionOut = angleRootToMove * directionSpeed;
		
	}
	
}
