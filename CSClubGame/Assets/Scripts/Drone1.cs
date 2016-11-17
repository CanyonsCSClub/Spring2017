using UnityEngine;
using System.Collections;

public class Drone1 : Enemy {

    AIWeapon weapon;
    public Transform shotSpawn;

    private int damage              = 5;
    private float range             = 10f;
    private float weaponVelocity    = 500f;
    private float fireRate          = 0.5f;

    void Start()
    {
        ConfigEnemy("Drone", 1, true, 5, 7, 10, 25, 5, 5, 5, 5);
        //enemySightRangeCollider = GetComponent<CircleCollider2D> ();
        //enemySightRangeCollider.radius = newRadius;
        weapon = new AIWeapon(this.gameObject);
        weapon.setPerm(this.damage, this.range, this.weaponVelocity, this.fireRate);

    }

    public override void Attack(GameObject currentTarget)
    {
        if (currentTarget == null)
            return;
        if(shotSpawn == null)
            return;

        weapon.Attack(shotSpawn);
        //Debug.Log("Enemy attack");
    }

    void Update()
    {
        Attack(getPlayerInRange());
    }
}
