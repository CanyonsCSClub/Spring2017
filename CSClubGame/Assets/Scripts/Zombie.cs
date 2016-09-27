using UnityEngine;
using System.Collections;

public class Zombie : Enemy{

    public Transform player;

   // private Rigidbody2D zombieRDB2D;

   	// private float nextAttack;

    // Use this for initialization

	public Zombie(){
		
	}


	/*
    void Start ()
    {
		//for now Im just gonna give zombie some basic stats to get the ball rolling
		//Enemy(string newEnemyName, int newRank, bool newIsMelee, int newLevel, int newHealth, 
									//float newSpeed, int newAttack, float newAttackSpeed, int newArmor)


       // zombieRDB2D = GetComponent<Rigidbody2D>();
    }
	
	// Update is called once per frame
	void FixedUpdate ()
    {
       // GameObject currentTarget = zombie.GetClosestTarget();
       //  zombie.Move(currentTarget);
       //  zombie.Attack(currentTarget);


        //float z = Mathf.Atan2((player.transform.position.y - transform.position.y), (player.transform.position.x - transform.position.x)) * Mathf.Rad2Deg - 90;

        // transform.eulerAngles = new Vector3(0, 0, z);

        // zombieRDB2D.AddForce(gameObject.transform.up * speed);

    }
	*/
}
