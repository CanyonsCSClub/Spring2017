using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

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
		if (Input.GetKeyDown (KeyCode.R) && paused) {
			Restart ();
		}
		if (Input.GetKeyDown (KeyCode.M) && paused) {
			SceneManager.LoadScene ("MainMenu");
		}
	}

	public void Resume(){
		paused = false;
	}

	public void quitToMain(){
		SceneManager.LoadScene ("MainMenu");
	}

	public void QTM(Scene s){
		SceneManager.LoadScene (s.name);
	}
	//other options?

	public void OpenSettings(){
		//scene with:
			//volume
			//brightness?
			//???
	}

	public void Restart(){
		Application.LoadLevel(Application.loadedLevel);
	}
}

//bulletMover? to stop bullets from being spawned while paused
/*
 * if (Time.timeScale >= 1f){
 * 	//do all the shot spawn stuff
 * }
 */
