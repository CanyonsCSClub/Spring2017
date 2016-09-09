using UnityEngine;
using System.Collections;

public class ZombieScript : MonoBehaviour {

    public float speed;
    public Transform player;
    private Rigidbody2D zombieRDB2D;

    private int health;
    private int level;
    private int exp;

    // Use this for initialization
    void Start ()
    {
        health = 25;

        zombieRDB2D = GetComponent<Rigidbody2D>();
    }
	
	// Update is called once per frame
	void FixedUpdate ()
    {
        float z = Mathf.Atan2((player.transform.position.y - transform.position.y), (player.transform.position.x - transform.position.x)) * Mathf.Rad2Deg - 90;

        transform.eulerAngles = new Vector3(0, 0, z);

        zombieRDB2D.AddForce(gameObject.transform.up * speed);

    }
}
