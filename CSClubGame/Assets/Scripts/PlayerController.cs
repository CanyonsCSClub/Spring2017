using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {

    public float speed;
    private Rigidbody2D PlayerRDB2D;
    Animator meleeAnim;

	// Use this for initialization
	void Start ()
    {
        PlayerRDB2D = GetComponent<Rigidbody2D>();

        meleeAnim = GetComponent<Animator>();
    }

    void Update ()
    {
        if(Input.GetMouseButtonDown(1))
        {
            meleeAnim.SetTrigger("MeleeAttack");
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
}
