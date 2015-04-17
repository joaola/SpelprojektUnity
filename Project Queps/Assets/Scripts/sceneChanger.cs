using UnityEngine;
using System.Collections;

public class sceneChanger : MonoBehaviour {

	public string nextscene;
	public bool containPlayer = false;

	// Use this for initialization
	void Start () {
		containPlayer = false;
	}
	
	// Update is called once per frame
	void Update () {
		if(containPlayer && Input.GetKey(KeyCode.T))
			Application.LoadLevel (nextscene);
	}

	void OnTriggerStay(Collider other) {
		
		if(other.gameObject.tag == "Player")
			this.containPlayer = true;
	}
	void OnTriggerExit(Collider other) {
		
		if(other.gameObject.tag == "Player")
			this.containPlayer = false;
	}

	private void OnGUI(){
		// used for showing fov, fps and healthbar

		if (containPlayer) {
			GUI.contentColor = Color.black;
			GUI.Label (new Rect (100, 80, 300, 20), "Press  t to move to " + nextscene);
		}
	}
}
