using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour{

	public GameObject bloodSplatter;
	public GameObject deadZombie;
	public GameObject expOrb;
	public GameObject damageTakenNumber;
	public CircleCollider2D enemySightRangeCollider;

	#region Variables
	//Enemy constructor variables_____________________________________________________________
	protected string enemyName { get; set; }
	protected int rank { get; set; } //0 - 10. 0 being a common, 10 being a boss-ish enemy
	protected bool isMelee { get; set; } //true for melee character
	public float attackRange { get; set; } //0 - 5 for melee , 5 to infinity for ranged?
	public float sightLine { get; set; } //used with the enemysightline class & 
	protected int level { get; set; }
	public int health { get; set; }
	private int MAXHEALTH;//to make sure enemies do not heal more health than they have
	public float speed { get; set; }
	public int expGiven { get; set; }
	/*this will "hardcoded" in each constructor to be proportional
	 * to the enemy's level and "type" Ex; common vs boss type
	 */
	public int attack { get; set; }
	public float attackSpeed { get; set; }//a value of 1 is a second
	protected int armor { get; set; } //armor should always be greater than 1 for codes sake
	//_________________________________________________________________________________________

	//Defaults for stat modifing skills_____
	protected float defaultSpeed;
	protected int defaultAttack;
	protected int defaultArmor;
	protected float defaultAttackSpeed;

	/*flinching "being stunned" ____________
	public bool flinchOn = true;
	public bool isStunned = false;
	public float stunTime = 1f;
	public float timeStunned = 0;
	*/

	GameObject playerInRange = null;
	GameObject aggroOnPlayer = null;
	public float moveArgSpeedModifier;//having a wierd result in movement speeds...in certain situations
	//this was my solution
	#endregion

	public Enemy(){
		// force a default character to be spawned
	}

	public Enemy(string newEnemyName, int newRank, bool newIsMelee, float newAttackRange, float newSightLine, int newLevel, int newHealth, float newSpeed, int newAttack, float newAttackSpeed, int newArmor){
		ConfigEnemy (newEnemyName, newRank, newIsMelee, newAttackRange, newSightLine,
			newLevel, newHealth, newSpeed, newAttack, newAttackSpeed, newArmor); 
		//level, rank, armor >0
	}

	public void ConfigEnemy(string newEnemyName, int newRank, bool newIsMelee, float newAttackRange, float newSightLine, int newLevel,
		int newHealth, float newSpeed, int newAttack, float newAttackSpeed, int newArmor ){
		this.enemyName = newEnemyName;
		this.rank = newRank;
		this.isMelee = newIsMelee;
		this.attackRange = newAttackRange;
		this.sightLine = newSightLine;
		GetComponentInChildren<EnemySightLine> ().setColliderRadius (newSightLine);
		this.level = newLevel;
		this.health = newHealth;
		//Debug.Log ("Constructor Health: " + health);
		this.MAXHEALTH = newHealth;
		this.speed = newSpeed;
		this.attack = newAttack;
		this.attackSpeed = newAttackSpeed;
		this.armor = newArmor;
		//this.expGiven = ((level * type) * (attack * armor)) / health;
		//idk maybe something like this formula... but for now 
		this.expGiven = level;

		this.defaultSpeed = speed;
		this.defaultAttack = attack;
		this.defaultAttackSpeed = attackSpeed;
		this.defaultArmor = armor;

	}

	//Mutators & Accessors____________________________________________________________
	#region Mutators
	public void setPlayerInRange(GameObject newPlayerInRange){
		if (getPlayerInRange () == null) {
			playerInRange = newPlayerInRange;
		} else {
			playerInRange = null;
		}
	}

	public GameObject getPlayerInRange(){
		return playerInRange;
	}


	public virtual void setAggro(GameObject newAggro){
		this.aggroOnPlayer = newAggro;
	}

	public void updateAggro(){
		if(!getAggro().activeInHierarchy){
			setAggro (null);
		}
	}

	public GameObject getAggro(){
		return aggroOnPlayer;
	}
	#endregion
	//End Mutators & Accessors_________________________________________________________



	//Movement________________________________________________________
	#region Movement

	/// <summary>
	/// This is the move handler for the other two move methods
	/// called from fixed update.
	/// </summary>
	/// <param name="PIR">Player In Range GameObject.</param>
	/// <param name="AGGRO">AggroOnPlayer GameObject.</param>
	private void Move(GameObject PIR,GameObject AGGRO){
		//Debug.Log ("PIR: " + PIR + "    AGGRO: " + AGGRO);
		float distanceToAGGRO = -1;
		if (AGGRO != null) {
			distanceToAGGRO = Vector3.Distance (this.transform.position, AGGRO.transform.position);
		}
		//multiplied *3 because for some reason dist is by a factor of 3 smaller than the radius of 
		//the circle collider in enemySightLine
		if ((distanceToAGGRO < 3 * sightLine) & (distanceToAGGRO > 0)) {//aggro is in range
			Move (AGGRO);
		} else if (PIR != null) {//aggro out of range and there is a player in range
			Move (PIR);
		} else { 
			Move ();
		}
	}


	public virtual void Move(GameObject currentTarget){	//how you move to a target
		//Debug.Log ("CurrentTarget" + currentTarget);
		if (currentTarget != null) {
			float z = Mathf.Atan2 ((currentTarget.transform.position.y - transform.position.y),
				          (currentTarget.transform.position.x - transform.position.x)) * Mathf.Rad2Deg - 90;
			transform.eulerAngles = new Vector3 (0, 0, z);
			// Moving to Target
			transform.position = Vector3.MoveTowards (transform.position, 
				currentTarget.transform.position, moveArgSpeedModifier * speed * Time.deltaTime);
		} else {
			Move ();
		}
		
	}

	public virtual void Move(){// " Ai " movement
		var rotationVector = transform.rotation.eulerAngles;
		rotationVector.z += Random.Range (-5f, 5f);
		transform.rotation = Quaternion.Euler (rotationVector);
		transform.position += transform.up * Time.deltaTime * speed;
	}

	#endregion
	//End Movement_____________________________________________________________

	/// <summary>
	/// Attack the specified currentTarget.
	/// Will need to be overridden in the individual enemy child
	/// </summary>
	/// <param name="currentTarget">Current target GameObject.</param>
	public virtual void Attack(GameObject currentTarget)
	{
			//currentTarget.GetComponent<PlayerController>().TakeDamage(attack);	
	}
		
	public virtual void RecieveHealing(int healing){
		if (health > 0) {
			health = health + healing;
		}
	}

	int timesDamaged;
	public virtual void TakeDamage(int damageTaken, GameObject GO){
		
		if (GO.CompareTag ("Player")) {
			setAggro (GO);
			//Debug.Log ("aggro: " + aggroOnPlayer);
		}
		health = health - damageTaken;
		timesDamaged++;
		//Debug.Log ("Enemy Health" + health);
		//damage Taken numbers ..... (if we want em)
		//Instantiate (damageTakenNumber, transform.position + new Vector3(0,0,1), transform.rotation);
		if(timesDamaged%4 == 0 || damageTaken > MAXHEALTH/4){//limits the amount of blood on the screen at once
			Instantiate(bloodSplatter,transform.position + new Vector3(0,0, .5f),transform.rotation);
		//maybe you want sparks instead of blood for drones....eyes on you Prescott
		}
		if (health <= 0)
			OnDeath();
	}



	//Utilities______________________________________________________________________
	#region Utilities

	public bool CanIAttack(GameObject currentTarget){
		float distanceToTarget = (transform.position - currentTarget.transform.position).sqrMagnitude;
		if (distanceToTarget < attackRange && currentTarget.tag == "Player") {
			return true;
		}
		return false;
	}

	void OnDeath()
	{

		//Instantiae expOrb
		Instantiate(deadZombie, transform.position, transform.rotation);
		Destroy(this.gameObject); //until pooling
	}
	#endregion 
	//End Utilities_____________________________________________________________________


	//Event Handelers__________________________________________________________
	#region Events
	void Start(){

	}

	void Update (){
	
	}

	void FixedUpdate(){
		//Debug.Log ("FixedUpdate" + getPlayerInRange () + "_" + getAggro() + "_");
		Move (getPlayerInRange(), getAggro());
	}
	#endregion
	//End Event & Update Handelers____________________________________________________

}

//NOTES__________________________________________________________________
#region Notes
/*
 *	From the move ai, original code
    Vector3 RandomPos = new Vector3 (transform.position.x + Random.Range (-0.1f, 0.1f),
			transform.position.y + Random.Range (-0.1f, 0.1f), transform.position.z);
	float z = Mathf.Atan2 ((RandomPos.y - transform.position.y), 
			(RandomPos.x - transform.position.x)) * Mathf.Rad2Deg - 90;
	transform.eulerAngles = new Vector3 (0, 0, z);
	transform.position = Vector3.MoveTowards (transform.position, RandomPos, speed * Time.deltaTime);

 * The original onTriggerStay2D since been rendered useless...maybe?
 	void OnTriggerStay2D(Collider2D other){

		if(other.CompareTag("Player")){
			Debug.Log ("aggro:" + aggroOnPlayer + "PIR: " + playerInRange);
			if(aggroOnPlayer == playerInRange){//if they are the same
				if (playerInRange != null) {
					Move (playerInRange);
				}
			} else { //if they aren't the same \/
				
				if (other.gameObject == playerInRange && aggroOnPlayer == null) {//if the other in range is the same target as the
					//playerInRange and the enemy is not aggro'd
					Move (other.gameObject);
				} else if(other.gameObject != playerInRange && aggroOnPlayer == null){
					//if the player in the collider is not the playerInRange 
					Move(playerInRange);
					//ignore them
				} else if (aggroOnPlayer != null) { //if the enemy is aggro'd on somebody
					if ((aggroOnPlayer.transform.position - this.transform.position).sqrMagnitude < enemySightRangeCollider.radius * enemySightRangeCollider.radius) {
						//if the aggro'd target is in range move to them
						Move (aggroOnPlayer);
					}
				} else {
					Move ();
				}
			}

		} else {
			
			//if the tag is not a player do.....
		}
	}
 * Add Lerping to Move?
 * 
 * 
 * 
 * 
 * 
 * 
 * 
 * 
 */
#endregion
//End NOTES______________________________________________________________