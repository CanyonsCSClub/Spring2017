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
    private bool isPiercing;


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
        reloadedTime = 0f;

        //should probably have a file that loads this
        ammoCount       = 100;
        magazineSize    = 10;
        currentMagazine = 10;

        damage          = 1;
        range           = 10;
        velocity        = 1000;
        fireRate        = 0.5f;
        reloadTime      = 1.0f;

        isPiercing      = true;


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

    /// <summary>
    /// Fire, This is the function that the fire button should call
    /// This will handle the logic for if it should fire
    /// 
    /// </summary>
    /// <param name="bullet">This is where the bullet GameObject gets called (Must have BulletMover Script on it)</param>
    /// <param name="bulletSpawn">This is the spawnpoint of the bullet, make sure it is attached to the player but not in the player</param>
    public void Fire(GameObject bullet, Transform bulletSpawn)
    {
        if (Time.time > nextFire && currentMagazine != 0  && !isReloading)
        {
            if(!infiniteAmmo)
            {
                currentMagazine--;
                //ammoCount--;
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
        bulletClone.GetComponent<BulletMover>().isPiercing = isPiercing;

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
            ammoCount = ammoCount - (magazineSize - currentMagazine);
            currentMagazine = magazineSize;
            //ammoCount = ammoCount - (magazineSize - currentMagazine);
            Debug.Log(string.Format("MagDelta: {0} - {1} = {2}", magazineSize, currentMagazine, magazineSize - currentMagazine));
        }
        else 
        {
            currentMagazine = ammoCount;
            ammoCount = 0;
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

    public bool IsReloading()
    {
        return isReloading;
    }

    /************
     *  Utility *
     ************/

    public void InfiniteAmmo()
    {
        
    }
}
