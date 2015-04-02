using UnityEngine;
using System.Collections;

public class GroundCheck : MonoBehaviour {
	public GameObject Creature;


	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void FixedUpdate () {
		Creature.GetComponent<AnimatorLogic>().setGroundCheck(false); 
	}

	void OnTriggerStay(Collider other) {

		Creature.GetComponent<AnimatorLogic>().setGroundCheck(true);
		//Debug.Log("ground");

	}

}
