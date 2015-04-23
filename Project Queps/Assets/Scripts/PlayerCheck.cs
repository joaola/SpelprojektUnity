using UnityEngine;
using System.Collections;

public class PlayerCheck : MonoBehaviour {
	private bool seesPlayer=false;
	// Use this for initialization
	void Start () {
	
	}

	void OnTriggerStay(Collider col){
		if (col.gameObject.tag == "Player"){
			this.seesPlayer=true;

		}
	}
	void OnTriggerExit(Collider col){
		if (col.gameObject.tag == "Player"){
			this.seesPlayer=false;
			
		}
	}
	public bool getSeesPlayer()
	{
		return this.seesPlayer;
	}
	// Update is called once per frame
	void Update () {
	
	}
}
