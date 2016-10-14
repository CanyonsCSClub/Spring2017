using UnityEngine;
using System.Collections;

public class Pause_Menu : MonoBehaviour {

	public bool paused = false;
	public GameObject pauseMenu;
	public GameObject HUD;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		//turning the pause on and off
		if (paused) {//on
			Time.timeScale = 0f;
			HUD.SetActive (false);//de-activates the HUD
			pauseMenu.SetActive (true);
		} else {//off
			Time.timeScale = 1f;
			HUD.SetActive (true);//re-activates the HUD
			pauseMenu.SetActive (false);
		}
		//press the escape key to pause/un-pause
		if(Input.GetKeyDown(KeyCode.Escape)){
			paused = !paused;
		}
	}

	public void Resume(){
		paused = false;
	}

	//other options?

	public void OpenSettings(){
		//scene with:
			//volume
			//brightness?
			//???
	}

	public void QuitToMain(){

	}
}

//bulletMover? to stop bullets from being spawned while paused
/*
 * if (Time.timeScale >= 1f){
 * 	//do all the shot spawn stuff
 * }
 */
