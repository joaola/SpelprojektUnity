using UnityEngine;
using System.Collections;

public class enemyChar : MonoBehaviour {

	public int enemyHealth;
	public int maxenemyHealth;
	public Transform playerTarget;
	public float distance;
	public float attackDistance = 5.5f;
	public Vector3 lastknownpos;
	private RaycastHit hit;
	public LayerMask layermask;

	private Animator animator;
	private int searchrotation=-1;
	private NavMeshAgent agent;
	private float invTimer;
	private float startInvTimer = 0.57f;
	// Use this for initialization
	void Start () {

		lastknownpos = gameObject.transform.position;
		animator = GetComponent<Animator>();
		agent = GetComponent<NavMeshAgent>();
		agent.stoppingDistance = attackDistance;
		maxenemyHealth = enemyHealth;
		invTimer = startInvTimer;
	}
	
	// Update is called once per frame
	void Update () {
		distance = Vector3.Distance (transform.position, playerTarget.position);
		PlayerCheck ();
		searchfunc ();
		DamageChecker ();
		if(!animator.GetCurrentAnimatorStateInfo (0).IsName ("Attack2"))
			agent.destination = lastknownpos;
		lookAtplayer ();
	}
	void searchfunc()
	{
		if(animator.GetFloat("searchTimer")<=0 || animator.GetBool("seesPlayer"))
			searchrotation = -1;

		if (animator.GetCurrentAnimatorStateInfo (0).IsName ("rotate")) {
			if(searchrotation==-1){
				Vector3 toTarget = (lastknownpos - transform.position).normalized;
				if(Vector3.Dot(toTarget,transform.right)>0){
					// right
					searchrotation = 1;
				}
				else{
					// left
					searchrotation = 0;
				}
			}
			if(searchrotation ==1)
				animator.SetFloat ("x", 1);
			else if(searchrotation ==0)
				animator.SetFloat ("x", 0);
		}
	}

	void PlayerCheck(){

		if (LineOfSight()) 
		{
			Vector3 toTarget = (playerTarget.position - transform.position).normalized;
			// in front
			if(Vector3.Dot(toTarget,transform.forward)>0 && distance<62.5f){
				animator.SetBool("seesPlayer",true);
			}
			// behind
			else if(Vector3.Dot(toTarget,transform.forward)<0 && distance<15.0f){
				animator.SetBool("seesPlayer",true);
			}
			else{ // don't see
				animator.SetBool("seesPlayer",false);
				animator.SetFloat("searchTimer", animator.GetFloat("searchTimer")- Time.deltaTime);
				if(!animator.GetCurrentAnimatorStateInfo(0).IsName("rotate"))
					animator.SetFloat("searchTimer", 10f);
			}
		}
		else{ // don't see
			animator.SetBool("seesPlayer",false);
			animator.SetFloat("searchTimer", animator.GetFloat("searchTimer")- Time.deltaTime);
			if(!animator.GetCurrentAnimatorStateInfo(0).IsName("rotate"))
				animator.SetFloat("searchTimer", 10f);
		}
	}


	void DamageChecker()
	{
		if (enemyHealth <= 0){
			Debug.Log("the enemy has died" + enemyHealth);
			playerTarget.GetComponent<AnimatorLogic>().enemies.Remove(gameObject);
			Destroy(gameObject, 1f);
		}
		if (invTimer > 0) {
			invTimer -= Time.deltaTime;
		}

	}

	public void lookAtplayer()
	{
		if (animator.GetBool ("seesPlayer") && !animator.GetCurrentAnimatorStateInfo (0).IsName ("Attack2")) {
			if (distance <= agent.stoppingDistance) {
				var newRotation = Quaternion.LookRotation (playerTarget.position - transform.position).eulerAngles;
				newRotation.x = 0;
				newRotation.z = 0;
				transform.rotation = Quaternion.Slerp (transform.rotation, Quaternion.Euler (newRotation), Time.deltaTime * 6f);
			}
			moveTowardsPlayer ();
		}

		else if (animator.GetBool ("seesPlayer")) {
			lastknownpos=playerTarget.position;
		}
	}

	void moveTowardsPlayer(){

		if (distance <= attackDistance) {
			lastknownpos=transform.position;
			if (animator.GetBool ("attackPlayer") != true)
				animator.SetInteger ("attackpick", Random.Range (0, 3));
			animator.SetTrigger ("attackPlayer");

		} else {
			animator.ResetTrigger("attackPlayer");
			lastknownpos=playerTarget.position;
		}
	}

	public bool LineOfSight(){
		// check if the player is in line of sight
		if (Physics.Linecast(transform.Find ("Armature_001/Bone/spine/rib/neck/head").position, playerTarget.FindChild("camtarget").position, out hit, layermask)) {
			if (hit.transform.name != playerTarget.name){
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
			invTimer = startInvTimer;
		}
	}

}
