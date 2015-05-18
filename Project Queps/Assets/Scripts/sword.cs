using UnityEngine;
using System.Collections;

public class sword : MonoBehaviour {
	public GameObject host;
	private Animator animator;
	// Use this for initialization
	void Start () {
		this.host = transform.root.gameObject;
		animator = host.GetComponent<Animator> ();
	}
	
	// Update is called once per frame
	void Update () {

	}
	void OnTriggerEnter(Collider col) {
		if (col.gameObject.tag == "enemy" && (animator.GetCurrentAnimatorStateInfo(0).IsTag("attack") && !animator.IsInTransition(0))){ 
				col.GetComponent<enemyChar> ().damage (20);
		}
	}
}
