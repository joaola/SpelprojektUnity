using UnityEngine;
using System.Collections;

public class PlayerCheck : MonoBehaviour {
	public bool playerincone=false;
	public GameObject host;
	private RaycastHit hit;
	public LayerMask layermask;
	public GameObject player;
	private float timer = 0;

	// Use this for initialization
	void Start () {
		this.host = transform.root.gameObject;
	}

	void OnTriggerStay(Collider col){
		if (col.gameObject.tag == "Player") {
			playerincone = true;
		}
	}
	void OnTriggerExit(Collider col)
	{
		if (col.gameObject.tag == "Player") {
			playerincone = false;
		}
	}

	public bool LineOfSight(){
		// check if the player is in line of sight
		if (Physics.Linecast(transform.position, player.transform.position, out hit, layermask)) {
			if (hit.transform.name != player.transform.name){
				return false;
			}
		}
		return true;
	}
	// Update is called once per frame
	void Update () {
		if (playerincone && LineOfSight())
			host.GetComponent<Animator>().SetBool("seesPlayer",true);
		else 
			host.GetComponent<Animator>().SetBool("seesPlayer",false);
	}
}
