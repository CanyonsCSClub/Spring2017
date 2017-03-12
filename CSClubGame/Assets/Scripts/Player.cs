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
	//was protected

	public int health { get; set; }
    protected int BASE_HEALTH = 0;  //Perhaps we can overheal characters, base health will maintain original health value to limit or allow this feature
    protected float speed;
    protected bool alive;

    protected int experience { get; set; }
    protected int currentLevelCeiling { get; set; }

    protected int ammo;
    protected int maxAmmo;

    private float damageShaderTime;
    private float damageShaderCooldown;
    private SpriteRenderer PlayerRender;

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

    }

    /// <summary>
    /// Assigns default values to player. Will be overridden by sub classes (specific player archetypes)
    /// alive is referenced when checking for pickups and expgained
    /// </summary>
    void Start()
    {
        DefaultSettings();
    }

    // Update is called once per frame
    void Update()
    {
        InputControls();
    }

    void FixedUpdate()
    {
        Movement();
        LevelSystem();
        shaderController();
    }

    /// <summary>
    /// Assigns default values to player. Will be overridden by sub classes (specific player archetypes)
    /// </summary>
    public void DefaultSettings()
    {
        this.playerName     = "Default";
        this.playerClass    = "Default";
        this.PlayerRB2D     = GetComponent<Rigidbody2D>();
        this.isMelee        = false;
        this.playerLevel    = 1;
        this.health         = 40;
        this.BASE_HEALTH    = health;
        this.speed          = 100;
        this.experience     = 0;
        this.anim           = GetComponent<Animator>();
        this.alive          = true;

        this.damageShaderCooldown   = 0.1f;
        this.damageShaderTime       = 0f;
        PlayerRender = GetComponent<SpriteRenderer>();
    }

    /// <summary>
    /// Move system for your character goes here.  Note becareful with editing this.
    /// </summary>
    public virtual void Movement()
    {
        //Inputs
        Vector3 mousePos = Camera.main.ScreenPointToRay(Input.mousePosition).origin;
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        //Player movement
        Vector2 movement = new Vector2(moveHorizontal, moveVertical);
        movement.Normalize();
        PlayerRB2D.AddForce(movement * speed);

        //Player rotation
        float z = Mathf.Atan2(((mousePos.y) - transform.position.y), ((mousePos.x) - transform.position.x)) * Mathf.Rad2Deg - 90;
        //transform.eulerAngles = new Vector3(0, 0, z);
        transform.eulerAngles = Vector3.Lerp(transform.eulerAngles, new Vector3(0, 0, z), 10.0f);
        //Vector3 rotationLerp = new Vector3(0, 0, z);


        PlayerRB2D.angularVelocity = 0;
    }

    /// <summary>
    /// Default Controls for your player character goes in this function.
    /// </summary>
    public virtual void InputControls()
    {

    }

    /// <summary>
    /// Takes damage passed to method and modifies health, unless dead, which will return 0
    /// </summary>
    /// <param name="damageTaken"></param>
    public virtual void TakeDamage(int damageTaken)
    {
        if (health > 0)
        {
            PlayerRender.color = Color.red;
            this.damageShaderTime = Time.time + this.damageShaderCooldown;
            if (health - damageTaken <= 0)
                health = 0;
            else
                health = health - damageTaken;
        }
        Debug.Log(damageTaken + " dmg taken. Started at " + BASE_HEALTH + " and now at " + health + " Percent rem:" + getHealthPercent());
        if (health <= 0)
        {
            alive = false;
            death();
            health = 0;
        }
    }

    public virtual void TakeDamage(int damageTaken, GameObject gObj)
    {
        if (health > 0)
        {
            PlayerRender.color = Color.red;
            this.damageShaderTime = Time.time + this.damageShaderCooldown;
            if (health - damageTaken <= 0)
                health = 0;
            else
                health = health - damageTaken;
        }
        Debug.Log(damageTaken + " dmg taken. Started at " + BASE_HEALTH + " and now at " + health + " Percent rem:" + getHealthPercent());
        if (health <= 0)
        {
            alive = false;
            death();
            health = 0;
        }
    }

    public void death()
    {
        speed = 0;
        GetComponent<SpriteRenderer>().enabled = false;
        GetComponent<BoxCollider2D>().enabled = false;
        transform.Translate(0, 0, -10);
    }

    void shaderController()
    {
        if (PlayerRender.color == Color.red && health > 0 && damageShaderTime < Time.time)
            PlayerRender.color = Color.white;
    }

    /// <summary>
    /// Simplistic leveling system, framework subject to change with group's input.
    /// Called in update, checks current playerLevel against switch to determine experience needed to playerLevel++
    /// </summary>
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

    /// <summary>
    /// Receives exp value and adds it to experience, then checks against currentLevelCeiling 
    /// to determine if LevelUp() is called
    /// </summary>
    /// <param name="value"></param>
    public bool GiveExp(int value)
    {
        if (alive)
        {
            experience += value;
            Debug.Log("EXP: " + experience + "\n");
            if (experience >= currentLevelCeiling)
            {
                LevelUp();
            }
            return true;
            
        }
        return false;
    }

    /// <summary>
    /// When called from GiveExp, adds some value to BASE_HEALTH, fully heals player to new BASE_HEALTH, playerLevel++
    /// </summary>
    public void LevelUp()
    {
        BASE_HEALTH += 20; //Edit, placeholder value. Also may effect other attributes ie. speed, attack speed
        health = BASE_HEALTH;
        playerLevel++;
        Debug.Log(playerLevel + "   " + health);
    }

    /// <summary>
    /// Receives health value from orb, etc.. Checks to determine if healing is necessary, and also to make sure player is alive
    /// Then adds value to health
    /// Also, caps health at BASE_HEALTH. We can implement an overheal mechanic or a shielding system by modifiying this.
    /// </summary>
    /// <param name="value"></param>
    public bool GiveHealth(int value) //Compare to BASE_HEALTH to check and potentially overheal 
    {
        if (health < BASE_HEALTH && alive)
        {
            if (health + value > BASE_HEALTH)
            {
                health = BASE_HEALTH;
                return true;
            }
            else
            {
                health += value;
                return true;
            }
        }
        return false;

    }

    /// <summary>
    /// Checks 
    /// </summary>
    /// <param name="value"></param>
    public virtual void GiveAmmo(int value)
    {
        if (alive)
        {
            if (ammo + value > maxAmmo)
                ammo = maxAmmo;
            else
                ammo += value;
        }

    }

    /// <summary>
    /// alive state
    /// </summary>
    /// <returns></returns>
    public bool isAlive() {
        if (alive)
            return true;
        else
            return false;
    }

    /// <summary>
    /// Get Functions   
    /// </summary>
    /// <returns></returns>
    public int getHealth()
    {
        return health;
    }
    public int getBASE_HEALTH()
    {
        return BASE_HEALTH;
    }
    public float getHealthPercent()
    {
        if (!alive)
            return 0;
        else
            return ((float)health / (float)BASE_HEALTH);
    }

    public int getExp()
    {
        if (experience <= 0)
            return 0;
        else
            return experience;
    }
    public float getExpPercent()
    {
        if (experience <= 0)
            return 0;
        else
            return ((float)experience / (float)currentLevelCeiling);
    }

    public virtual string getHUDString()
    {
        return string.Format("Health:" + health + "/" + BASE_HEALTH);
    }






}

