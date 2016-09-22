using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {

    protected string playerName { get; set; }
    protected GameObject playerClass { get; set; }
    protected bool isMelee { get; set; }
    protected int playerLevel;
    protected Rigidbody2D PlayerRB2D;

    protected int health { get; set; }
    protected float speed { get; set; }
    protected int experience { get; set; }

    

    public Player(string newPlayerName, GameObject newPlayerClass, Rigidbody2D newPlayerRB2D, bool newIsMelee, int newPlayerLevel, int newHealth, float newSpeed, int newExperience)
    {
        this.playerName = newPlayerName;
        this.playerClass = newPlayerClass;
        this.PlayerRB2D = GetComponentInChildren<Rigidbody2D>();
        this.isMelee = newIsMelee;
        this.playerLevel = newPlayerLevel;
        this.health = newHealth;
        this.speed = newSpeed;

        Debug.Log(playerName + " " + playerClass + " " + playerLevel + " " + health
             + " " + isMelee + " " + speed + "  " + experience + "\n" + "has been created");



    }


    // Use this for initialization
    void Start () {
        PlayerRB2D = GetComponent<Rigidbody2D>();
        speed = 80; // Hardcoded placeholder
    }

// Update is called once per frame
    void Update()
    {

        if (Input.GetMouseButtonDown(0))
        {
            //Attack1
        }
        if (Input.GetMouseButtonDown(1))
        {
            //Attack2
        }

    }
    void FixedUpdate()
    {
        movement();
    }
    public void movement() {
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
}

