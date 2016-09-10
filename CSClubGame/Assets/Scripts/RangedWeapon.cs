using UnityEngine;
using System.Collections;

public class RangedWeapon : MonoBehaviour {

    public GameObject shot;

    private int damage;
    private float range;
    private float velocity;

    private int ammoCount;
    private int magazineSize;
    private int currentMagazine;

    private float fireRate;
    private float reloadTime;

    private float nextFire;
    private float reloadedTime;

    private bool infiniteAmmo;
    private bool isReloading;


    //Default Constructor
    public RangedWeapon()
    {
        Start();
    }

	// Use this for initialization
	void Start ()
    {
        infiniteAmmo = false;
        isReloading = false;
        nextFire = 0;

        //should probably have a file that loads this
        ammoCount = 100;
        magazineSize = 10;
        currentMagazine = 10;

        damage = 5;
        range = 10;
        velocity = 1000;
        fireRate = 0.5f;
        reloadTime = 1.0f;
        reloadedTime = 0f;

	}
	
	// Update is called once per frame (for Input)
	void Update ()
    {
	
	}

    // Updates for Physics
    void FixedUpdate()
    {

    }

    public void LateUpdate()
    {
        if (isReloading)
            ReloadDelay(reloadedTime);
    }

    // Fire function
    public void Fire(GameObject bullet, Transform bulletSpawn)
    {
        if (Time.time > nextFire && currentMagazine != 0  && !isReloading)
        {
            if(!infiniteAmmo)
            {
                currentMagazine--;
                ammoCount--;
            }

            nextFire = Time.time + fireRate;
            BulletSpawn(bullet, bulletSpawn);          //change to charge if its a charge up attack
            Debug.Log(string.Format("Firing: {0}/{1} : {2}",currentMagazine, magazineSize, ammoCount));
        }
    }

    // Start Charging up weapon if it is charged
    public void ChargingFire()
    {

    }

    // Fire Charged Weapon
    public void ChargedFire(float chargeTime)
    {

    }

    //  Spawn the bullet
    public void BulletSpawn(GameObject bullet, Transform bulletSpawn)
    {
        GameObject bulletClone = (GameObject)Instantiate(bullet, bulletSpawn.position, bulletSpawn.rotation);

        bulletClone.GetComponent<BulletMover>().speed = velocity;
        bulletClone.GetComponent<BulletMover>().damage = damage;
        bulletClone.GetComponent<BulletMover>().range = range;

        //// This is how you would do a shotgun
        // Quaternion rotation2 = bulletSpawn.rotation;
        // rotation2.Set(bulletSpawn.rotation.x, bulletSpawn.rotation.y, bulletSpawn.rotation.z + 0.1f, bulletSpawn.rotation.w);
        // Instantiate(bullet, bulletSpawn.position, rotation2);
    }

    // Reloading, starts thread so other methods are not delayed
    public void Reload()
    {
        if (ammoCount != 0)
        {
            isReloading = true;
            reloadedTime = Time.time + reloadTime;
            Debug.Log(string.Format("Reloading Start: {0}/{1} : {2}", currentMagazine, magazineSize, ammoCount));
            //Invoke("Reloading", reloadTime);
        }


    }

    void ReloadDelay(float delayTime)
    {
        //Debug.Log(string.Format("Reload delay: {0} > {1}", Time.time, delayTime));
        if (Time.time > delayTime)
        {
            Reloading();
        }
    }

    void Reloading ()
    {

        if (ammoCount > (magazineSize - currentMagazine))
        {
            currentMagazine = magazineSize;
        }
        else 
        {
            currentMagazine = ammoCount;
        }
        isReloading = false;
        Debug.Log(string.Format("Reloading Done: {0}/{1} : {2}", currentMagazine, magazineSize, ammoCount));
    }


    public void AddAmmo()
    {
        ammoCount += magazineSize;
    }

    /************
     *  Getters *
     ************/

    public int getAmmoCount()
    {
        return ammoCount;
    }

    public int getMagazineSize()
    {
        return magazineSize;
    }

    public int getCurrentMagazine()
    {
        return currentMagazine;
    }


    /************
     *  Utility *
     ************/

    public void InfiniteAmmo()
    {
        
    }
}
