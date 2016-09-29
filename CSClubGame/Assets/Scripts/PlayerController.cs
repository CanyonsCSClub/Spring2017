using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour {

    public float speed;
    public float fireRate;
    public GameObject shot;
    public Transform shotSpawn;
    public Text ammoText;

	private int health;

    private Rigidbody2D PlayerRDB2D;

    private RangedWeapon pistol;

    Animator meleeAnim;


	// Use this for initialization
	void Start ()
    {
        pistol = new RangedWeapon();
        PlayerRDB2D = GetComponent<Rigidbody2D>();
        meleeAnim = GetComponent<Animator>();
		health = 100;

        ammoText.text = string.Format("Ammo: {0}/{1} \nHealth: {2}", pistol.getCurrentMagazine(), pistol.getAmmoCount(), health);
    }

    void Update ()
    {
        if(Input.GetMouseButtonDown(0))
        {
            pistol.Fire(shot, shotSpawn);
        }
        if(Input.GetMouseButtonDown(1))
        {
            meleeAnim.SetTrigger("MeleeAttack");
        }

		//goig to change this from mouse button 2 to the keyboard key R
		if(Input.GetKeyDown(KeyCode.R) == true)
        {
            pistol.Reload();
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
        ammoText.text = string.Format("Ammo: {0}/{1} \nHealth: {2}", pistol.getCurrentMagazine(), pistol.getAmmoCount(), health);
        pistol.LateUpdate();
    }

	public void TakeDamage(int damageTaken){
		health = health - damageTaken;
		if (health <= 0) {
			//you dead
		}
	}
}
