using UnityEngine;
using System.Collections;

public class BulletMover : MonoBehaviour
{
    public float speed;
    public float range;
    private Rigidbody2D bulletBody;
    private Vector2 start;

    void Start()
    {
        start = transform.position;
        bulletBody = GetComponent<Rigidbody2D>();
        bulletBody.AddForce(gameObject.transform.up * speed);
    }

    void FixedUpdate()
    {
        if (range < Vector2.Distance(start, transform.position))
            Destroy(this.gameObject); 
    }

    public void setSpeed(float inputSpeed)
    {
        speed = inputSpeed;
    }

    public void setRange(float inputRange)
    {
        range = inputRange;
    }
    
}
