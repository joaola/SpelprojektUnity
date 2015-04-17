using UnityEngine;
using System.Collections;

public class camerafovchange : MonoBehaviour {
	public float defaultfov = 60;
	public float fov = 60;
	public float timer = 0;
	public float changespeed = 6;
	private float alpha = 0f;

	
	// Use this for initialization
	void Start () {
		Camera.main.GetComponent<Camera>().fieldOfView = fov;
	}
	
	// Update is called once per frame
	void Update () {
		float fieldov = Camera.main.fieldOfView;
		
	 	//GetComponent<GUIText>().text ="fov:" + fieldov;
		
		//GetComponent<GUIText>().material.color = new Color(1f,1f,1f, alpha);
		
		if(Input.GetKey(KeyCode.M)){
			fov += Time.deltaTime * changespeed;
			timer = 3;
			if(alpha<1){
				alpha += Time.deltaTime * 3;
			}
			changefov();
		}
		if(Input.GetKey(KeyCode.N)){
			fov -= Time.deltaTime * changespeed;
			timer = 3;
			if(alpha<1){
				alpha += Time.deltaTime * 3;
				
			}
			
			changefov();
		}
		if(Input.GetKeyDown(KeyCode.B) && fov!=defaultfov){
			fov = defaultfov;
			alpha = 1;
			timer = 3;
			changefov();
		}
		if(timer>0){
			timer-= Time.deltaTime;
		}
		
		if(timer<0 && alpha>0 ){
			alpha-=Time.deltaTime;
		}
	}
	void changefov(){
		Camera.main.GetComponent<Camera>().fieldOfView = fov;
	}
}
