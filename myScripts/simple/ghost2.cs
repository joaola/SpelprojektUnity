using UnityEngine;
using System.Collections;

public class ghost2 : MonoBehaviour {

	//public GameObject targetobject;
	public Transform target;
	public float rotationspeeed;
	//private float movement=5;
	private float distance = 0;
	private Transform myTransform;

	// Use this for initialization
	void Start () {
		myTransform = transform;
	}
	
	// Update is called once per frame
	void FixedUpdate () {

		distance = Vector3.Distance(target.position, transform.position);

		rotate();

		//if (distance>1){
			transform.Translate( Vector3.forward * Time.deltaTime * 2 * (distance/4) );
		//}


	}
	void rotate() {
		myTransform.rotation = Quaternion.Slerp(myTransform.rotation, Quaternion.LookRotation(target.position - myTransform.position), rotationspeeed *Time.deltaTime);
	}
}
