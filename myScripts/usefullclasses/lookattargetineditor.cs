using UnityEngine;
using System.Collections;

[ExecuteInEditMode]
public class colourchange : MonoBehaviour {
	public Transform target;
	void Update() {
		if (target)
			transform.LookAt(target);
		
	}
	
}
