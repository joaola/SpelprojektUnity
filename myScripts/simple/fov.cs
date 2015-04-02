using UnityEngine;
using System.Collections;

public class fov : MonoBehaviour {
	

	
	// Update is called once per frame
	void Update () {
		float fieldov = Camera.main.fieldOfView;
		
	 GetComponent<GUIText>().text ="fov:" + fieldov;
	}
}
