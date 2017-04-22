using UnityEngine;
using System.Collections;

public class BossWeapon2 : Weapon
{

    // Use this for initialization
    void Start()
    {
        DefaultSettings();
        targetTags = new string[] { "Player" };

        isPiercing = false;

        InfiniteAmmo();

        shot = Resources.Load("Rocket", typeof(GameObject)) as GameObject;

    }

    public override void DefaultSettings()
    {
        infiniteAmmo = false;
        isReloading = false;
        isAttached = false;
        nextFire = 0;
        reloadedTime = 0f;

        ammoCount = 100;
        ammoMax = 200;
        magazineSize = 10;
        currentMagazine = 10;

        damage = 5;
        range = 10;
        velocity = 500f;
        fireRate = 10.0f;
        reloadTime = 1.0f;

        targetTags = new string[] { "Player" };
    }

    public BossWeapon2(GameObject playerid)
    {
        playerID = playerid;
        Start();
    }

    public void setPerm(int inDmg, float inRange, float inVelocity, float inFireRate)
    {
        DefaultSettings();

        this.damage = inDmg;
        this.range = inRange;
        this.velocity = inVelocity;
        this.fireRate = inFireRate;

        targetTags = new string[] { "Player" };

        isPiercing = false;

        InfiniteAmmo();

        shot = Resources.Load("Rocket", typeof(GameObject)) as GameObject;

    }


    public override void BulletSpawn(GameObject bullet, Transform bulletSpawn)
    {
        GameObject bulletClone;

        //null check
        if (this.playerID == null)
            Debug.Log("gameObject null");
        if (this.velocity == null)
            Debug.Log("velocity null");
        if (this.range == null)
            Debug.Log("range null");
        if (this.damage == null)
            Debug.Log("damage null");
        if (this.isPiercing == null)
            Debug.Log("isPiercing null");
        if (this.targetTags == null)
            Debug.Log("targetTags null");

        if (!this.isAttached)
        {
            bulletClone = (GameObject)Instantiate(bullet, bulletSpawn.position, bulletSpawn.rotation);


        }
        else
        {
            bulletClone = (GameObject)Instantiate(bullet, bulletSpawn.position, bulletSpawn.rotation);

            bulletClone.transform.parent = transform;
        }
    }

}
