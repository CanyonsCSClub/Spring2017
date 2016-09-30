using UnityEngine;
using System.Collections;

public class SubMachineGun : RangedWeapon
{
    public GameObject shot;



	public SubMachineGun(GameObject playerid)
    {
		playerID = playerid;
        infiniteAmmo    = false;
        isReloading     = false;
        nextFire        = 0;
        reloadedTime    = 0f;

        //should probably have a file that loads this
        ammoCount       = 300;
        magazineSize    = 30;
        currentMagazine = 30;

        damage          = 2;
        range           = 10;
        velocity        = 2000;
        fireRate        = 0.05f;
        reloadTime      = 1.0f;

        isPiercing = false;

        shot = Resources.Load("Bullet", typeof(GameObject)) as GameObject;
    }

    // Use this for initialization
    void Start () {

    }

    public void AttackHold(Transform bulletSpawn)
    {
        if (Time.time > nextFire && currentMagazine != 0 && !isReloading)
        {
            if (!infiniteAmmo)
            {
                currentMagazine--;
            }

            nextFire = Time.time + fireRate;
            BulletSpawn(shot, bulletSpawn);          //change to charge if its a charge up attack
            Debug.Log(string.Format("Firing: {0}/{1} : {2}", currentMagazine, magazineSize, ammoCount));
        }
    }

    //  Spawn the bullet
    public void BulletSpawn(GameObject bullet, Transform bulletSpawn)
    {
        Quaternion rotation2 = bulletSpawn.rotation;
        rotation2.Set(bulletSpawn.rotation.x, bulletSpawn.rotation.y, bulletSpawn.rotation.z + Random.Range(-0.1f,0.1f), bulletSpawn.rotation.w);
        GameObject bulletClone = (GameObject)Instantiate(bullet, bulletSpawn.position, rotation2);
		bulletClone.GetComponent<BulletMover>().setParm(playerID, velocity, range, damage, isPiercing);

        //// This is how you would do a shotgun
        // Quaternion rotation2 = bulletSpawn.rotation;
        // rotation2.Set(bulletSpawn.rotation.x, bulletSpawn.rotation.y, bulletSpawn.rotation.z + 0.1f, bulletSpawn.rotation.w);
        // Instantiate(bullet, bulletSpawn.position, rotation2);
    }
}
