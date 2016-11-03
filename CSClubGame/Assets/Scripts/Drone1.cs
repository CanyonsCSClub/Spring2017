using UnityEngine;
using System.Collections;

public class Drone1 : Enemy {

    void Start()
    {
        ConfigEnemy("Drone", 1, true, 5, 7, 10, 25, 5, 5, 5, 5);
        //enemySightRangeCollider = GetComponent<CircleCollider2D> ();
        //enemySightRangeCollider.radius = newRadius;

    }

    public override void Attack(GameObject currentTarget)
    {

    }
}
