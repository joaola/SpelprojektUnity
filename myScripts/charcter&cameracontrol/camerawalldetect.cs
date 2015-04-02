using UnityEngine;
using System.Collections;

public class camerawalldetect : MonoBehaviour {
	
	public float speed;

	
	void OnTriggerStay() {

			transform.Translate(Vector3.forward * speed * Time.deltaTime);
			Debug.Log ("collides");
		
	}
}
