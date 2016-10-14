using UnityEngine;
using System.Collections;

public class cardboard_box_demo : Enemy {


	cardboard_box_demo(){
		
	}

	// Use this for initialization
	void Start () {
		ConfigEnemy ("Box", 1, true, 5, 3,1, 10, 5f, 5, 5, 5);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
