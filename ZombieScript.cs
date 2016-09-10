using UnityEngine;
using System.Collections;

public class ZombieScript : MonoBehaviour {

    public float speed;
    public Transform player;
    public GameObject deadZombie;
	public GameObject bloodSplatter;

   // private Rigidbody2D zombieRDB2D;

    private int health;
    private int level;
    private int exp;



    // Use this for initialization
    void Start ()
    {
		
        health = 25;
       // zombieRDB2D = GetComponent<Rigidbody2D>();
    }
	
	// Update is called once per frame
	void FixedUpdate ()
    {
		GameObject currentTarget = GetClosestTarget ();
		transform.position = Vector3.MoveTowards (transform.position, currentTarget.transform.position, speed * Time.deltaTime);
		if (CanIBite (currentTarget)) {
			Vector3 bloodPosDelta = new Vector3(0,0,0.5f);
			Instantiate(bloodSplatter,currentTarget.transform.position + bloodPosDelta, currentTarget.transform.rotation);
			currentTarget.GetComponent<PlayerController>().TakeDamage(10);
		}


		//float z = Mathf.Atan2((player.transform.position.y - transform.position.y), (player.transform.position.x - transform.position.x)) * Mathf.Rad2Deg - 90;

       // transform.eulerAngles = new Vector3(0, 0, z);

       // zombieRDB2D.AddForce(gameObject.transform.up * speed);

    }
		
    public void Damage(int hitDamage)
    {
        health = health - hitDamage;
        if (health <= 0)
            OnDeath();
    }

    void OnDeath()
    {
        Instantiate(deadZombie, transform.position, transform.rotation);
        Destroy(gameObject);
    }


	//**********Added by Chris Leal 9/10****************



	private GameObject GetClosestTarget(){ // enemies in the zombies eyes are GO's tagged player
		/*will only ever be 4 players, because there is only 4 physical players
		 * makes an array with all active players on map
		 * looks for closest one
		 * returns the closest one, to then moveTo or whatever else.
		 */
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

	private bool CanIBite(GameObject currentTarget){
		float distanceToTarget = (transform.position - currentTarget.transform.position).sqrMagnitude;
		if (distanceToTarget < 4.0000f && currentTarget.tag == "Player") {
			return true;
		}
		return false;
	}


		

}
