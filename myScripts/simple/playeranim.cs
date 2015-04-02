using UnityEngine;
using System.Collections;

public class playeranim : MonoBehaviour {

    void Update(){
		if(Input.GetKey(KeyCode.S)){
			GetComponent<Animation>().Play("runtest");
		}
	}
}
