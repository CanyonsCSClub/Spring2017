using UnityEngine;
using System.Collections;

public class ZombieScript : MonoBehaviour {

    public float speed;
    public Transform player;
    public GameObject deadZombie;
	public GameObject bloodSplatter;

    // private Rigidbody2D zombieRDB2D;
    private SpriteRenderer enemyRender;

    private int health;
    private int level;
    private int exp;
    private int attackDamage;
    private float attackSpeed;

    private float nextAttack;
    private float redTime;

    // Use this for initialization
    void Start ()
    {
		
        health          = 25;
        attackDamage    = 10;
        attackSpeed     = 2f;

        nextAttack = 0;
        redTime = 0;

        enemyRender = GetComponent<SpriteRenderer>();
       // zombieRDB2D = GetComponent<Rigidbody2D>();
    }
	
	// Update is called once per frame
	void FixedUpdate ()
    {
        GameObject currentTarget = GetClosestTarget();
        Move(currentTarget);
        Attack(currentTarget);

        //float z = Mathf.Atan2((player.transform.position.y - transform.position.y), (player.transform.position.x - transform.position.x)) * Mathf.Rad2Deg - 90;

        // transform.eulerAngles = new Vector3(0, 0, z);

        // zombieRDB2D.AddForce(gameObject.transform.up * speed);

    }

    void LateUpdate()
    {
        if (enemyRender.color == Color.red && health > 0 && redTime < Time.time)
            enemyRender.color = Color.white;
    }
		
    public void TakeDamage(int hitDamage)
    {
        health = health - hitDamage;
        enemyRender.color = Color.red;
        redTime = Time.time + 0.1f;
        if (health <= 0)
            OnDeath();
    }

    void OnDeath()
    {
        Instantiate(deadZombie, transform.position, transform.rotation);
        Destroy(gameObject);
    }

    void Move(GameObject currentTarget)
    {
        if (currentTarget == null)
            return; //idle?
        // Rotation to Target
        float z = Mathf.Atan2((currentTarget.transform.position.y - transform.position.y), (currentTarget.transform.position.x - transform.position.x)) * Mathf.Rad2Deg - 90;
        transform.eulerAngles = new Vector3(0, 0, z);
        // Moving to Target
        transform.position = Vector3.MoveTowards(transform.position, currentTarget.transform.position, speed * Time.deltaTime);
    }

    void Attack(GameObject currentTarget)
    {
        if (Time.time > nextAttack && CanIBite(currentTarget))
        {
            Vector3 bloodPosDelta = new Vector3(0, 0, 0.5f);
            Instantiate(bloodSplatter, currentTarget.transform.position + bloodPosDelta, currentTarget.transform.rotation);
            currentTarget.GetComponent<PlayerController>().TakeDamage(attackDamage);
            nextAttack = Time.time + attackSpeed;
        }
    }

    //**********Added by Chris Leal 9/10****************


    /// <summary>
    /// Enemies in the zombies eyes are GO's tagged player
    /// will only ever be 4 players, because there is only 4 physical players
    /// makes an array with all active players on map
    /// looks for closest one
    /// returns the closest one, to then moveTo or whatever else.
    /// </summary>
    /// <returns>returns the closest one, to then moveTo or whatever else.</returns>
    private GameObject GetClosestTarget()
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

	private bool CanIBite(GameObject currentTarget){
		float distanceToTarget = (transform.position - currentTarget.transform.position).sqrMagnitude;
		if (distanceToTarget < 4.0000f && currentTarget.tag == "Player") {
			return true;
		}
		return false;
	}


		

}
