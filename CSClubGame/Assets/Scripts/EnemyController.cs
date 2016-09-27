using UnityEngine;
using System.Collections;

public class EnemyController : MonoBehaviour {


	public Enemy enemy;
	public GameObject bloodSplatter;
	public GameObject deadZombie;
	public GameObject expOrb;
	public GameObject damageTakenNumber;

	public CircleCollider2D enemySightRangeCollider;
	private bool playerInRange;
	private GameObject currentTarget;
	public float newRadius;
	GameObject[] playersInRange = new GameObject[4];



	void Start(){
		enemy = new Enemy("Zombie", 0, true, 3, 1, 100, 5, 10, 1f, 10);
		enemySightRangeCollider = GetComponent<CircleCollider2D> ();
		enemySightRangeCollider.radius = newRadius;
	}

	void OnTriggerStay(Collider other){
		if (other.CompareTag ("Player")) {
			playerInRange = true;
			addToTargets (other.gameObject);
		}
	}

	void OnTriggerExit(Collider other){
		if (other.CompareTag ("Player")) {
			playerInRange = false;
			removeFromTargets (other.gameObject);
		}
	}

	private void addToTargets(GameObject newTarget){
		int i = 0;
		GameObject currentTarget;
		while (true) {
			if (i >= 4) {
				break;
			}
			currentTarget = playersInRange [i];
			if (currentTarget == newTarget) {
				break;
			} else if (currentTarget == null){
				playersInRange [i] = newTarget;
				break;
			} else {
					i++;
			}
		}
	}

	private void removeFromTargets(GameObject exitingTarget){
		int i = 0;
		GameObject currentTarget;
		while (true) {
			if (i >= 4) {
				break;
			}
			currentTarget = playersInRange [i];
			if (currentTarget == null) {
				break;
			} else if (currentTarget == exitingTarget) {
				playersInRange [i] = null;
				break;
			} else {
				i++;
			}
		}
	}



	void FixedUpdate(){
		if (playerInRange) {
			GameObject currentTarget = GetClosestTarget (enemySightRangeCollider.radius);
			Move (currentTarget);
			Attack (currentTarget);
		} else {
			Move ();
		}
	}
	//UPDATED GET CLOSEST TARGET______
	/// <summary>
	/// instead of scanning the entire map for characters first
	/// the OnTriggerMethod makes an array of targets in range
	/// we use this array to then find the closest target, if there are more than one...
	/// this makes it so that even if all 4 playerts are in range it targets the closest one.
	/// </summary>
	/// <returns>The closest target.</returns>
	/// <param name="newRadius">radius of the circle collider.</param>
	/// 
	public GameObject GetClosestTarget (float newRadius){
		GameObject closest = null;
		Vector3 position = transform.position;
		foreach (GameObject player in playersInRange) {
			Vector3 dist = player.transform.position - position;
			float currentDistance = dist.sqrMagnitude;
			if (currentDistance < newRadius && player.activeInHierarchy) {
				closest = player;
				newRadius = currentDistance;
			}
		}
		return closest;
	}


//	public GameObject GetClosestTarget(float radius)
//	{ 
//		GameObject[] players;
//		players = GameObject.FindGameObjectsWithTag ("Player");
//		GameObject closest = null;
//		float maxDistToTarget = 500;
//		Vector3 position = transform.position;
//		foreach (GameObject player in players) {
//			Vector3 dist = player.transform.position - position;
//			float currentDistance = dist.sqrMagnitude;
//			if (currentDistance < maxDistToTarget && player.activeInHierarchy) {
//				closest = player;
//				maxDistToTarget = currentDistance;
//			}
//		}
//		return closest;
//	}


	public void Move(GameObject currentTarget){	
		if (enemy.CanIMove ()) {
			// Rotation to Target
			//Since there is going to be up to 4 players, i think there should be no reference to a player
			//but instead a reference to a target which can be any player.
			float z = Mathf.Atan2 ((currentTarget.transform.position.y - transform.position.y),
				          (currentTarget.transform.position.x - transform.position.x)) * Mathf.Rad2Deg - 90;
			transform.eulerAngles = new Vector3 (0, 0, z);
			// Moving to Target
			transform.position = Vector3.MoveTowards (transform.position, 
				currentTarget.transform.position, enemy.speed * Time.deltaTime);
		}
	}

	public void Move(){
		
		float speed = enemy.speed;
		var rotationVector = transform.rotation.eulerAngles;
		rotationVector.z += Random.Range (-30f, 30f);
		transform.rotation = Quaternion.Euler (rotationVector);
		transform.position += transform.forward * Time.deltaTime * speed;
	}



	float nextAttack = 0;
	public void Attack(GameObject currentTarget){
		
		if (Time.time > nextAttack  && CanIAttack(currentTarget)){
			Vector3 bloodPosDelta = new Vector3(0, 0, 0.5f);
			Instantiate(bloodSplatter, currentTarget.transform.position + bloodPosDelta, 
				currentTarget.transform.rotation);
			currentTarget.GetComponent<PlayerController>().TakeDamage(enemy.attack);
			nextAttack = Time.time + enemy.attackSpeed;
			enemy.lastTouchTime = Time.time;
			enemy.Frenzy (false); 
			enemy.lastTouchTime = 0f;
		}
	}

	public bool CanIAttack(GameObject currentTarget){
		float distanceToTarget = (transform.position - currentTarget.transform.position).sqrMagnitude;
		if (distanceToTarget < enemy.attackRange && currentTarget.tag == "Player") {
			return true;
		}
		return false;
	}

	public void TakeDamage(int damageTaken){
		enemy.health = enemy.health - damageTaken;
		//string damageString = damageTaken.ToString ();
		//make a number of somesort pop above the enemy's head
		//Instantiate (damageTakenNumber, transform.position + new Vector3(0,0,1), transform.rotation);
		if (enemy.health <= 0)
			OnDeath();
		if ((double)damageTaken > (double)(enemy.MAXHEALTH * .1) && enemy.flinchOn == true) {
			enemy.Flinch ();
		}
	}

	void OnDeath()
	{
		for (int i = 0; i < enemy.expGiven; i++) {
			Instantiate (expOrb, transform.position, transform.rotation);
		}
		//add a small explosive force, with no damage, to "explode" expOrbs from an enemy
		//kinda like minecraft? 
		//we can also do, for performance sake, Instantiate one exp orb with an exp value 
		//and just drop at death location?
		Instantiate(deadZombie, transform.position, transform.rotation);
		Destroy(gameObject);
	}


}
