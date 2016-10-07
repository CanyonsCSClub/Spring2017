using UnityEngine;
using System.Collections;

public class RangedWeapon : MonoBehaviour {

    public GameObject shot;

    protected int damage;
    protected float range;
    protected float velocity;

	protected int ammoMax;
    protected int ammoCount;
    protected int magazineSize;
    protected int currentMagazine;

    protected float fireRate;
    protected float reloadTime;

    protected float nextFire;
    protected float reloadedTime;

    protected bool infiniteAmmo;
    protected bool isReloading;
    protected bool isPiercing;

	protected GameObject playerID;

	public RangedWeapon(){
		Start ();
	}


    //Default Constructor
	public RangedWeapon(GameObject playerid	)
    {
		playerID = playerid;
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
        ammoCount = 100;
		ammoMax = 200;
        magazineSize = 10;
        currentMagazine = 10;

        damage = 5;
        range = 10;
        velocity = 1000;
        fireRate = 0.5f;
        reloadTime = 1.0f;

        isPiercing = true;

        shot = Resources.Load("Bullet", typeof(GameObject)) as GameObject;
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
    /// Attack, This is the function that the fire button should call
    /// This will handle the logic for if it should fire, this is for semi auto
    /// 
    /// </summary>
    /// <param name="bullet">This is where the bullet GameObject gets called (Must have BulletMover Script on it)</param>
    /// <param name="bulletSpawn">This is the spawnpoint of the bullet, make sure it is attached to the player but not in the player</param>
    public virtual void Attack(Transform bulletSpawn)
    {
        if (Time.time > nextFire && currentMagazine != 0  && !isReloading)
        {
            if(!infiniteAmmo)
            {
                currentMagazine--;
            }

            nextFire = Time.time + fireRate;
            BulletSpawn(shot, bulletSpawn);          //change to charge if its a charge up attack
            //Debug.Log(string.Format("Firing: {0}/{1} : {2}",currentMagazine, magazineSize, ammoCount));
        }
    }

    /// <summary>
    /// AttackHold() is used when the player is holding down the button
    /// Fully Automatic fire should be controlled here or charging.
    /// </summary>
    public virtual void AttackHold(Transform bulletSpawn)
    {

    }

    /// <summary>
    /// AttackRelease() is the function for when the player releases the button
    /// This is a great way to activate a charged weapon.
    /// </summary>
    /// <param name="chargeTime"></param>
    public virtual void AttackRelease(Transform bulletSpawn)
    {

    }

    //  Spawn the bullet
    public virtual void BulletSpawn(GameObject bullet, Transform bulletSpawn)
    {
        GameObject bulletClone = (GameObject)Instantiate(bullet, bulletSpawn.position, bulletSpawn.rotation);

        if(bulletClone != null)
			bulletClone.GetComponent<BulletMover>().setParm(this.gameObject, velocity, range, damage, isPiercing);

        //// This is how you would do a shotgun
        // Quaternion rotation2 = bulletSpawn.rotation;
        // rotation2.Set(bulletSpawn.rotation.x, bulletSpawn.rotation.y, bulletSpawn.rotation.z + 0.1f, bulletSpawn.rotation.w);
        // Instantiate(bullet, bulletSpawn.position, rotation2);
    }

    // Reloading, starts thread so other methods are not delayed
    public virtual void Reload()
    {
        if (ammoCount != 0)
        {
            isReloading = true;
            reloadedTime = Time.time + reloadTime;
            //Debug.Log(string.Format("Reloading Start: {0}/{1} : {2}", currentMagazine, magazineSize, ammoCount));
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
            //Debug.Log(string.Format("MagDelta: {0} - {1} = {2}", magazineSize, currentMagazine, magazineSize - currentMagazine));
        }
        else 
        {
            currentMagazine = ammoCount;
            ammoCount = 0;
        }
        isReloading = false;
        //Debug.Log(string.Format("Reloading Done: {0}/{1} : {2}", currentMagazine, magazineSize, ammoCount));
    }


    public void AddAmmo()
	{
		ammoCount = ammoCount + (magazineSize / 4);
		//Debug.Log ("Adding ammo. ammoCount=" + ammoCount + "ammoMax=" + ammoMax);
		if (ammoCount > ammoMax) {
			ammoCount = ammoMax;
		}
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
