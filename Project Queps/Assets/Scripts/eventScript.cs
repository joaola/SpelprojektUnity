using UnityEngine;
using System.Collections;

public class eventScript : MonoBehaviour {
	private AudioSource audio;
	void Start () {
		audio = GetComponent<AudioSource>();
	}

	public void PlaySoundEffects(AudioClip soundEffect)
	{
		audio.PlayOneShot(soundEffect);

	}
}
