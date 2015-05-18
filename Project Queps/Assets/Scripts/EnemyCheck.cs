using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EnemyCheck : MonoBehaviour {
	public Transform target;
	private GameObject targ;
	public GameObject player;
	private GameObject nextTargObj;
	private Transform nextTarget;
	public List<GameObject> triggers = new List<GameObject>();

	// Use this for initialization
	void Start () {
		targ = null;
		nextTarget = null;
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		if (Input.GetButtonDown ("lock on")) {
			targ = FindClosestEnemy();
			//nextTargObj = FindNextEnemy(targ);

			if(targ != null){
				target = targ.transform;
			}
			/*if(nextTargObj != null){
				nextTarget = nextTargObj.transform;
			}*/
			if(triggers.Count == 0){
				target = null;
			}
		}
	}

	void OnTriggerStay(Collider col){
		if (col.gameObject.tag == "enemy" && !triggers.Contains(col.gameObject)) {
			triggers.Add(col.gameObject);
		}
	}

	void OnTriggerExit(Collider col){
		if (col.gameObject.tag == "enemy") {
			RemoveEnemy(col.gameObject);
		}
	}

	public void RemoveEnemy(GameObject enemy){
		triggers.Remove (enemy);
	}

	#region getters
	public Transform getTarget(){
		return this.target;
	}

	public GameObject getNextTarget(){
		return this.nextTargObj;
	}

	public GameObject getObject(){
		return this.targ;
	}

	public List<GameObject> getTriggers(){
		return this.triggers;
	}
	#endregion

	GameObject FindClosestEnemy(){
		GameObject closest = null;
		float distance = Mathf.Infinity;
		Vector3 position = transform.position;

		foreach (GameObject go in triggers) {
			if(go != null){
				Vector3 diff = go.transform.position - position;
				float curDistance = diff.sqrMagnitude;
				if (curDistance < distance) {
					closest = go;
					distance = curDistance;
				}
			}
		}
		return closest;
	}

	public GameObject FindNextEnemy(GameObject currTarget){
			List<GameObject> TempTriggers = triggers;
			if (currTarget != null)
				TempTriggers.Remove (currTarget);
			
			GameObject next = null;
			float distance = Mathf.Infinity;
			Vector3 position = transform.position;
		
			foreach (GameObject go in TempTriggers) {
				if (go != null) {
					Vector3 diff = go.transform.position - position;
					float curDistance = diff.sqrMagnitude;
					if (curDistance < distance) {
						next = go;
						distance = curDistance;
					}
				}
			}
			return next;
	}
}
