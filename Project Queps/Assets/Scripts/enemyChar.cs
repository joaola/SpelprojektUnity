﻿using UnityEngine;
using System.Collections;

public class enemyChar : MonoBehaviour {

	public int enemyHealth;
	private int maxenemyHealth;
	public GameObject DeathCheck;

	private float invTimer = 0.5f;
	// Use this for initialization
	void Start () {
		maxenemyHealth = enemyHealth;
	}
	
	// Update is called once per frame
	void Update () {
		if (enemyHealth <= 0){
			Debug.Log("the enemy has died" + enemyHealth);
			DeathCheck.GetComponent<EnemyCheck>().RemoveEnemy(gameObject);
			//DeathCheck.GetComponent<AnimatorLogic>().setTarget(null);
			Destroy(gameObject, 1f);

		}

		if(invTimer>0)
			invTimer-=Time.deltaTime;
	}

	/*void OnCollisionEnter(Collision col) {
		if(invTimer<0)
		{
			damage (20);
			Debug.Log (enemyHealth);
			invTimer = 2f;
		}
	}*/
	

	public void damage(int value,Vector3 forceDir)
	{
		if (invTimer < 0) {
			//transform.Translate(Vector3.up*0.2f);
			//GetComponent<Rigidbody>().AddRelativeForce ((forceDir+new Vector3(0,0.5f,0))* 5000f);
			enemyHealth -= value;
			Debug.Log(enemyHealth);
			invTimer = 0.4f;
		}
	}
}
