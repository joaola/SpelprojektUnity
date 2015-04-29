using UnityEngine;
using System.Collections;

public class enemyChar : MonoBehaviour {

	public int enemyHealth;
	private int maxenemyHealth;
	public GameObject DeathCheck;
	public Transform playerTarget;
	public GameObject fovCone;

	private Animator animator;
	private float invTimer = 0.65f;
	// Use this for initialization
	void Start () {
		animator = GetComponent<Animator>();
		maxenemyHealth = enemyHealth;
	}
	
	// Update is called once per frame
	void Update () {
		DamageChecker ();
		lookAtplayer ();
	}

	void DamageChecker()
	{
		if (enemyHealth <= 0){
			Debug.Log("the enemy has died" + enemyHealth);
			DeathCheck.GetComponent<EnemyCheck>().RemoveEnemy(gameObject);
			//DeathCheck.GetComponent<AnimatorLogic>().setTarget(null);
			Destroy(gameObject, 1f);
		}
		if (invTimer > 0) {
			invTimer -= Time.deltaTime;
		}

	}

	public void lookAtplayer()
	{
		if (animator.GetBool("seesPlayer")) {
			var newRotation = Quaternion.LookRotation (playerTarget.position - transform.position).eulerAngles;
			newRotation.x = 0;
			newRotation.z = 0;
			transform.rotation = Quaternion.Slerp (transform.rotation, Quaternion.Euler (newRotation), Time.deltaTime * 6f);
		}
	}

	public void damage(int value)
	{
		if (invTimer < 0) {
			//animator.SetBool("hit",true);
			enemyHealth -= value;
			Debug.Log(enemyHealth);
			invTimer = 0.65f;
		}
	}

}
