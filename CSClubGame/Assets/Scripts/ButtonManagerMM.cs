using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class ButtonManagerMM : MonoBehaviour {

	public void startDemo_Button(string level){
		SceneManager.LoadScene (level);

	}

	public void exitDemo_Button (){
		Application.Quit ();
	}
}
