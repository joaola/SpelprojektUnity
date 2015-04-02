/*using UnityEngine;
using System.Collections;

public class movetargetparent : MonoBehaviour {

	public GameObject follower;
	void Start () {
		transform.position = follower.transform.position;
	}
	
	// Update is called once per frame
	void Update () {
		
		if(Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.S) ){
			move ();
		}
		else{
			still();
		}
	}
	
		void move(){
		float forward2 = 0;
		float backward2 = 0;
		float right2 = 0;
		float left2 = 0;
		if(Input.GetKey(KeyCode.W)){
			forward2 = 5;
		}
		if(Input.GetKey(KeyCode.S)){
			backward2 = 5;
		}
		if(Input.GetKey(KeyCode.D)){
			right2 = 90;
		}
		if(Input.GetKey(KeyCode.A)){
			left2 = 90;
		}
		if(Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.S) ){
			var followpos = follower.transform.position;
			transform.position = followpos;
			   transform.localScale = new Vector3(5,0,5);
		}
		
	}
	
	void still(){
		
		var followpos = follower.transform.position;
		transform.position = followpos;
		transform.localScale = new Vector3(2,0,2);
	}
	
}
 */