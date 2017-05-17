using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenu1 : MonoBehaviour {

    public Button NewGame;
    public Button ExitGame;

	// Use this for initialization
	void Start () {
        Button start = NewGame.GetComponent<Button>();
        start.onClick.AddListener(LoadNewGame);
        Button exit = ExitGame.GetComponent<Button>();
        exit.onClick.AddListener(QuitGame);
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    void LoadNewGame()
    {
        //Application.LoadLevel("level001");
        this.enabled = false;
        SceneManager.LoadScene("level001", LoadSceneMode.Single);
        //Destroy(this);
    }

    void QuitGame()
    {
        Application.Quit();
    }
}
