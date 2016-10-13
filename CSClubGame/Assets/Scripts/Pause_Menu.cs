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
		if (paused) {
			Time.timeScale = 0f;
			HUD.SetActive (false);
			pauseMenu.SetActive (true);

		} else {
			Time.timeScale = 1f;
			HUD.SetActive (true);
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

	public void OpenSettings(){

	}

	public void QuitToMain(){

	}
	//other button methods in the future
}
