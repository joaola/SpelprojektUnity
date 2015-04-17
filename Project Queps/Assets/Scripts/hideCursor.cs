using UnityEngine;
using System.Collections;

public class hideCursor : MonoBehaviour {

	// Use this for initialization
	void Start () {
		// lock cursor and make it invisible
		Cursor.visible = false;
		//Cursor.lockState = CursorLockMode.Locked;
		
		// unlock cursor if escape is pressed
		//if(Input.GetKeyDown(KeyCode.Escape))
			//Cursor.lockState = CursorLockMode.None;
	}
	
	// Update is called once per frame
	void Update () {

	}
	
}
