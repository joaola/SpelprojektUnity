using UnityEngine;
using System.Collections;

public class MouseAimCamera : MonoBehaviour {
	public GameObject target;
	public GameObject targetfollower;
	public float rotateSpeed = 5;
	Vector3 offset;
	

	void Start () {
		offset = target.transform.position - transform.position;
	}

	void Update () {
		target.transform.position = targetfollower.transform.position;
		
		float right2 = 0;
		float left2 = 0;
		float forward2 =0;
		float backward2 =0;
		
		if(Input.GetKey(KeyCode.W)){
			forward2 = 3;
		}
		if(Input.GetKey(KeyCode.S)){
			backward2 = 3;
		}
		if(Input.GetKey(KeyCode.D)){
			right2 = 3;
		}
		if(Input.GetKey(KeyCode.A)){
			left2 = 3;
		}
		
		target.transform.localPosition +=  target.transform.forward * (forward2-backward2);
		target.transform.localPosition +=  target.transform.right * (right2-left2);
		
		
		
		float horizontal = Input.GetAxis("Mouse X") * rotateSpeed ;
		//float vertical = Input.GetAxis("Mouse Y") * rotateSpeed ;
		
		target.transform.Rotate(0, horizontal, 0);
		
	
		
		
		
		float desiredAngle = target.transform.eulerAngles.y;
		
		Quaternion rotation = Quaternion.Euler(0, desiredAngle, 0);
		
		transform.position = targetfollower.transform.position - (rotation * offset);
		
		transform.LookAt(targetfollower.transform);
		
		
	
	}

}
