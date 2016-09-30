using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour
{

    protected string playerName { get; set; }
    protected string playerClass { get; set; } //Do we need this? Are class attributes solely decided by individual class scripts?
    protected bool isMelee { get; set; } //Need?
    protected int playerLevel;
    protected Rigidbody2D PlayerRB2D;
    protected Animator anim;
    protected int health { get; set; }
    protected int BASE_HEALTH = 0;  //Perhaps we can overheal characters, base health will maintain original health value to limit or allow this feature
    protected float speed;

    protected int experience { get; set; }
    protected int currentLevelCeiling { get; set; }

    protected int ammo;
    protected int maxAmmo;


    public Player(string newPlayerName, string newPlayerClass, Rigidbody2D newPlayerRB2D, bool newIsMelee, int newPlayerLevel, int newHealth, float newSpeed, int newExperience, Animator newAnim)
    {
        this.playerName     = newPlayerName;
        this.playerClass    = newPlayerClass;
        this.PlayerRB2D     = newPlayerRB2D;
        this.isMelee        = newIsMelee;
        this.playerLevel    = newPlayerLevel;
        this.health         = newHealth;
        this.BASE_HEALTH    = health;
        this.speed          = newSpeed;
        this.experience     = newExperience;
        this.anim           = newAnim;

        Debug.Log(playerName + " " + playerClass + " " + playerLevel + " " + health
             + " " + isMelee + " " + speed + "  " + experience + "\n" + "has been created");



    }


    // Use this for initialization
    void Start()
    {
        DefaultSettings();
    }

    // Update is called once per frame
    void Update()
    {
     
    }
    
    void FixedUpdate()
    {
        Movement();
        Attack();
        LevelSystem();
    }

    public void DefaultSettings()
    {
        this.playerName     = "Default";
        this.playerClass    = "Default";
        this.PlayerRB2D     = GetComponent<Rigidbody2D>();
        this.isMelee        = false;
        this.playerLevel    = 1;
        this.health         = 100;
        this.BASE_HEALTH    = 100;
        this.speed          = 100;
        this.experience     = 0;
        this.anim           = GetComponent<Animator>();
    }

    public virtual void Movement()
    {
        //Inputs
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        //Player movement
        Vector2 movement = new Vector2(moveHorizontal, moveVertical);
        movement.Normalize();
        PlayerRB2D.AddForce(movement * speed);

        //Player rotation
        Quaternion rot = Quaternion.LookRotation(transform.position - mousePos, Vector3.forward);

        transform.rotation = rot;
        transform.eulerAngles = new Vector3(0, 0, transform.eulerAngles.z);
        PlayerRB2D.angularVelocity = 0;
    }

    public virtual void Attack()
    {
        if (Input.GetMouseButtonDown(0))
        {
            //NEED TO CHANGE
            anim.SetTrigger("Attack");
        }
        if (Input.GetMouseButtonDown(1))
        {
 
        }
    }

    public virtual void TakeDamage(int damageTaken)
    {
        health = health - damageTaken;
        if (health <= 0)
        {
            Debug.Log("You dead mofo");
        }
    }

    public void LevelSystem()
    {
        switch (playerLevel)
        {
            case 1:
                currentLevelCeiling = 10;
                break;
            case 2:
                currentLevelCeiling = 20;
                break;
            case 3:
                currentLevelCeiling = 30;
                break;
            case 4:
                currentLevelCeiling = 40;
                break;
            case 5:
                currentLevelCeiling = 50;
                break;
            case 6:
                currentLevelCeiling = 60;
                break;
            case 7:
                currentLevelCeiling = 70;
                break;
            case 8:
                currentLevelCeiling = 80;
                break;
            case 9:
                currentLevelCeiling = 90;
                break;
            case 10:
                currentLevelCeiling = 100;
                break;
        }
    }

    public void LevelUp()
    {
        health = health + 20; //Edit, placeholder value. Also may effect other attributes ie. speed, attack speed
        playerLevel++;
        Debug.Log(playerLevel + "   " + health);
    }

    public void GiveExp(int value)
    {
        experience += value;
        Debug.Log("EXP: " + experience + "\n");
        if (experience >= currentLevelCeiling)
        {
            LevelUp();
        }
    }
    
    public void GiveHealth(int value) //Compare to BASE_HEALTH to check and potentially overheal 
    {
        if (health < BASE_HEALTH)
            health += value;
    }

    public void GiveAmmo(int value) 
    {
        if (ammo < maxAmmo)
            ammo += value;
    }


}

