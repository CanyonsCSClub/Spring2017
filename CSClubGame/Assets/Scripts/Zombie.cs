using UnityEngine;
using System.Collections;

public class Zombie : Enemy{



	public Zombie(){
	
	}

	void Start(){
		ConfigEnemy ("Zombie", 1, true, 5, 10, 100, 5, 5, 5, 5); 
		//enemySightRangeCollider = GetComponent<CircleCollider2D> ();
		//enemySightRangeCollider.radius = newRadius;

	}


}
