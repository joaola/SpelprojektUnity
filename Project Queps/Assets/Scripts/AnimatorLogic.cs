using UnityEngine;
using System.Collections;

public class AnimatorLogic : MonoBehaviour {
	private Animator animator;
	public AudioClip jumpsound;

	public Camera daCam;

	#region Player variables
	public float health;
	public float maxHealth;
	public float rotationspeed;
	public float jumppower = 840;
	public bool groundCheck;
	private float speed;
	private float horizontal = 0.0f;
	private float vertical = 0.0f;
	public GameObject sword;
	private float invTimer = 0.0f;
	private float stamina;
	private bool staminaBool;
	#endregion

	#region targetting
	public Transform target;
	private Transform temptarget;
	private GameObject targ;
	public GameObject targetcheck;
	private float damping = 30.0f;
	bool lockOn = false;
	bool next = false;
	private RaycastHit hit;
	public LayerMask layermask;
	private float shieldWeight;
	private float rolltime = 0;
	#endregion
	
	#region fps
	private float updateinterval = 0.5f;
	private float accum = 0.0f; // FPS accumulated over the interval
	private int frames = 0; // Frames drawn over the interval
	private float timeleft;
	#endregion
	 
 	private void Start () {
		maxHealth = health;
		stamina = 20;
		print("player health is:" + health);
		shieldWeight = 0f;

		animator = GetComponent<Animator>();
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
			Debug.Log("the player has died" + health);
			Destroy(gameObject, 2f);
		}

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
		if(Input.GetButtonDown ("Jump") && sword.GetComponent<sword>().getAttackTimer() <= 0){
			if(animator.GetBool("groundCheck") && rolltime <= 0){
				GetComponent<Rigidbody>().AddRelativeForce (Vector3.up * jumppower);
				GetComponent<AudioSource>().PlayOneShot(jumpsound);
			}	
		}

		if(rolltime > 0)
			rolltime -= Time.deltaTime;

		// Roll
		if (Input.GetButtonDown ("roll")) {
			if(animator.GetBool("groundCheck")){
				animator.SetTrigger("roll");
				rolltime = 0.8f;
			}
		}
		if (Input.GetButtonUp ("roll")) {
			animator.ResetTrigger ("roll");
		}

		//Attack
		//animator.SetBool("attack", false);
		if (Input.GetButtonDown ("Attack") && rolltime <= 0 && animator.GetBool("groundCheck")) {
			//animator.SetBool("attack", true);
			animator.SetTrigger("attack");
		}
	}

	public void LockOnFunc(){
		// Swap to next enemy
		if (lockOn && Input.GetButtonDown ("Next Enemy")) {
			Transform temp = targetcheck.GetComponent<EnemyCheck>().FindNextEnemy(targ).transform;
			GameObject tempg = targetcheck.GetComponent<EnemyCheck> ().FindNextEnemy (targ);

			if(temp != null){
				target = temp;
				targ = tempg;
			}
		}

		// lock-on
		if (Input.GetButtonDown("lock on")) {
			if(!lockOn){
				// get current target
				temptarget = targetcheck.GetComponent<EnemyCheck> ().getTarget ();
				targ = targetcheck.GetComponent<EnemyCheck> ().getObject ();
				if(temptarget!=null){
					target = temptarget;
					lockOn = true;
					animator.SetBool("lockOn", true);
					//Debug.Log(target.name);
				}
				else{
					Debug.Log ("No enemies in range");
					removeTarget();
				}
			}
			else{
				removeTarget();
			}
		}
		// target out-of-range
		if (lockOn == true && targetcheck.GetComponent<EnemyCheck> ().getTriggers ().Contains (targ) == false ) {
			removeTarget();
		}
		// target in-range, lock on target
		else if (lockOn == true && targetcheck.GetComponent<EnemyCheck>().getTriggers().Contains(targ) != false){
			var newRotation = Quaternion.LookRotation(target.position - transform.position).eulerAngles;
			newRotation.x = 0;
			newRotation.z = 0;
			transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(newRotation), Time.deltaTime*damping);
			//player.transform.LookAt (new Vector3(target.position.x, player.transform.position.y, target.position.z));
		}
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
			stamina += Time.deltaTime;

		if (stamina <= 0.0f)
			staminaBool = false;

		if (staminaBool)
			animator.SetLayerWeight (1, shieldWeight = Mathf.MoveTowards (shieldWeight, 1f, Time.deltaTime * speed));

		else
			animator.SetLayerWeight (1, shieldWeight = Mathf.MoveTowards (shieldWeight, 0f, Time.deltaTime * speed));
	}

	public void LineOfSight(){
		if (lockOn) {
			// check if the target is not in line of sight
			if (Physics.Linecast(transform.position, target.position, out hit, layermask)) {
				if (hit.transform.name != target.transform.name){
					removeTarget();
				}
			}
		}
	}

	private void removeTarget ()
	{
		lockOn = false;
		target = null;
		animator.SetBool("lockOn", false);
	}

	public void DamagePlayer(int damage){
		if (invTimer < 0.1f && rolltime < 0.1f) {
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
		GUI.Box(new Rect(10f,50f,Screen.width / 4f /(maxHealth/health), 20f),"health");
		GUI.Box (new Rect (10f, 80f, Screen.width / 4f / (20 / stamina), 20f), "stamina");
	}

}
