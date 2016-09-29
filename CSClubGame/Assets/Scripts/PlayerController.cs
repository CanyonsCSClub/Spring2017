using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour {

    public float speed;
    public float fireRate;
    public Transform shotSpawn;
    public Text ammoText;
    public Image healthBar;

	private int health;
    private int maxHealth;
    private float redTime;
    private Vector3 healthBarScale;
    private float healthBarScaleMax;

    private Rigidbody2D PlayerRDB2D;
    private SpriteRenderer PlayerRender;

    private SubMachineGun rangedAttack;

    Animator meleeAnim;


	// Use this for initialization
	void Start ()
    {
        redTime = 0;
        rangedAttack = new SubMachineGun();

        PlayerRDB2D = GetComponent<Rigidbody2D>();
        PlayerRender = GetComponent<SpriteRenderer>();
        meleeAnim = GetComponent<Animator>();
		health = 100;
        maxHealth = 100;
        healthBarScaleMax = healthBar.rectTransform.localScale.y;

        playerHUD();
    }

    void Update ()
    {
        if(Input.GetMouseButtonDown(0))
        {
            rangedAttack.Attack(shotSpawn);
        }
        if(Input.GetMouseButton(0))
        {
            rangedAttack.AttackHold(shotSpawn);
        }
        if(Input.GetMouseButtonUp(0))
        {
            rangedAttack.AttackRelease(shotSpawn);
        }
        if(Input.GetMouseButtonDown(1))
        {
            meleeAnim.SetTrigger("MeleeAttack");
        }

		//goig to change this from mouse button 2 to the keyboard key R
		if(Input.GetKeyDown(KeyCode.R) == true)
        {
            rangedAttack.Reload();
        }
    }
	

	void FixedUpdate ()
    {
        //Inputs
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        //Player movement
        Vector2 movement = new Vector2(moveHorizontal, moveVertical);
        movement.Normalize();
        PlayerRDB2D.AddForce(movement * speed);

        //Player rotation
        Quaternion rot = Quaternion.LookRotation(transform.position - mousePos,Vector3.forward);

        transform.rotation = rot;
        transform.eulerAngles = new Vector3(0, 0, transform.eulerAngles.z);
        PlayerRDB2D.angularVelocity = 0;

    }

    void LateUpdate()
    {
        //HUD
        playerHUD();


        rangedAttack.LateUpdate();
        if (PlayerRender.color == Color.red && health > 0 && redTime < Time.time)
            PlayerRender.color = Color.white;
    }

	public void TakeDamage(int damageTaken){
		health = health - damageTaken;
        PlayerRender.color = Color.red;
        redTime = Time.time + 0.1f;

        if (health <= 0) {
            PlayerRender.color = Color.grey;
            //you dead
        }
	}

    void playerHUD()
    {
        //HUD
        ammoText.text = string.Format("Ammo: \n{0}/{1} \n", rangedAttack.getCurrentMagazine(), rangedAttack.getAmmoCount());
        float hbScale = ((float)health / (float)maxHealth) * healthBarScaleMax;
        if (hbScale < 0)
            hbScale = 0;
        healthBarScale = new Vector3(healthBar.rectTransform.localScale.x, hbScale, 1);
        healthBar.rectTransform.localScale = healthBarScale;
    }
}
