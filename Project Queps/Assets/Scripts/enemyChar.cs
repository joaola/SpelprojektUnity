using UnityEngine;
using System.Collections;

public class enemyChar : MonoBehaviour {

	public int enemyHealth;
	private int maxenemyHealth;
	public GameObject DeathCheck;
	public Transform playerTarget;
	public GameObject fovCone;
	public LayerMask layermask;
	private RaycastHit hit;
	public float distance;

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
		LineOfSight ();
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
		if (animator.GetBool("seesPlayer") && !animator.GetCurrentAnimatorStateInfo(0).IsName("Attack2")) {
			var newRotation = Quaternion.LookRotation (playerTarget.position - transform.position).eulerAngles;
			newRotation.x = 0;
			newRotation.z = 0;
			transform.rotation = Quaternion.Slerp (transform.rotation, Quaternion.Euler (newRotation), Time.deltaTime * 6f);

			moveTowardsPlayer();
		}
		if(animator.GetCurrentAnimatorStateInfo(0).IsName("Attack2")){
			transform.rotation = transform.rotation;
		}
	}

	void moveTowardsPlayer(){
		distance = Vector3.Distance (transform.position, playerTarget.position);
		if (distance < 1.8f) {
			animator.SetFloat ("y", 0);
			if (animator.GetBool ("attackPlayer") != true)
				animator.SetInteger ("attackpick", Random.Range (0, 3));
			animator.SetTrigger ("attackPlayer");

			if(animator.GetCurrentAnimatorStateInfo(0).IsName("Attack2")){
				transform.rotation = transform.rotation;
			}

		} else {
			animator.ResetTrigger("attackPlayer");
			animator.SetFloat ("y", 1);
		}
	}

	public bool LineOfSight(){
			// check if the player is in line of sight
		if (Physics.Linecast(transform.position, playerTarget.position, out hit, layermask)) {
			if (hit.transform.name != playerTarget.transform.name){
				animator.SetBool("seesPlayer", false);

				return false;
			}
		}
		return true;
	}

	public void damage(int value)
	{
		if (invTimer < 0) {
			animator.SetTrigger("hit");
			enemyHealth -= value;
			Debug.Log(enemyHealth);
			invTimer = 0.65f;
		}
	}

}
