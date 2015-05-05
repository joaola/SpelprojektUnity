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
		if (col.gameObject.tag == "Player") {
			if(animator.GetCurrentAnimatorStateInfo(0).IsName("Attack2")){
				col.GetComponent<AnimatorLogic> ().DamagePlayer (40);
			}
			else if(col.GetComponent<AnimatorLogic> ().getShieldWeight() == 1.0f){
				col.GetComponent<AnimatorLogic> ().lowerStamina (5);
				col.GetComponent<AnimatorLogic> ().DamagePlayer (10);
			}
			else
				col.GetComponent<AnimatorLogic> ().DamagePlayer (20);

			Debug.Log(col.GetComponent<AnimatorLogic> ().getHealth());
		}
	}

	// Update is called once per frame
	void Update () {
	
	}
}
