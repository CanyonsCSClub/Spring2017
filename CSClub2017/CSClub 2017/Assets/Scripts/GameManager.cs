using UnityEngine;
using System.Collections;
using UnityEngine.UI; //Need this for calling UI scripts
using UnityEngine.SceneManagement; // Need this to restart scenes

public class GameManager : MonoBehaviour {

	[SerializeField]
	Transform UIPanel;

	bool isPaused;

	void Start ()
	{
		UIPanel.gameObject.SetActive(false); //  sure our pause menu is disabled when scene starts
		isPaused = false; // make sure isPaused is false when our scene opens
	}

	void Update ()
	{

		// If player presses escape and game is not paused. Pause game. If game is paused and player presses escape, unpause.
		if(Input.GetKeyDown(KeyCode.P) && !isPaused)
			pause();
		else if(Input.GetKeyDown(KeyCode.P) && isPaused)
			unpause();
	}

	public void pause()
	{
		isPaused = true;
		UIPanel.gameObject.SetActive(true); // turn on the pause menu
		Time.timeScale = 0f; // pause the game
		//AudioListener.pause = true; // Pause in game sounds
		Cursor.lockState = CursorLockMode.None;
		Cursor.visible = true;
	}

	public void unpause()
	{
		isPaused = false;
		UIPanel.gameObject.SetActive(false); //turn off pause menu
		Time.timeScale = 1f; //resume game
		AudioListener.pause = false;
		Cursor.lockState = CursorLockMode.Locked;
		Cursor.visible = false;
	}

	public void quitGame()
	{
		Application.Quit();
	}

	public void restart()
	{
		SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);

	}
}