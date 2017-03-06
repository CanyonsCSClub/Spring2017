using UnityEngine;
using System.Collections;
using UnityEngine.UI;//health slider

public class FactoryBoss : Enemy
{

    BossWeapon1 weapon;
    BossWeapon2 rocket;
    public Transform shotSpawn;
    public Transform rocketSpawn;

    public GameObject [] waypoints;

    public GameObject bosstarget;

    private Vector3[] vectorWaypoints;

    private int setdamage = 5;
    private float setsight = 20f;
    private float setrange = 20f;
    private float setweaponVelocity = 500f;
    private float setfireRate = 0.5f;
    private int sethealth = 500;

    private float moveTime = 50f;

	//for the health slider.
	public Color zeroHealthColor = Color.red;
	public Color fullHealthColor = Color.green;
	public Slider slider;
	public Image fillImage;

    void Start()
    {
        ConfigEnemy("Drone Boss", 1, true, setrange, setsight, 10, sethealth, 5, setdamage, setfireRate, 5);
        //enemySightRangeCollider = GetComponent<CircleCollider2D> ();
        //enemySightRangeCollider.radius = newRadius;
        weapon = new BossWeapon1(this.gameObject);
        rocket = new BossWeapon2(this.gameObject);
        weapon.setPerm(this.setdamage, this.setrange, this.setweaponVelocity, this.setfireRate);
        this.sightLine = 20;
        convertGObj2Vector3();
		SetHealthUI();
    }

    void Update()
    {
        Attack(getPlayerInRange());

    }


    public override void Move()
    {
        if (bosstarget == null)
            return;
        transform.position = Vector3.MoveTowards(transform.position,
                bosstarget.transform.position, moveArgSpeedModifier * speed * Time.deltaTime);
    }

    public override void Attack(GameObject currentTarget)
    {
        if (currentTarget == null)
            return;
        if (shotSpawn == null)
            return;
        //if (weapon == null)
        //return;
        if (rocketSpawn != null)
            rocket.Attack(rocketSpawn);

        if (shotSpawn != null)
            weapon.Attack(shotSpawn);
        else
            Debug.Log("shotSpawn null");
    }

    public override void TakeDamage(int damageTaken, GameObject GO)
    {

        base.TakeDamage(damageTaken, GO);

        Debug.Log("Boss Health: " + health);

		SetHealthUI ();//health slider

    }

	public void SetHealthUI(){//health slider
		slider.value = health;
		fillImage.color = Color.Lerp(Color.red, fullHealthColor, getHealthPercentage());

	}

    void convertGObj2Vector3()
    {
        int index = 0;
        vectorWaypoints = new Vector3[waypoints.Length];
        foreach(GameObject gobj in waypoints)
        {
            Debug.Log("Getting waypoint @ " + gobj.transform.position);
            this.vectorWaypoints[index] = gobj.transform.position;
            index++;
        }
    }

    public void setTarget(GameObject gobj)
    {
        this.bosstarget = gobj;
    }

}
