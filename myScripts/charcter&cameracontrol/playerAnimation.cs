using UnityEngine;
using System.Collections;

public class playerAnimation : MonoBehaviour {
	CharacterController controller;
	public float slowWalkLimit;
	public float walkLimit;
	public float runLimit;
	
	public static float distance = 1;
	public RaycastHit hit;
	public RaycastHit hit2;
	
	public AnimationClip hangingAnimation;
	public AnimationClip idleAnimation;
	public AnimationClip walkAnimation;
	public AnimationClip runAnimation;

	// Use this for initialization
	void Awake () {
		controller = GetComponent<CharacterController>();
	}
	
	// Update is called once per frame
	void LateUpdate () {
		
		
		
		var fwd = transform.TransformDirection(Vector3.forward);
		
        Vector3 horizontalVelocity = controller.velocity;
        horizontalVelocity = new Vector3(controller.velocity.x, 0, controller.velocity.z);
        float horizontalSpeed = horizontalVelocity.magnitude;
        //float verticalSpeed = controller.velocity.y;
        //float overallSpeed = controller.velocity.magnitude;
		
		//Debug.Log(horizontalSpeed);
		
		if(horizontalSpeed>walkLimit && horizontalSpeed<runLimit){
			GetComponent<Animation>().CrossFade(walkAnimation.name, 0.2f);
		}
		else if(horizontalSpeed>runLimit){
			GetComponent<Animation>().CrossFade(runAnimation.name, 0.1f);
		}
		else if(GetComponent<Animation>()["hanging"].enabled == false){
			//animation.CrossFade(idleAnimation.name, 0.2f);
		}
		
		if(Input.GetKeyDown(KeyCode.G)){
			GetComponent<Animation>().Play("slowwalk");
		}
		
		
		if(Physics.Raycast(transform.position + new Vector3(0,3f,0), fwd,out hit, 1f) && !Physics.Raycast(transform.position + new Vector3(0,3.5f,0), fwd,out hit2, 1f)){

			if(hit.transform.gameObject.layer == LayerMask.NameToLayer("walls")){
				GetComponent<Animation>().CrossFade("hanging", 0.1f);
				
			}
		}
		
		
		
		/*if(Physics.Raycast(transform.position + new Vector3(0,0.5f,0), fwd,out hit, distance)){
			
			Debug.Log(LayerMask.NameToLayer("walls"));
			if(hit.transform.gameObject.layer == LayerMask.NameToLayer("walls")){
				
				Debug.Log(LayerMask.LayerToName(hit.transform.gameObject.layer));
				
				
				
			}
			
		}*/
		
		
		
	
	}
}
