using UnityEngine;
using System.Collections;

public class PlayerCheck : MonoBehaviour {
	private bool seesPlayer=false;
	public GameObject host;
	private GameObject player;
	// Use this for initialization
	void Start () {
		this.host = transform.root.gameObject;
	}

	void OnTriggerStay(Collider col){
		if (col.gameObject.tag == "Player"){
			//Debug.Log("I see you");
			host.GetComponent<Animator>().SetBool("seesPlayer",true);
		}
	}
	void OnTriggerExit(Collider col){
		if (col.gameObject.tag == "Player"){
			Debug.Log("Player Exited FOV");
			host.GetComponent<Animator>().SetBool("seesPlayer",false);
			
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
