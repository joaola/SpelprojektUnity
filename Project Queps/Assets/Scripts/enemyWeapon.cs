using UnityEngine;
using System.Collections;

public class enemyWeapon : MonoBehaviour {
	public GameObject host;
	private Animator animator;

	// Use this for initialization
	void Start () {
		this.host = transform.root.gameObject;
		animator = host.GetComponent<Animator> ();
	}

	void OnTriggerEnter(Collider col) {
		if (col.gameObject.tag == "Player" && animator.GetCurrentAnimatorStateInfo(0).IsTag("enemyAttack")) {
			if(animator.GetCurrentAnimatorStateInfo(0).IsName("Attack2")){
				col.GetComponent<AnimatorLogic> ().DamagePlayer (40);
			}
			else if(col.GetComponent<AnimatorLogic> ().getShieldWeight() == 1.0f && animator.GetCurrentAnimatorStateInfo(0).IsName("Attack1")){
				col.GetComponent<AnimatorLogic> ().lowerStamina (5);
				col.GetComponent<AnimatorLogic> ().DamagePlayer (10);
			}
			else
				col.GetComponent<AnimatorLogic> ().DamagePlayer (20);
		}
	}

	// Update is called once per frame
	void Update () {
	
	}
}
