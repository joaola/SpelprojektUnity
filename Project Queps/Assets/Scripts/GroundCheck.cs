using UnityEngine;
using System.Collections;

public class GroundCheck : MonoBehaviour {
	public GameObject Creature;


	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void FixedUpdate () {
		Creature.GetComponent<Animator>().SetBool("groundCheck",false);
	}

	void OnTriggerStay(Collider other) {

		if(other.gameObject.tag != "Player" && other.gameObject.tag != "EnemyCheck"&& other.gameObject.tag != "shield" && other.gameObject.tag != "enemyWeapon")
			Creature.GetComponent<Animator>().SetBool("groundCheck",true);
		//Debug.Log("ground");

	}

}
