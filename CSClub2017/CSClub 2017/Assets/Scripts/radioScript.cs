/* Author: Gerardo Bonnet
 * Date: 5/5/17 Hunter Was here
 */
 using UnityEngine;
using System.Collections;

//Description: when a Cassette object is inserted to the Radio, audio output changes to AudioSource of Cassette.
/*at the moment, this script holds placeholder methods to simulate Cassette objects being inserted, 
and therefore must be adapted to implement storage of Cassette GameObjects
    */

public class radioScript : MonoBehaviour
{

		public GameObject cassette;  // inserted cassettes placed here
		private AudioSource radioOut; //to be assigned audio from cassette objects

		void Start ()
		{
				radioOut = GetComponent<AudioSource> (); // assigns radio output
		}

		void Update ()
		{

				if (Input.GetKeyUp (KeyCode.Space)) {  //turns radio on and off
						radioOut.enabled = !radioOut.enabled;
			Debug.Log ("Radio on/off Toggled: " + radioOut.enabled);
				}
		
				if (Input.GetKeyUp (KeyCode.E)) {   //ejects current cassette
						radioOut.enabled = false;
						radioOut = GetComponent<AudioSource> (); 
						cassette = null;  // needs to place cassette in player inventory (?)
			Debug.Log ("cassette ejected.  Cassette content: " + cassette);
				}
		
				if (Input.GetKeyUp (KeyCode.T)) { //insert a cassette  -- PLACEHOLDER TO SIMULATE GAMEPLAY
						cassette = GameObject.Find ("Cassette"); //needs to remove cassette from player inventory (?)
						cassetteInput ();
						Debug.Log (cassette + " - Bramble Blast inserted");
				}

				if (Input.GetKeyUp (KeyCode.R)) {  //insert other cassette --- PLACEHOLDER TO SIMULATE GAMEPLAY
						cassette = GameObject.Find ("Cassette_R"); //needs to remove cassette from player inventory(?)
						cassetteInput ();
						Debug.Log (cassette + " - Batman Theme inserted");
				}
		}

		void cassetteInput ()  //changes audio output
		{
				radioOut.enabled = false;
				radioOut = cassette.GetComponent<AudioSource> ();
		}

}

//add cassette insertion and removal