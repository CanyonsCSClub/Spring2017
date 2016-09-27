using UnityEngine;
using System.Collections;

public class Enemy {

	public GameObject bloodSplatter;
	public GameObject deadZombie;
	public GameObject expOrb;
	public GameObject damageTakenNumber;


	//Enemy constructor variables_____________________________________________________________
	protected string enemyName { get; set; }
	protected GameObject enemySpecies { get; set; }//zombie....etc
	//not sure how to work this in at the moment, maybe we dont need it, but just in case
	protected int rank { get; set; } //0 - 10. 0 being a common, 10 being a boss-ish enemy
	protected bool isMelee { get; set; }
	public float attackRange { get; set; } //0 - 5 for melee , 5 to infinity for ranged
	protected int level { get; set; }
	public int health { get; set; }
	public int MAXHEALTH; //used for a flinch mechanic...and whatever else tbh
	public float speed { get; set; }
	public int expGiven { get; set; }
	/*this will "hardcoded" in each constructor to be proportional
	 * to the enemy's level and "type" Ex; common vs boss type
	 */
	public int attack { get; set; }
	public float attackSpeed { get; set; }
	protected int armor { get; set; } //armor should always be greater than 1 for codes sake
	//_________________________________________________________________________________________

	//Defaults for stat modifing skills_____
	protected float defaultSpeed;
	protected int defaultAttack;
	protected int defaultArmor;
	protected float defaultAttackSpeed;

	//flinching "being stunned" ____________
	public bool flinchOn = true;
	public bool isStunned = false;
	public float stunTime = 1f;
	public float timeStunned = 0;
	//______________________________________

	//frenzy ability _______________________
	public float lastTouchTime = 0;
	//______________________________________

	//bum Rush ability______________________
	bool isBumRushing = false;

	public Enemy(){
		// force a default character to be spawned
	}

	public Enemy(string newEnemyName, int newRank, bool newIsMelee, float newAttackRange, int newLevel, int newHealth, float newSpeed, int newAttack, float newAttackSpeed, int newArmor){
		this.enemyName = newEnemyName;
		this.rank = newRank;
		this.isMelee = newIsMelee;
		this.attackRange = newAttackRange;
		this.level = newLevel;
		this.health = newHealth;
		this.MAXHEALTH = newHealth;
		this.speed = newSpeed;
		this.attack = newAttack;
		this.attackSpeed = newAttackSpeed;
		this.armor = newArmor;
		//this.expGiven = ((level * type) * (attack * armor)) / health;
		//idk maybe something like this formula... but for now
		this.expGiven = level;
		Debug.Log (enemyName + " " + rank + " " + level + " " + health
			+ " " + attack + " " + attackSpeed + " " + armor + " " + expGiven + "\n" + "has been created" );
		this.defaultSpeed = speed;
		this.defaultAttack = attack;
		this.defaultAttackSpeed = attackSpeed;
		this.defaultArmor = armor;
	}


	/// <summary>
	/// Enemies in the zombies eyes are GO's tagged "Player"
	/// will only ever be 4 players, because there is only 4 physical players
	/// makes an array with all active players on map
	/// looks for closest one
	/// </summary>
	/// <returns>returns the closest player object</returns>

	/*
	public GameObject GetClosestTarget()
	{ 
		GameObject[] enemies;
		enemies = GameObject.FindGameObjectsWithTag ("Player");
		GameObject closest = null;
		float maxDistToTarget = 500;
		Vector3 position = transform.position;
		foreach (GameObject enemy in enemies) {
			Vector3 dist = enemy.transform.position - position;
			float currentDistance = dist.sqrMagnitude;
			if (currentDistance < maxDistToTarget && enemy.activeInHierarchy) {
				closest = enemy;
				maxDistToTarget = currentDistance;
			}
		}
		return closest;
	}
		
	public void Move(GameObject currentTarget)
	{		//( false || __________) first term will always false unless No-flinch modifier is on
		if ( true ){
			if (LastTargetTouched (lastTouchTime) > 10f) {//10 seconds
				Frenzy (true);
			} else {
				Frenzy (false);
			}
			// Rotation to Target
			//Since there is going to be up to 4 players, i think there should be no reference to a player
			//but instead a reference to a target which can be any player.
			float z = Mathf.Atan2 ((currentTarget.transform.position.y - transform.position.y),
				         (currentTarget.transform.position.x - transform.position.x)) * Mathf.Rad2Deg - 90;
			transform.eulerAngles = new Vector3 (0, 0, z);
			// Moving to Target
			transform.position = Vector3.MoveTowards (transform.position, 
				currentTarget.transform.position, speed * Time.deltaTime);
		}
	}

*/
	public bool CanIMove(){
		return  !flinchOn || (Time.time - timeStunned >= stunTime);
	}

/*
	public void Attack(GameObject currentTarget)
	{
		float nextAttack = 0;
		if (Time.time > nextAttack  && CanIAttack(currentTarget))
		{
			Vector3 bloodPosDelta = new Vector3(0, 0, 0.5f);
			Instantiate(bloodSplatter, currentTarget.transform.position + bloodPosDelta, 
				currentTarget.transform.rotation);
			currentTarget.GetComponent<PlayerController>().TakeDamage(attack);
			nextAttack = Time.time + attackSpeed;
			lastTouchTime = Time.time;
			Frenzy (false); 
			lastTouchTime = 0f;
		}
	}

	public bool CanIAttack(GameObject currentTarget){
		float distanceToTarget = (transform.position - currentTarget.transform.position).sqrMagnitude;
		if (distanceToTarget < attackRange && currentTarget.tag == "Player") {
			return true;
		}
		return false;
	}

	*/

	public void RecieveHealing(int healing){
		if (health > 0) {
			health = health + healing;
		}
	}
	/*
	public void TakeDamage(int damageTaken){
		health = health - damageTaken;
		//string damageString = damageTaken.ToString ();
		//make a number of somesort pop above the enemy's head
		//Instantiate (damageTakenNumber, transform.position + new Vector3(0,0,1), transform.rotation);
		if (health <= 0)
//			OnDeath();
		if ((double)damageTaken > (double)(MAXHEALTH * .1) && flinchOn == true) {
			Flinch ();
		}
	}
	*/

	/*Skills and Modifiers_________________________________________________________________
	*Modifiers Include: Flinch
	*
	*Skills Include: BumRush, Frenzy
	*/

	public void Flinch(){
		isStunned = true;
		timeStunned = Time.time;
	}

	/// <summary>
	/// 1.5 * speed runs in straight line until a)hits a wall b)hits a player c)dies
	/// </summary>
	public void BumRush(GameObject currentTarget){
		//will i need to somehow stop the normal move method so the path stays straight?
		isBumRushing = true;
	
	}

	/// <summary>
	/// The enemy who has this trait gets increased speed and next hit damage until it hits 
	/// its next target. After which all stats go back to default.
	/// </summary>
	public void Frenzy(bool isFrenzied){
		if (isFrenzied) {
			//statChanges
			//Maybe the enemy GO does some sort of scream, so the players know
					//or glows red or something?
			speed = speed * 1.5f;
			attack = attack * 2;

		} else {
			//stat roll back
			speed = defaultSpeed;
			attack = defaultAttack;
		}


	}
	//End Skills and Modifiers_______________________________________________________


	/*Utilities______________________________________________________________________
	 *Utilities Include: OnDeath, LastTargetTouched
	 * 
	 * 
	 */

	/*
	void OnDeath()
	{
		for (int i = 0; i < expGiven; i++) {
			Instantiate (expOrb, transform.position, transform.rotation);
		}
		//add a small explosive force, with no damage, to "explode" expOrbs from an enemy
		//kinda like minecraft? 
		//we can also do, for performance sake, Instantiate one exp orb with an exp value 
				//and just drop at death location?
		Instantiate(deadZombie, transform.position, transform.rotation);
		Destroy(gameObject);
	}
	*/

	/// <summary>
	/// Used in conjunction with activating the frenzy skill, returns the time of the last time 
	/// a target was hit by an enemy. If it is greater than some"time" the target will Frenzy(true);
	/// </summary>
	/// <returns>Time of last touch...specifically time of last damage.</returns>
	public float LastTargetTouched(float lastTouched){
		return Time.time - lastTouched;
	}



	//End Utilities_____________________________________________________________________

}