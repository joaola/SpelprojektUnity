using UnityEngine;
using System.Collections;

public class playMusic : MonoBehaviour {

	public Animator anim;
	public AudioSource musicSource;
	public AudioClip dungeonMusic;
	public AudioClip graveyardMusic;
	public bool containPlayer;
	public string currentScene;
	public float sceneTimer;

	// Use this for initialization
	void Start()
	{
		sceneTimer = 2.0f;
		currentScene = null;
		containPlayer = false;
		musicSource = GetComponent<AudioSource> ();
		musicSource.PlayOneShot (dungeonMusic);
		musicSource.loop = true;
	}

	void Update()
	{
		if (sceneTimer > 0) {
			sceneTimer -= Time.deltaTime;
		}

		if (Input.GetKeyDown (KeyCode.T) && containPlayer){
			sceneTimer = 0.8f;
		}

		if (containPlayer && Input.GetKey (KeyCode.T)) 
		{
			currentScene = Application.loadedLevelName;
				if(sceneTimer > 0f)
			{
					MusicChanger ();
			}
		}
	}

	void OnTriggerStay(Collider other) {
		
		if(other.gameObject.tag == "Player")
			this.containPlayer = true;
	}
	void OnTriggerExit(Collider other) {
		
		if(other.gameObject.tag == "Player")
			this.containPlayer = false;
	}

	void MusicChanger()
	{
		if(currentScene == "characterTest")
		{
			musicSource.Stop ();
			musicSource.PlayOneShot (graveyardMusic);
			musicSource.loop = true;
			Debug.Log("graveyardMusic playing");
		}
	}
}

