using UnityEngine;
using System.Collections;

public class copyPosition : MonoBehaviour {
	
	public Vector3 offset;
	public Transform target;

	// Use this for initialization
	void Start () {
		transform.position = target.transform.position + offset;
	}
	
	// Update is called once per frame
	void LateUpdate () {
		transform.position = new Vector3(target.transform.position.x, transform.position.y ,target.position.z) ;

	
	}
}
