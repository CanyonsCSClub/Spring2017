using UnityEngine;
using System.Collections;

public class windBlast : BulletMover
{
    private float force = 1000;

	// Use this for initialization
	void Start () {
        isPiercing = true;
        Invoke("Die", .6f);
    }
	
	// Update is called once per frame
	void Update () {
	
	}

    void FixedUpdate()
    {

    }

    void OnTriggerStay2D(Collider2D other)
    {
        if (other.tag == "Enemy" && other.attachedRigidbody)
            other.attachedRigidbody.AddForce(new Vector3(other.transform.position.x - transform.position.x, other.transform.position.y - transform.position.y, 0) * force);
    }

    void Die()
    {
        Destroy(gameObject);
    }
}
