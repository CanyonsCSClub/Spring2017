using UnityEngine;
using System.Collections;

public class Cardboard_Box : Enemy {

	private Rigidbody2D boxRB;
	public Cardboard_Box(){
	
	}


	void Start(){
		boxRB = GetComponent<Rigidbody2D> ();
		boxRB.constraints = RigidbodyConstraints2D.FreezePosition;
		ConfigEnemy ("Carboard Box", 0, true, 3, 3, 10, 100, 10, 10, 5, 1);
	
	}

	public override void Move() {//just sit and wait
		boxRB.constraints = RigidbodyConstraints2D.FreezeAll;
	}


	public override void Move(GameObject GO){
		if (GO = getAggro ()) {//the game object "in range" is the aggroOnPlayer
			//will only move if the cardboard box is aggro/d on somebody/something
			boxRB.constraints = RigidbodyConstraints2D.None;
			float z = Mathf.Atan2 ((GO.transform.position.y - transform.position.y),
				(GO.transform.position.x - transform.position.x)) * Mathf.Rad2Deg - 90;
			transform.eulerAngles = new Vector3 (0, 0, z);
			// Moving to Target
			transform.position = Vector3.MoveTowards (transform.position, 
				GO.transform.position, moveArgSpeedModifier * speed * Time.deltaTime);
		} else {
			Move ();
		}


	}

	public override void Attack(GameObject GO){//attack no matter what
		float distanceToTarget =  Vector3.Distance (this.transform.position, GO.transform.position);
		if (GO.CompareTag ("Enemy")) {
			if(distanceToTarget < attackRange){
				GO.GetComponent<Enemy> ().TakeDamage (attack, this.gameObject);
				//GO.GetComponent<Rigidbody2D> ().AddForce (); add a "Boop" in the direction the box is going
				//strong enought to knock it out of range
			}
		} else if(GO.CompareTag("Player")){
			if(distanceToTarget < attackRange){
				GO.GetComponent<PlayerController>().TakeDamage(attack);
				//GO.GetComponent<Rigidbody2D> ().AddForce (); add a "Boop" in the direction the box is going
				//strong enough to knock it out of range
			}
		} else {
			//yikes
		}
	}













	//set a time scale so that these can only flip every idk....10 seconds?
	private void Enlarge(){//if 0<hp<.5MAXHEALTH
		//get bigger by 2 and multiply armor by 2
		//movement by half
		this.transform.localScale = new Vector3(transform.localScale.x * 2,transform.localScale.y * 2,1);
		this.armor = this.armor * 2;
		this.speed = this.speed / 2;
	}
	private void Shrink(){//if for some reason he is healed to above .5MAXHEALTH
		this.transform.localScale = new Vector3(transform.localScale.x / 2,transform.localScale.y / 2,1);
		this.armor = this.armor / 2;
		this.speed = this.speed * 2;
	}
}




/*Notes______________________
 * 
 * 
 * 
 * 
 */