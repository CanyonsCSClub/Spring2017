using UnityEngine;
using System.Collections;

public class guiTextSet : MonoBehaviour {

	// Use this for initialization
	void Start () {
	 GetComponent<GUIText>().text= 	"\nArrow Keys: Move\nLeft Shift: Sneak\nZ: Use Switch\nX: Attract Attention";
	
	}

}
