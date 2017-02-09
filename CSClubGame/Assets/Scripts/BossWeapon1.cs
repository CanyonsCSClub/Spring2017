using UnityEngine;
using System.Collections;

public class BossWeapon1 : Weapon
{
    private float shotSpread = 0.5f;
    // Use this for initialization
    void Start()
    {
        DefaultSettings();
        targetTags = new string[] { "Player" };

        isPiercing = false;

        InfiniteAmmo();

        shot = Resources.Load("AI_Bullet", typeof(GameObject)) as GameObject;
        shot.GetComponent<AIBullet>().setParm(this.playerID, velocity, range, damage, isPiercing, targetTags);
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
        fireRate = 0.5f;
        reloadTime = 1.0f;

        targetTags = new string[] { "Player" };
    }

    public BossWeapon1(GameObject playerid)
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

        shot = Resources.Load("AI_Bullet", typeof(GameObject)) as GameObject;
        shot.GetComponent<AIBullet>().setParm(this.playerID, velocity, range, damage, isPiercing, targetTags);
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
            if (bulletClone != null)
                bulletClone.GetComponent<AIBullet>().setParm(this.playerID, velocity, range, damage, isPiercing, targetTags);

            Quaternion rotation2 = bulletSpawn.rotation;
            rotation2.Set(bulletSpawn.rotation.x, bulletSpawn.rotation.y, bulletSpawn.rotation.z + shotSpread, bulletSpawn.rotation.w);
            bulletClone = (GameObject)Instantiate(bullet, bulletSpawn.position, rotation2);
            if (bulletClone != null)
                bulletClone.GetComponent<AIBullet>().setParm(this.playerID, velocity, range, damage, isPiercing, targetTags);

            Quaternion rotation3 = bulletSpawn.rotation;
            rotation3.Set(bulletSpawn.rotation.x, bulletSpawn.rotation.y, bulletSpawn.rotation.z - shotSpread, bulletSpawn.rotation.w);
            bulletClone = (GameObject)Instantiate(bullet, bulletSpawn.position, rotation3);
            if (bulletClone != null)
                bulletClone.GetComponent<AIBullet>().setParm(this.playerID, velocity, range, damage, isPiercing, targetTags);

        }
        else
        {
            bulletClone = (GameObject)Instantiate(bullet, bulletSpawn.position, bulletSpawn.rotation);
            if (bulletClone != null)
                bulletClone.GetComponent<AIBullet>().setParm(this.playerID, velocity, range, damage, isPiercing, targetTags);
            bulletClone.transform.parent = transform;


            Quaternion rotation2 = bulletSpawn.rotation;
            rotation2.Set(bulletSpawn.rotation.x, bulletSpawn.rotation.y, bulletSpawn.rotation.z + shotSpread, bulletSpawn.rotation.w);
            bulletClone = (GameObject)Instantiate(bullet, bulletSpawn.position, rotation2);
            if (bulletClone != null)
                bulletClone.GetComponent<AIBullet>().setParm(this.playerID, velocity, range, damage, isPiercing, targetTags);
            bulletClone.transform.parent = transform;

            Quaternion rotation3 = bulletSpawn.rotation;
            rotation3.Set(bulletSpawn.rotation.x, bulletSpawn.rotation.y, bulletSpawn.rotation.z - shotSpread, bulletSpawn.rotation.w);
            bulletClone = (GameObject)Instantiate(bullet, bulletSpawn.position, rotation3);
            if (bulletClone != null)
                bulletClone.GetComponent<AIBullet>().setParm(this.playerID, velocity, range, damage, isPiercing, targetTags);
            bulletClone.transform.parent = transform;
        }
    }

}
