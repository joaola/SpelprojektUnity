using UnityEngine;
using System.Collections;

public class cam : MonoBehaviour {

	public GameObject Creature;

 	public LayerMask layermask;
	public Transform target;
	public float height;
	public float sidestep = 1.0f;
	public float distance;
	public float camColRad ;
	public float smooth;
	
	public float x;
	public float xspeed;
	public float y;
	public float yspeed;
	public float yMin;
	public float yMax;
	
	private float sidestep2;
	private Quaternion rotation;
	private Vector3 position;
	private RaycastHit hit;
	private Vector3 hit2;
	private float tempDistance;

	void Start () {
		sidestep2 = 0;
	}
	
	// Update is called once per frame
	void LateUpdate () {
		x += xspeed* Input.GetAxis("Mouse X");
		y -= yspeed* Input.GetAxis("Mouse Y");
		if(y > yMax){
			y = yMax;
		}
		else if(y < yMin){
			y = yMin;
		}
		if(Input.GetButtonDown("tab") && sidestep2>0 && sidestep2 != 0){
			//Creature.GetComponent<AnimatorLogic>().DamagePlayer(10);
			sidestep2 *=(-1);
		}
		else if(Input.GetButtonDown("tab") && sidestep2<0){
			sidestep2 = 0;
		}
		else if(Input.GetButtonDown("tab") && sidestep2 == 0){
			sidestep2 = sidestep;
		}
		
		var rotation =  Quaternion.Euler(y,x,0);
		var position = rotation * new Vector3(sidestep2,height, -distance) + target.position ;
		
		transform.rotation = rotation;
		transform.position =  position;
		
		
		if(Physics.Linecast(target.position-transform.forward,transform.position, out hit, layermask)){
			tempDistance = Vector3.Distance(target.position,hit.point) ;
			position = rotation * new Vector3(sidestep2, height, -tempDistance) + target.position;
			transform.position = position+ transform.forward;			
		}
		
	}
	/*void OnCollisionStay(Collision collision){
		
	}*/
}
