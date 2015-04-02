using UnityEngine;
using System.Collections;

public class PlayerHealth : MonoBehaviour {
	public int maxHealth = 100;
	public int curHealth = 100;
	
	public float healthbarlength;
	
	// Use this for initialization
	void Start () {
		healthbarlength = Screen.width / 3;
	
	}
	
	// Update is called once per frame
	void Update () {
		AddjustCurrentHealth(0);
	
	}
	
	void OnGUI() {
		GUI.Box(new Rect(10, 10, healthbarlength, 20), curHealth + "/" + maxHealth);
		
	}
	public void AddjustCurrentHealth(int adj){
		curHealth += adj;
		
		if(curHealth<0){
			curHealth = 0;
		}
		
		if(curHealth>maxHealth){
			curHealth = maxHealth;
		}
		if(maxHealth<1){
			maxHealth = 1;
		}
		
		healthbarlength = (Screen.width / 3) * (curHealth / (float)maxHealth);
		
	}
}


