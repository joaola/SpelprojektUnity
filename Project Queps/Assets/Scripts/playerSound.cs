using UnityEngine;
using System.Collections;

public class playerSound : MonoBehaviour 

{
	public AudioSource runningSource;
	public AudioSource jumpingSource;

	public Animator anim;
	public GameObject sword;
	public float rollTimer; //Roll animation time

	// Use this for initialization
	void Start () 
	{
		rollTimer = 0.8f;
		anim = GetComponent<Animator> ();
	}
	
	// Update is called once per frame
	void Update () 
	{
		ResetTimers ();
		//Roll time
		if (rollTimer > 0) {
			rollTimer -= Time.deltaTime;
		}

		JumpingSounds ();
		RunningSounds ();
	}

	/*Functions*/
	void ResetTimers()
	{
		//Reset rolltime
		if (Input.GetButtonDown ("roll")) {
			if (anim.GetBool ("groundCheck")) {
				rollTimer = 0.8f;
			}
		}
	}

	void JumpingSounds(){
		//Jumping sound effect
		if (Input.GetButtonDown ("Jump") && sword.GetComponent<Animator> ().GetCurrentAnimatorStateInfo(0).IsTag("attack")) 
		{
			if (anim.GetBool ("groundCheck") && rollTimer <= 0) {
				Debug.Log ("Jumping sound");
				jumpingSource.Play ();
			}
		}

	}

	void RunningSounds(){
		//Stop running sound effect
		if (Input.GetButtonDown ("Jump") || !anim.GetBool ("groundCheck") || rollTimer >= 0 || anim.GetCurrentAnimatorStateInfo(0).IsName("attack.standingAttack") || anim.GetCurrentAnimatorStateInfo(0).IsName("attack.standingAttack2") || anim.GetCurrentAnimatorStateInfo(0).IsName("attack.standingAttack3")) 
		{
			runningSource.Stop ();
		}
		
		//Running sound effect
		if (runningSource.isPlaying != true && (Input.GetKey (KeyCode.W) || Input.GetKey (KeyCode.A) || Input.GetKey (KeyCode.S) || Input.GetKey (KeyCode.D)) && anim.GetBool ("groundCheck") && rollTimer <= 0) 
		{
			Debug.Log ("Running sound");
			runningSource.Play ();
		}
	}
}