using UnityEngine;
using System.Collections;

public class radioScript : MonoBehaviour {

	public GameObject cassette;

	private AudioSource radioOut; //to be assigned audio from cassette objects

	void Start () 
	{
		radioOut = GetComponent<AudioSource> (); // assigns radio output
		//radioOut = cass.GetComponent<AudioSource>();
	}

	void Update () 
	{
/*		if (Input.GetKeyUp (KeyCode.E)) {
			radioOut.enabled = false; 
			casette = null;
				}
		*/
		if (Input.GetKeyUp (KeyCode.Space)) {
						radioOut.enabled = !radioOut.enabled;
				}

/*		if (Input.GetKeyUp (KeyCode.Space)) { //reads UI of Space key// turns radio output On and Off
		
						cassette.GetComponent<AudioSource> ().enabled = !cassette.GetComponent<AudioSource>().enabled;
				}
*/		
		
		if (Input.GetKeyUp (KeyCode.T)) {
			cassette = GameObject.Find ("Casette");
			radioOut = cassette.GetComponent<AudioSource>();
			Debug.Log ("casette");
		}

		if (Input.GetKeyUp (KeyCode.R)) {
			cassette = GameObject.Find ("Casette_R");
			radioOut = cassette.GetComponent<AudioSource>();
			Debug.Log ("casette_R");
				}
	}
}

//set to shift cassette audio to radioOut when cassette inserted
//add cassette insertion and removal
//default to original static without casette