using UnityEngine;
using System.Collections;

public class sword : MonoBehaviour {
	public GameObject SwordHolder;
	private Animator animator;
	private float attackTimer = 0;
	// Use this for initialization
	void Start () {
		attackTimer = 0;


	}
	
	// Update is called once per frame
	void Update () {
		attackFunc ();
		if(attackTimer > 0)
			attackTimer-= Time.deltaTime;
	}
	void OnTriggerEnter(Collider col) {
		if (col.gameObject.tag == "enemy" && attackTimer > 0) {
			col.GetComponent<enemyChar> ().damage (20,SwordHolder.transform.forward);
		}
	}

	void attackFunc(){
		if (SwordHolder.GetComponent<Animator> ().GetBool ("attack"))
			attackTimer = 0.6f;
	}
}
