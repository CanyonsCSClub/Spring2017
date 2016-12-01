using UnityEngine;
using System.Collections;

public class FactoryBoss : Enemy
{

    AIWeapon weapon;
    public Transform shotSpawn;

    private int damage = 5;
    private float sight = 20f;
    private float range = 20f;
    private float weaponVelocity = 500f;
    private float fireRate = 0.5f;
    private int health = 500;

    void Start()
    {
        ConfigEnemy("Drone Boss", 1, true, range, sight, 10, health, 5, damage, fireRate, 5);
        //enemySightRangeCollider = GetComponent<CircleCollider2D> ();
        //enemySightRangeCollider.radius = newRadius;
        weapon = new AIWeapon(this.gameObject);
        weapon.setPerm(this.damage, this.range, this.weaponVelocity, this.fireRate);
        this.sightLine = 20;

    }

    public override void Move()
    {
        
    }

    public override void Attack(GameObject currentTarget)
    {
        if (currentTarget == null)
            return;
        if (shotSpawn == null)
            return;
        //if (weapon == null)
        //return;


        if (shotSpawn != null)
            weapon.Attack(shotSpawn);
        else
            Debug.Log("shotSpawn null");
    }

    void Update()
    {
        Attack(getPlayerInRange());
    }
}
