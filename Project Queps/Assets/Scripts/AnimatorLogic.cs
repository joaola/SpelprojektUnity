using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AnimatorLogic : MonoBehaviour {
	private Animator animator;
	public AudioClip jumpsound;

	public Camera daCam;

	#region Player variables
	public float health;
	public float maxHealth;
	public float rotationspeed;
	public float jumppower = 840;
	private float speed;
	private float horizontal = 0.0f;
	private float vertical = 0.0f;
	public GameObject sword;
	private float invTimer = 0.0f;
	private float stamina;
	#endregion

	#region targetting
	public Transform target;
	private Transform temptarget;
	private GameObject targ;
	public List<GameObject> enemies = new List<GameObject>();
	//public GameObject targetcheck;
	private float damping = 30.0f;
	private RaycastHit hit;
	public LayerMask layermask;
	private float shieldWeight;
	#endregion
	
	#region fps
	private float updateinterval = 0.5f;
	private float accum = 0.0f; // FPS accumulated over the interval
	private int frames = 0; // Frames drawn over the interval
	private float timeleft;
	#endregion

	#region bools
	public bool groundCheck;
	private bool attackStamina=false;
	private bool lockOn = false;
	private bool staminaBool;
	bool next = false;
	private bool rollBool = false;
	#endregion

 	private void Start () {
		animator = GetComponent<Animator>();
		maxHealth = health;
		stamina = 20;
		shieldWeight = 0f;

		GameObject[] enemies2 = GameObject.FindGameObjectsWithTag ("enemy");
		foreach (GameObject enemy in enemies2) {
			enemies.Add(enemy);
		}
	}
	
	private void Update () {
		fps ();
		PlayerFunc ();
		LockOnFunc ();
		ShieldFunc ();
		LineOfSight ();
	}

	private void PlayerFunc(){
		// health-check
		if (health <= 0){
			foreach(GameObject enemy in enemies)
				enemy.GetComponent<enemyChar>().playerTarget = null;
			Debug.Log("the player has died" + health);
			Destroy(gameObject, 2f);
		}

		if (stamina < 0)
			stamina = 0;

		// check if damaged
		if (invTimer > 0.0f) {
			invTimer -= Time.deltaTime;
		}

		// movement
		if (animator.GetBool ("groundCheck")) {
			horizontal = Input.GetAxis ("Horizontal");
			vertical = Input.GetAxis ("Vertical");
		}
		Vector3 movement = Vector3.zero;
		movement.x = horizontal;
		movement.y = vertical;
		movement = Vector3.ClampMagnitude (movement, 1.0f);

		animator.SetFloat ("x", movement.x);
		animator.SetFloat ("y", movement.y);
		Vector3 stick = new Vector3(horizontal,0, vertical);


		speed = Mathf.Clamp (new Vector2(horizontal, vertical).sqrMagnitude,0f,1f);
		animator.SetFloat("Speed",speed);

		//normal movement
		if(speed >0.1 && !lockOn){
			Vector3 camDir = daCam.transform.rotation * stick;
			Vector3 objectDir = transform.position + camDir;
			Quaternion targetRotation = Quaternion.LookRotation(objectDir - transform.position);
			Quaternion constrained = Quaternion.Euler(0.0f, targetRotation.eulerAngles.y, 0.0f);
			transform.rotation = Quaternion.Slerp(transform.rotation, constrained, Time.deltaTime * rotationspeed );
		}

		//Jump
		if(Input.GetButtonDown ("Jump") && !(animator.GetCurrentAnimatorStateInfo(0).IsTag("attack"))){
			if(animator.GetBool("groundCheck") && !animator.GetCurrentAnimatorStateInfo(0).IsTag("dodge")){
				GetComponent<Rigidbody>().AddRelativeForce (Vector3.up * jumppower);
				//GetComponent<AudioSource>().PlayOneShot(jumpsound);
			}	
		}
		
		// Roll
		if (Input.GetButtonDown ("roll") && stamina >= 6.5f) {
			if(animator.GetBool("groundCheck")){
				animator.SetTrigger("roll");
				rollBool = true;

				//stamina -= 6.5f;
			}
		}

		if (rollBool == true && !animator.GetBool ("roll")) {
			rollBool = false;
			stamina -= 6.5f;
		}


		//Attack
		if (Input.GetButtonDown ("Attack") && animator.GetBool("groundCheck") && stamina >= 3) {
			animator.SetTrigger("attack");
			attackStamina=true;

		}
		if (attackStamina == true && !animator.GetBool ("attack")) {
			attackStamina=false;
			stamina -= 3.0f;
		}

	}

	public void LockOnFunc(){
		if (Input.GetButtonDown ("lock on")) {
			if (lockOn){
				targ = null;
				lockOn = !lockOn;
				animator.SetBool("lockOn",!animator.GetBool("lockOn"));
			}
			else
				targ = FindClosestEnemy();
			if(targ!=null){
				target = targ.transform;
				animator.SetBool("lockOn",!animator.GetBool("lockOn"));
				lockOn = !lockOn;
			}
		}
		// Swap to next enemy
		if (lockOn && Input.GetButtonDown ("Next Enemy") && enemies.Count > 1) {
			target = FindNextEnemy (targ).transform;
			targ = target.gameObject;
			
		}

		if(lockOn && LineOfSight())
		{
			var newRotation = Quaternion.LookRotation(target.position - transform.position).eulerAngles;
			newRotation.x = 0;
			newRotation.z = 0;
			transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(newRotation), Time.deltaTime*damping);
		}
	}

	GameObject FindClosestEnemy(){
		GameObject closest = null;
		float distance = Mathf.Infinity;
		Vector3 position = transform.position;
		
		foreach (GameObject enemy in enemies) {
			if(enemy != null){
				Vector3 diff = enemy.transform.position - position;
				float curDistance = diff.sqrMagnitude;
				if (curDistance < distance) {
					closest = enemy;
					distance = curDistance;
				}
			}
		}
		return closest;
	}

	GameObject FindNextEnemy(GameObject currTarget){
		List<GameObject> tempList = new List<GameObject>(enemies);
		tempList.Remove (currTarget);
		
		GameObject next = null;
		float distance = Mathf.Infinity;
		Vector3 position = transform.position;
		
		foreach (GameObject go in tempList) {
			if (go != null) {
				Vector3 diff = go.transform.position - position;
				float curDistance = diff.sqrMagnitude;
				if (curDistance < distance) {
					next = go;
					distance = curDistance;
				}
			}
		}
		return next;
	}


	private void ShieldFunc(){
		float speed = 7f;
		if (Input.GetButtonDown ("Block")) {
			staminaBool = true;
		}
		else if (Input.GetButtonUp ("Block"))
		{
			staminaBool = false;
		}

		if(animator.GetLayerWeight(1)>0.1)
				stamina -= Time.deltaTime;

		else if (animator.GetLayerWeight(1)<0.1 && stamina<20.0f)
			stamina += Time.deltaTime*3;

		if (stamina <= 0.0f)
			staminaBool = false;

		if (staminaBool)
			animator.SetLayerWeight (1, shieldWeight = Mathf.MoveTowards (shieldWeight, 1f, Time.deltaTime * speed));

		else
			animator.SetLayerWeight (1, shieldWeight = Mathf.MoveTowards (shieldWeight, 0f, Time.deltaTime * speed));
	}

	public bool LineOfSight(){
		if (target != null && lockOn) {
			// check if the target is not in line of sight
			if (Physics.Linecast(gameObject.transform.FindChild("camtarget").position, target.Find("Armature_001/Bone/spine/rib/neck/head").position, out hit, layermask)) {
				if (hit.transform.name != target.transform.name){
					removeTarget();
					return false;
				}
			}
		}
		return true;
	}

	private void removeTarget ()
	{
		lockOn = false;
		targ = null;
		animator.SetBool("lockOn", false);
	}

	public void DamagePlayer(int damage){
		if (invTimer < 0.1f && !animator.GetCurrentAnimatorStateInfo(0).IsTag("dodge")) {
			animator.SetTrigger("hit");
			this.health -= damage;
			invTimer = 0.65f;
		}
	}

	private void fps(){
		timeleft -= Time.deltaTime;
		accum += Time.timeScale/Time.deltaTime;
		frames++;

		if( timeleft <= 0.0f )
		{
			timeleft = updateinterval;
			accum = 0.0f;
			frames = 0;
		}
	}

	#region set&getfunks
	public void setGroundCheck(bool groundCheck)
	{
		this.groundCheck = groundCheck;
	}
	public void setTarget(Transform target)
	{
		this.target = target;
	}
	public Animator getAnimator(){
		return this.animator;
	}

	public float getHealth(){
		return this.health;
	}

	public float getShieldWeight(){
		return this.shieldWeight;
	}

	public bool getLockOn(){
		return this.lockOn;
	}

	public void lowerStamina(float stam){
		this.stamina -= stam;
	}
	#endregion

	private void OnGUI(){
		// used for showing fov, fps and healthbar
		GUI.contentColor = Color.yellow;
		GUI.Label(new Rect(10, 10, 100, 20), "fov: " + Camera.main.fieldOfView.ToString("f2"));
		if(frames != 0)
			GUI.Label (new Rect(80, 10, 100, 20), "fps: " + (accum/frames).ToString("f0"));
		GUI.Box(new Rect(10f,50f,Screen.width / 4f /(maxHealth/health), 20f), health + "/" + maxHealth);
		GUI.Box (new Rect (10f, 80f, Screen.width / 4f / (20 / stamina), 20f), stamina.ToString("f0") + "/" + "20");

		if (lockOn && target != null) {
			int hp = target.GetComponent<enemyChar> ().enemyHealth;
			int maxhp = target.GetComponent<enemyChar> ().maxenemyHealth;
			if(hp > 0)
				GUI.Box (new Rect (Screen.width - 250f, 50f, Screen.width / 4f / (maxhp / hp), 20f), hp + "/" + maxhp);

			else if (hp <= 0)
				removeTarget();
		}
	}
}
