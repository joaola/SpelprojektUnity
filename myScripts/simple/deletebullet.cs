using UnityEngine;
using System.Collections;

public class deletebullet : MonoBehaviour {

	public float scaletimestime = -0.01f;
	public bool coll = false;
	public float delaytimer = 10;
	
void Start() {
    }
	
void LateUpdate(){
		if (coll == false){
			delaytimer -= Time.deltaTime;
		}
		if (coll == true){
			delaytimer -= Time.deltaTime;
		}
		if (delaytimer < 0){
			transform.localScale += new Vector3(scaletimestime,scaletimestime,scaletimestime);
			var objscale = transform.lossyScale.x;
			
			
			if(objscale < 0 ){
				Destroy(gameObject);
			}
		}
	}
	
void OnCollisionEnter (Collision theCollision){
		
			delaytimer = 5;
			coll = true;
}

}