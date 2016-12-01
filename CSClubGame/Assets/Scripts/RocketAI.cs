using UnityEngine;
using System.Collections;

public class RocketAI : Enemy
{
    private Rigidbody2D bulletBody;
    private Vector2 start;


    private int setdamage = 5;
    private float setsight = 0.20f;
    private float setrange = 10f;
    private float setweaponVelocity = 700f;
    private float setfireRate = 0.5f;
    private int sethealth = 5;

    private int rocketDmg = 20;
    private float rocketSpeed = 200f;

    public GameObject areaEffect;
    public GameObject explodeEffect;

    // Use this for initialization
    void Start()
    {
        start = transform.position;
        ConfigEnemy("Drone Boss", 1, true, setrange, setsight, 10, sethealth, 5, setdamage, setfireRate, 5);
        Move();
    }

    // Update is called once per frame
    void Update()
    {

    }

    void FixedUpdate()
    {
        if (setrange < Vector2.Distance(start, transform.position))
        {
            SpawnAOE();
            Destroy(this.gameObject);
        }
            
    }

    public override void Move()
    {
        bulletBody = GetComponent<Rigidbody2D>();
        bulletBody.AddForce(gameObject.transform.up * rocketSpeed);
    }

    void OnTriggerEnter2D(Collider2D target)
    {
        GameObject targetObject = target.gameObject;

        if (target.tag == "World")
        {
            SpawnAOE();
            Destroy(gameObject);
        }

        if (target.tag == "Player")
        {
            SpawnAOE();
            targetObject.GetComponent<Player>().TakeDamage(rocketDmg, gameObject);
            Destroy(gameObject);
        }
    }

    void SpawnAOE()
    {
        if (this.explodeEffect != null)
            Instantiate(explodeEffect, transform.position, transform.rotation);
        if (this.areaEffect != null)
            Instantiate(areaEffect, transform.position, transform.rotation);
    }
}
