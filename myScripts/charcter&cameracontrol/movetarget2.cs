using UnityEngine;
using System.Collections;

public class movetarget2 : MonoBehaviour {
	
	public GameObject theCamera;
	public GameObject player;
	

	// Use this for initialization
	void Start () {
		transform.position = player.transform.position;
	
	}
	
	// Update is called once per frame
	void LateUpdate () {
		transform.position = player.transform.position;
		float camsYRot = theCamera.transform.eulerAngles.y;
		transform.eulerAngles = new Vector3 (0,camsYRot,0);
		
		
		
		float forward2 =0;
		float right2 = 0;
		

		forward2 = 3 * Input.GetAxis("Vertical");
		

		right2 = 3 * Input.GetAxis("Horizontal");
		
		
		transform.localPosition +=  transform.forward * (forward2) + transform.right * (right2);
	
	}
}
