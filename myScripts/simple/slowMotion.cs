using UnityEngine;
using System.Collections;

public class slowMotion : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update() {
		if (Input.GetButtonDown("slow")) {
			if (Time.timeScale == 1.0F){
				Time.timeScale = 0.1F;
				//Time.fixedDeltaTime =  0.02f * Time.timeScale;
			}
			else{
				Time.timeScale = 1.0F;
				//Time.fixedDeltaTime =  1.0f;
			}
			Time.fixedDeltaTime = 0.02F * Time.timeScale;
		}
	}
}
