using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class MeleePlayerController : Player {
    Animator anim; // EDIT!!!

    public MeleePlayerController(string newPlayerName, GameObject newPlayerClass, Rigidbody2D newPlayerRB2D, bool newIsMelee, int newPlayerLevel, int newHealth, float newSpeed, int newExperience) : base(newPlayerName, newPlayerClass, newPlayerRB2D, newIsMelee, newPlayerLevel, newHealth, newSpeed, newExperience)
    {
        playerName = "Test";
        //PlayerRB2D = GetComponent<Rigidbody2D>();
        isMelee = true;
        playerLevel =50;
        health = 100;
        speed = 50;
        

    }

    // Use this for initialization
    void Start ()
    {
        anim = GetComponent<Animator>();
		health = 100;
    }
    

	public void TakeDamage(int damageTaken){
		health = health - damageTaken;
		if (health <= 0) {
			//you dead
		}
	}
}
