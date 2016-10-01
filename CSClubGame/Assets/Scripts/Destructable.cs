using UnityEngine;
using System.Collections;

public class Destructable : ZombieScript
{

    public int health;
    

	// Use this for initialization
	void Start () {
        redTime = 0;

        enemyRender = GetComponent<SpriteRenderer>();

        if (this.tag != "Enemy")
            this.tag = "Enemy";
        
	
	}

    public override void Move(GameObject currentTarget)
    {

    }

    public override void Attack(GameObject currentTarget)
    {

    }


    public override void TakeDamage(int damageTaken)
    {
        Debug.Log("Wall taking dmg");
        health = health - damageTaken;
        if (health < 0)
            Destroy(gameObject);
    }
}
