using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class MeleePlayerController : Player {
    private Animator anim; // EDIT!!!

    public MeleePlayerController(string newPlayerName, string newPlayerClass, Rigidbody2D newPlayerRB2D, bool newIsMelee, int newPlayerLevel, int newHealth, float newSpeed, int newExperience, Animator newAnim) : base(newPlayerName, newPlayerClass, newPlayerRB2D, newIsMelee, newPlayerLevel, newHealth, newSpeed, newExperience, newAnim)
    {
        isMelee = true;
        //This function is only called when you do: new MeleePlayerController()
        //This will only work if you spawn player via script, use Start() if you spawn via map
        //playerName = "Test";
        //isMelee = true;
        //playerLevel =50;
        //health = 100;
        //speed = 50;

    }

    // Use this for initialization
    void Start ()
    {
        DefaultSettings();

        playerName = "Test";
        isMelee = true;
        playerLevel = 50;
        health = 100;
        speed = 50;

        PlayerRB2D = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
		health = 100;
    }
    

	public override void TakeDamage(int damageTaken){
		health = health - damageTaken;
		if (health <= 0) {
			//you dead
		}
	}
}
