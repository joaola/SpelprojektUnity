using UnityEngine;
using System.Collections;
using System.Collections.Generic;



public class Looting : MonoBehaviour {
	public bool containPlayer = false;
	public GameObject looter;
	private Inventory inv;
	public int lootedArea;
	private Random rnd = new Random();
	public int pickup;

	// Use this for initialization
	void Start () {
		lootedArea = 0;
		containPlayer = false;
		inv = looter.GetComponent<Inventory>();
	}

	// Update is called once per frame
	void Update () {
		if (containPlayer && Input.GetKey (KeyCode.E) && lootedArea == 0) {
			inv.AddItem (Random.Range(0,9));
			//inv.AddItem(pickup);
			lootedArea = 1;
		}

		if (containPlayer == false) {
			lootedArea = 0;
		}
			
	}

	void OnTriggerStay(Collider other) {
		
		if(other.gameObject.tag == "Player")
			this.containPlayer = true;
	}

	void OnTriggerExit(Collider other) {
		
		if(other.gameObject.tag == "Player")
			this.containPlayer = false;
	}

	private void OnGUI(){
		// used for showing fov, fps and healthbar
		
		if (containPlayer) {
			GUI.contentColor = Color.black;
			GUI.Label (new Rect (100, 80, 300, 20), "Press E to Loot" );
		}
	}


}
