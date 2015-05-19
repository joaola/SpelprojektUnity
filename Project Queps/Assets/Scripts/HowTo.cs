using UnityEngine;
using System.Collections;

public class HowTo : MonoBehaviour {
	public GameObject camera;
	//public int speed=1;
	public string level;

	void Update(){
		//camera.transform.Translate (Vector3.down * Time.deltaTime * speed);

		if(Input.GetKey(KeyCode.Escape))
			Application.LoadLevel (level);
	}




}
