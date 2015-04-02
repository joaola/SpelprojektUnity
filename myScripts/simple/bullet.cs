using UnityEngine;
using System.Collections;

public class bullet : MonoBehaviour {

	public GameObject prefab = null;
	public int shootpower;
	public AudioClip gunshot;
	
    void Update() {
		if(Input.GetMouseButtonDown(0)){
			GameObject bullet = Instantiate(prefab, transform.position, transform.rotation) as GameObject;
			 bullet.GetComponent<Rigidbody>().AddForce(transform.forward * shootpower);
			GetComponent<AudioSource>().PlayOneShot(gunshot);
		}
		
        
	}
}