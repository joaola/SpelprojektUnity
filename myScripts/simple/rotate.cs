using UnityEngine;
using System.Collections;

public class rotate : MonoBehaviour {

	public float rotationspeed;
	
	// Update is called once per frame
	void Update () {
		transform.Rotate(Vector3.up * Time.deltaTime * rotationspeed);
	
	}
}
