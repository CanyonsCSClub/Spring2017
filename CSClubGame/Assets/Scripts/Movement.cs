using UnityEngine;
using System.Collections;

public class Movement : MonoBehaviour {
    protected Rigidbody2D PlayerRB2D;
    public float speed;

    void Start()
    {
        PlayerRB2D = GetComponent<Rigidbody2D>();
    }

    public static void Update () {
 
    }

    void testMovement() {
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


