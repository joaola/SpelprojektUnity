//C# Main Menu Script

using UnityEngine;
using System.Collections;

public class MainMenu : MonoBehaviour {

	
	public void QuitGame () {
		Debug.Log("Game is exiting");
		Application.Quit();
	}
	
	// Update is called once per frame
	public void StartGame (string level) {
		Application.LoadLevel(level);
		//level sätts till rätt scene i inspector-view för den aktuella knappen
	}

	
}