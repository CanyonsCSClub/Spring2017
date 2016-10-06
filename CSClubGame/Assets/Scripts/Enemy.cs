using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour{

	public GameObject bloodSplatter;
	public GameObject deadZombie;
	public GameObject expOrb;
	public GameObject damageTakenNumber;
	public CircleCollider2D enemySightRangeCollider;


	//Enemy constructor variables_____________________________________________________________
	protected string enemyName { get; set; }
	protected GameObject enemySpecies { get; set; }//zombie....etc
	//not sure how to work this in at the moment, maybe we dont need it, but just in case
	protected int rank { get; set; } //0 - 10. 0 being a common, 10 being a boss-ish enemy
	protected bool isMelee { get; set; }
	public float attackRange { get; set; } //0 - 5 for melee , 5 to infinity for ranged
	public float sightLine { get; set; } 
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

	GameObject playerInRange = null;
	GameObject aggroOnPlayer = null;
	public float newRadius;
	public float moveArgSpeedModifier;

	public Enemy(){
		// force a default character to be spawned
	}

	public Enemy(string newEnemyName, int newRank, bool newIsMelee, float newAttackRange, float newSightLine, int newLevel, int newHealth, float newSpeed, int newAttack, float newAttackSpeed, int newArmor){
		ConfigEnemy (newEnemyName, newRank, newIsMelee, newAttackRange, newSightLine,
			newLevel, newHealth, newSpeed, newAttack, newAttackSpeed, newArmor); 
	}
		
	//Movement____________________________________________________________

	//Updated Movement
	private void Move(GameObject PIR,GameObject AGGRO){
		//Debug.Log ("PIR: " + PIR + "    AGGRO: " + AGGRO);
		float distanceToAGGRO = -1;
		if (AGGRO != null) {
			distanceToAGGRO = Vector3.Distance (this.transform.position, AGGRO.transform.position);
		}

		if ((distanceToAGGRO < 3 * sightLine) & (distanceToAGGRO > 0)) {
			Move (AGGRO);
		} else {
			Move (PIR);
		}



	}


	public void Move(GameObject currentTarget){	//how you move to a target
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

	public void Move(){// " Ai " movement

		//Vector3 RandomPos = new Vector3 (transform.position.x + Random.Range (-0.1f, 0.1f),
		//	transform.position.y + Random.Range (-0.1f, 0.1f), transform.position.z);
		//float z = Mathf.Atan2 ((RandomPos.y - transform.position.y), 
		//	(RandomPos.x - transform.position.x)) * Mathf.Rad2Deg - 90;
		//transform.eulerAngles = new Vector3 (0, 0, z);
		//transform.position = Vector3.MoveTowards (transform.position, RandomPos, speed * Time.deltaTime);

		var rotationVector = transform.rotation.eulerAngles;
		rotationVector.z += Random.Range (-5f, 5f);
		transform.rotation = Quaternion.Euler (rotationVector);
		transform.position += transform.up * Time.deltaTime * speed;

	}


	//End Movement_____________________________________________________________


	public void Attack(GameObject currentTarget)
	{
		
			//currentTarget.GetComponent<PlayerController>().TakeDamage(attack);
		
	}

	public bool CanIAttack(GameObject currentTarget){
		float distanceToTarget = (transform.position - currentTarget.transform.position).sqrMagnitude;
		if (distanceToTarget < attackRange && currentTarget.tag == "Player") {
			return true;
		}
		return false;
	}
		
	public void RecieveHealing(int healing){
		if (health > 0) {
			health = health + healing;
		}
	}

	public void TakeDamage(int damageTaken, GameObject GO){

		if (GO.CompareTag ("Player")) {
			setAggro (GO);
			Debug.Log ("aggro: " + aggroOnPlayer);
		}

		health = health - damageTaken;
		Debug.Log ("Enemy Health" + health);
		//damage Taken numbers ..... (if we want em)
		//Instantiate (damageTakenNumber, transform.position + new Vector3(0,0,1), transform.rotation);
		if (health <= 0)
			OnDeath();
	}



	/*Utilities______________________________________________________________________
	 *Utilities Include: OnDeath
	 *
	 */


	void OnDeath()
	{

		//Instantiae expOrb
		Instantiate(deadZombie, transform.position, transform.rotation);
		Destroy(this.gameObject); //until pooling
	}

	//End Utilities_____________________________________________________________________


	//Event & Update Handelers__________________________________________________________

	void Start(){

	}

	void Update ()
	{
	
	}

	void FixedUpdate(){

		//Debug.Log ("FU" + getPlayerInRange () + "_" + getAggro() + "_");
		Move (getPlayerInRange(), getAggro());
	}
	/*

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
*/
	//End Event & Update Handelers____________________________________________________

	

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


	public void setAggro(GameObject newAggro){
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
}


//add Lerping to Move()
//trigger into child? give own tag
	//-script possibly 
