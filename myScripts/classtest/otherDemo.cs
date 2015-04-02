using UnityEngine;
using System.Collections;

public class otherDemo : MonoBehaviour {
	public AnimatorLogic playerscript;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if(playerscript.health<=0)
			Debug.Log("capsule says player has died");
	}
}
